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
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsService _departmentsService;

        public DepartmentsController(IDepartmentsService departmentsService)
        {
            _departmentsService = departmentsService;
        }


        // GET: Departments
        public async Task<IActionResult> Index(int? pageNumber)
        {
            return View(await PaginatedList<Department>.CreateAsync(_departmentsService.GetIQueryable(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _departmentsService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentsService.AddAsync(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _departmentsService.GetAsync(id, true);
            if (result == null) return NotFound();
            return View(result);
        }
        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!await _departmentsService.UpdateAsync(department)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _departmentsService.GetAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _departmentsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
