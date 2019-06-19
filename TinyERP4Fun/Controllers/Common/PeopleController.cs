using System;
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
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }
        // GET: People
        [Authorize(Roles = Constants.rolesCommon_User)]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            return View(await PaginatedList<Person>.CreateAsync(_peopleService.GetIQueryable(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: People/Details/5
        [Authorize(Roles = Constants.rolesCommon_User)]
        public async Task<IActionResult> Details(long? id)
        {
            var person = await _peopleService.GetAsync(id);
            if (person == null) return NotFound();
            return View(person);
        }

        private async Task SetCommonViewBag(long? id)
        {
            ViewBag.Users = await _peopleService.GetUsersIds(id);
            ViewBag.Companies = _peopleService.GetCompaniesIds();
        }
        // GET: People/Create
        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> Create()
        {
            await SetCommonViewBag(null);
            ViewBag.Roles = _peopleService.GetRolesIds();
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
                await _peopleService.AddAsync(person);
                return RedirectToAction(nameof(Index));
            }
            await SetCommonViewBag(null);
            ViewBag.Roles = _peopleService.GetRolesIds();
            return View(person);
        }

        // GET: People/Edit/5
        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> Edit(long? id)
        {
            var person = await _peopleService.GetAsync(id, true);
            if (person == null) return NotFound();
            await SetCommonViewBag(id);
            var personRoles = person.UserId==null?null:await _peopleService.GetRolesAsync(person.User);
            var viewmodel = new PersonViewModel
            {
                Person = person,
                RolesList = _peopleService.GetRolesNames(),
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
            
            if (id != personViewModel?.Person.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                await SetCommonViewBag(id);
                return View(personViewModel);
            }
            if (!await _peopleService.UpdateAsync(personViewModel)) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        // GET: People/Delete/5
        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> Delete(long? id)
        {
            var person = await _peopleService.GetAsync(id);
            if (person == null) return NotFound();
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _peopleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
