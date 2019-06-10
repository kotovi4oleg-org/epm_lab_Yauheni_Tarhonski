﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class EmployeesController : Controller
    {
        private readonly DefaultContext _context;

        public EmployeesController(DefaultContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var defaultContext =  _context.Employee.Include(x => x.Person)
                                               .Include(x => x.Department)
                                               .Include(x => x.Position)
                                               .OrderBy(x => x.Person.LastName)
                                               .ThenBy(x => x.Person.FirstName);
            return View(await PaginatedList<Employee>.CreateAsync(defaultContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.Include(x=>x.Person)
                                                  .Include(x => x.Department)
                                                  .Include(x => x.Position)
                                                  .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            /* MSDN:
             * We don't recommend using ViewBag or ViewData with the Select Tag Helper. 
             * A view model is more robust at providing MVC metadata and generally less problematic.
             * Но, т.к. задание тестовое, для упрощения будем все-таки использовать ViewBag.
             */
            ViewBag.People = CommonFunctions.AddFirstItem(new SelectList(_context.Person, "Id", "FullName"));
            ViewBag.Departments = CommonFunctions.AddFirstItem(new SelectList(_context.Department, "Id", "Name"));
            ViewBag.Positions = CommonFunctions.AddFirstItem(new SelectList(_context.Position, "Id", "Name"));
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,PersonId,DepartmentId,PositionId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            /* MSDN:
             * We don't recommend using ViewBag or ViewData with the Select Tag Helper. 
             * A view model is more robust at providing MVC metadata and generally less problematic.
             * Но, т.к. задание тестовое, для упрощения будем все-таки использовать ViewBag.
             */
            ViewBag.People = CommonFunctions.AddFirstItem(new SelectList(_context.Person, "Id", "FullName"));
            ViewBag.Departments = CommonFunctions.AddFirstItem(new SelectList(_context.Department, "Id", "Name"));
            ViewBag.Positions = CommonFunctions.AddFirstItem(new SelectList(_context.Position, "Id", "Name"));
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Address,PersonId,DepartmentId,PositionId")] Employee employee)
        {

            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.Include(x => x.Person)
                                                  .Include(x => x.Department)
                                                  .Include(x => x.Position)
                                                  .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(long? id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}