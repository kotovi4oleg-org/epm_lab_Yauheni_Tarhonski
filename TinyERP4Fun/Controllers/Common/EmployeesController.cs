using System;
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
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesCommon_User)]
    public class EmployeesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly ICommonService _commonService;

        public EmployeesController(DefaultContext context,ICommonService commonService)
        {
            _context = context;
            _commonService = commonService;
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
            var employee = await _commonService.GetEmployeeInfo(id);
            if (employee == null) return NotFound();
            return View(employee);
        }
        private void SetViewBag()
        {
            ViewBag.People = ControllerCommonFunctions.AddFirstItem(new SelectList(_context.Person, "Id", "FullName"));
            ViewBag.Departments = ControllerCommonFunctions.AddFirstItem(new SelectList(_context.Department, "Id", "Name"));
            ViewBag.Positions = ControllerCommonFunctions.AddFirstItem(new SelectList(_context.Position, "Id", "Name"));
        }
        // GET: Employees/Create
        public IActionResult Create()
        {
            SetViewBag();
            return View();
        }
        // POST: Employees/Create
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
            SetViewBag();
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var employee = await _commonService.GetEmployeeInfo(id);
            if (employee == null) return NotFound();
            SetViewBag();
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Address,PersonId,DepartmentId,PositionId")] Employee employee)
        {
            if (id != employee.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            SetViewBag();
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var employee = await _commonService.GetEmployeeInfo(id);
            if (employee == null) return NotFound();
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
