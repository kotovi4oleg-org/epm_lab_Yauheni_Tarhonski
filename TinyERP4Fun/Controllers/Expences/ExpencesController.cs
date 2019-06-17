using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ModelServiceInterfaces;
using TinyERP4Fun.ViewModels;

namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesExpences_User)]
    public class ExpencesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IExpencesService _expencesService;

        public ExpencesController(DefaultContext context, UserManager<IdentityUser> userManager, IExpencesService expencesService)
        {
            _context = context;
            _userManager = userManager;
            _expencesService = expencesService;
        }
        private bool IsAdminOrOwner(Expences entity, bool ownerCheck)
        {
            var currentUser = _userManager.GetUserAsync(User);
            Task.WaitAll(currentUser);
            var personRoles = _userManager.GetRolesAsync(currentUser.Result);
            if (personRoles.Result.Intersect(Constants.rolesExpences_Admin.Split(',').Select(x => x.Trim())).Count() != 0) return true;
            if (ownerCheck&&entity!=null) return entity.User == currentUser.Result;
            return false;
        }
        private async Task<ExpencesViewModel> IndexCreateViewModel(int? pageNumber, ExpencesViewModel expencesViewModel, bool adm)
        {
            //Настройку хранения фильтров не реализовывал. В планах - хранить фильтры в базе для каждого сочетания пользователь/модуль
            var currentUserId = _userManager.GetUserId(User);
            expencesViewModel = await _expencesService.GetFilteredExpences(pageNumber, expencesViewModel, currentUserId, adm);
            ViewBag.CurrencyFilter = new SelectList(_context.Currency.Where(x=>x.Active), "Id", "Name");
            ViewBag.CompanyFilter = new SelectList(_context.Expences.Select(x => x.Company).Distinct(), "Id", "Name");
            ViewBag.OurCompanyFilter = new SelectList(_context.Company.Where(x => x.OurCompany), "Id", "Name");
            return expencesViewModel;
        }

        public async Task<IActionResult> Index(int? pageNumber, ExpencesViewModel expencesViewModel)
        {
            if (expencesViewModel.AdmFilter&&!IsAdminOrOwner(null,false))
                expencesViewModel.AdmFilter = false;
            var model = await IndexCreateViewModel(pageNumber, expencesViewModel, false);
            return View(model);
        }

        // GET: Expences/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _expencesService.GetExpenceInfo(id);
            if (result == null) return NotFound();
            if (!IsAdminOrOwner(result,true)) return NotFound();
            return View(result);
        }
        private void SetViewData()
        {
            var currentUserId = _userManager.GetUserId(User);
            ViewData["DocumentTypeId"] = new SelectList(_context.DocumentType, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currency.Where(x => x.Active), "Id", "Name");
            ViewData["OurCompanyId"] = new SelectList(_context.Company.Where(x => x.OurCompany), "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.Person.Where(x => x.UserId == currentUserId && x.UserId != null), "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users.Where(x => x.Id == currentUserId), "Id", "Email");
        }
        // GET: Expences/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Expences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DocumentNumber,DocumentDate,DocumentTypeId,PersonId,UserId,OurCompanyId,CompanyId,DesiredPaymentDate,ApprovedPaymentDate,AmountOfPayment,CurrencyId,Approved,Declined,PurposeOfPayment")] Expences expences)
        {
            if (ModelState.IsValid)
            {
                #region можно взять юзера из куков. но не буду (предпочту лишний раз обратиться в базу, возможно зря). А делается это так
                // Получаем user ID не делая дополнительную вылазку в базу
                /*
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var userId = claim.Value;
                 
                // Но можно выкинуть этот код в отдельный класс
                
                public static class UserHelpers
                {
                    public static string GetUserId(this IPrincipal principal)
                    {
                        var claimsIdentity = (ClaimsIdentity)principal.Identity;
                        var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                        return claim?.Value;
                    }
                }
                
                // И обращаться так: var userId = this.User.GetUserId();
                /**/
                #endregion

                //Повторно считали Id пользователя 
                expences.UserId = _userManager.GetUserId(User);
                _context.Add(expences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(expences);
        }

        // GET: Expences/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _expencesService.GetExpenceInfo(id);
            if (result == null) return NotFound();
            if (!IsAdminOrOwner(result, true)) return NotFound();
            SetViewData();
            return View(result);
        }

        // POST: Expences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DocumentNumber,DocumentDate,DocumentTypeId,PersonId,UserId,OurCompanyId,CompanyId,DesiredPaymentDate,AmountOfPayment,CurrencyId,PurposeOfPayment")] Expences expences)
        {
            if (id != expences.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpencesExists(expences.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(expences);
        }

        // GET: Expences/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _expencesService.GetExpenceInfo(id);
            if (result == null) return NotFound();
            if (!IsAdminOrOwner(result, true)) return NotFound();
            return View(result);
        }

        // POST: Expences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var expences = await _context.Expences.FindAsync(id);
            _context.Expences.Remove(expences);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

         private bool ExpencesExists(long id)
        {
            return _context.Expences.Any(e => e.Id == id);
        }
    }
}
