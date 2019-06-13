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
    public class StatesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly ICommonService _commonService;

        public StatesController(DefaultContext context, ICommonService commonService)
        {
            _context = context;
            _commonService = commonService;
        }

        // GET: States
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var defaultContext = _context.State.Include(x => x.Country).OrderBy(x => x.Name);
            return View(await PaginatedList<State>.CreateAsync(defaultContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: States/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var state = await _commonService.GetStateInfo(id);
            if (state == null) return NotFound();
            return View(state);
        }
        public void SetViewBag()
        {
            ViewBag.Countries = CommonFunctions.AddFirstItem(new SelectList(_context.Country, "Id", "Name"));
        }
        // GET: States/Create
        public IActionResult Create()
        {
            SetViewBag();
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CountryId")] State state)
        {
            if (ModelState.IsValid)
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SetViewBag();
            return View(state);
        }

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var state = await _commonService.GetStateInfo(id);
            if (state == null) return NotFound();
            SetViewBag();
            return View(state);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] State state)
        {
            if (id != state.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            SetViewBag();
            return View(state);
        }

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var state = await _commonService.GetStateInfo(id);
            if (state == null) return NotFound();
            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var state = await _context.State.FindAsync(id);
            _context.State.Remove(state);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateExists(long? id)
        {
            return _context.State.Any(e => e.Id == id);
        }
    }
}
