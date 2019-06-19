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
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int? pageNumber)
        {
            return View(await PaginatedList<Employee>.CreateAsync(_employeesService.GetIQueryable(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var employee = await _employeesService.GetAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }
        private void SetViewBag()
        {
            ViewBag.People = _employeesService.GetPersonsIds();
            ViewBag.Departments = _employeesService.GetDepartmentsIds();
            ViewBag.Positions = _employeesService.GetPositionsIds();
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
                await _employeesService.AddAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            SetViewBag();
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var employee = await _employeesService.GetAsync(id, true);
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
                if (!await _employeesService.UpdateAsync(employee)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            SetViewBag();
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var employee = await _employeesService.GetAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _employeesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
