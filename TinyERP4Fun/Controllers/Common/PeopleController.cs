﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.ModelServiceInterfaces;
using TinyERP4Fun.ViewModels;

namespace TinyERP4Fun.Controllers
{
    public class PeopleController : Controller
    {
        private readonly DefaultContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICommonService _commonService;

        public PeopleController(DefaultContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ICommonService commonService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _commonService = commonService;
        }
        // GET: People
        [Authorize(Roles = Constants.rolesCommon_User)]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var defaultContext = _context.Person.Include(x => x.User)
                                               .Include(x => x.Company)
                                               .OrderBy(x => x.LastName)
                                               .ThenBy(x => x.FirstName);
            return View(await PaginatedList<Person>.CreateAsync(defaultContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: People/Details/5
        [Authorize(Roles = Constants.rolesCommon_User)]
        public async Task<IActionResult> Details(long? id)
        {
            var person = await _commonService.GetPersonInfo(id);
            if (person == null) return NotFound();
            return View(person);
        }

        private async Task SetCommonViewBag(long? id)
        {
            var defaultAdmin = new List<IdentityUser> { await _userManager.FindByNameAsync(Constants.defaultAdminName) };
            var usedUsers = _context.Person.Where(x => x.User != null&&x.Id!=id).Select(x => x.User);
            ViewBag.Users = ControllerCommonFunctions.AddFirstItem(new SelectList(_userManager.Users.Except(defaultAdmin).Except(usedUsers), "Id", "Email"));
            ViewBag.Companies = ControllerCommonFunctions.AddFirstItem(new SelectList(_context.Company, "Id", "Name"));
        }
        // GET: People/Create
        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> Create()
        {
            await SetCommonViewBag(null);
            ViewBag.Roles = new SelectList(_roleManager.Roles, "Id", "Name");
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Birthday,IsEmploye,IsChief,UserId,CompanyId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                if (person.IsEmployee)
                {
                    Employee employee = new Employee { Person = person };
                    _context.Add(employee);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await SetCommonViewBag(null);
            ViewBag.Roles = new SelectList(_roleManager.Roles, "Id", "Name");
            return View(person);
        }

        // GET: People/Edit/5
        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> Edit(long? id)
        {
            var person = await _commonService.GetPersonInfo(id);
            if (person == null) return NotFound();
            await SetCommonViewBag(id);
            var personRoles = person.UserId==null?null:await _userManager.GetRolesAsync(person.User);
            var viewmodel = new PersonViewModel
            {
                Person = person,
                RolesList = new SelectList(_roleManager.Roles, "Name", "Name"),
                SelectedRoles = personRoles
            };
            return View(viewmodel);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> Edit(long id, PersonViewModel personViewModel)
        {
            var person = personViewModel.Person;
            var selectedRoles = personViewModel.SelectedRoles;
            if (id != person.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                await SetCommonViewBag(id);
                return View(personViewModel);
            }
            
            try
            {
                if (person.UserId!=null)
                {
                    var user = await _userManager.FindByIdAsync(person.UserId);
                    var userRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    if (selectedRoles != null)
                        await _userManager.AddToRolesAsync(user, selectedRoles);
                }
                _context.Update(person);
                if (person.IsEmployee && _context.Employee.Where(x=>x.PersonId == person.Id).Count()==0)
                {
                    Employee employee = new Employee { Person = person };
                    _context.Add(employee);
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(person.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: People/Delete/5
        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> Delete(long? id)
        {
            var person = await _commonService.GetPersonInfo(id);
            if (person == null) return NotFound();
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(long? id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
