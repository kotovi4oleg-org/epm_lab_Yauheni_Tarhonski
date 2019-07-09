using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.ViewModels;


namespace TinyERP4Fun.Controllers
{
    [Authorize(Roles = Constants.rolesExpences_User)]
    public class ExpencesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IExpencesService _expencesService;

        public ExpencesController(UserManager<IdentityUser> userManager, IExpencesService expencesService)
        {
            _userManager = userManager;
            _expencesService = expencesService;
        }
        private async Task<bool> IsAdminOrOwner(Expences entity, bool ownerCheck)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var personRoles = _userManager.GetRolesAsync(currentUser);
            if (personRoles.Result.Intersect(Constants.rolesExpences_Admin.Split(',').Select(x => x.Trim())).Count() != 0) return true;
            if (ownerCheck&&entity!=null) return entity.User == currentUser;
            return false;
        }
        private async Task<ExpencesFiltredModel> IndexCreateViewModel(int? pageNumber, ExpencesFiltredModel expencesViewModel, bool adm)
        {
            //Настройку хранения фильтров не реализовывал. В планах - хранить фильтры в базе для каждого сочетания пользователь/модуль
            var currentUserId = _userManager.GetUserId(User);
            expencesViewModel = await _expencesService.GetFilteredContentAsync(pageNumber, expencesViewModel, currentUserId, adm);
            ViewBag.CurrencyFilter = new SelectList(_expencesService.GetCurrenciesIds(),"Id","Name");
            ViewBag.CompanyFilter = new SelectList(_expencesService.GetCompaniesIds(), "Id", "Name");
            ViewBag.OurCompanyFilter = new SelectList(_expencesService.GetOurCompaniesIds(), "Id", "Name");
            if (!string.IsNullOrEmpty(expencesViewModel.Total))
                expencesViewModel.Total = Localization.currentLocalizatin["Amount of Expenses"] + ": " + expencesViewModel.Total;
            
            return expencesViewModel;
        }

        public async Task<IActionResult> Index(int? pageNumber, ExpencesFiltredModel expencesViewModel)
        {
            if (expencesViewModel.AdmFilter&&!await IsAdminOrOwner(null,false))
                expencesViewModel.AdmFilter = false;
            var model = await IndexCreateViewModel(pageNumber, expencesViewModel, false);
            return View(model);
        }

        // GET: Expences/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await _expencesService.GetAsync(id);
            if (result == null) return NotFound();
            if (!await IsAdminOrOwner(result,true)) return NotFound();
            return View(result);
        }
        private void SetViewData()
        {
            var currentUserId = _userManager.GetUserId(User);
            ViewData["CompanyId"] = new SelectList(_expencesService.GetCompaniesIds(), "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_expencesService.GetCurrenciesIds(), "Id", "Name");
            ViewData["OurCompanyId"] = new SelectList(_expencesService.GetOurCompaniesIds(), "Id", "Name");
            ViewData["DocumentTypeId"] = new SelectList(_expencesService.GetDocumentTypesIds(), "Id", "Name");
            ViewData["PersonId"] = new SelectList(_expencesService.GetPersonsIds(currentUserId), "Id", "Name");
            ViewData["UserId"] = new SelectList(_expencesService.GetUsersIds(currentUserId), "Id", "Name");
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

                expences.UserId = _userManager.GetUserId(User);
                await _expencesService.AddAsync(expences);
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(expences);
        }

        // GET: Expences/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var result = await _expencesService.GetAsync(id, true);
            if (result == null) return NotFound();
            if (!await IsAdminOrOwner(result, true)) return NotFound();
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
                if(!await _expencesService.UpdateAsync(expences)) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            SetViewData();
            return View(expences);
        }

        // GET: Expences/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var result = await _expencesService.GetAsync(id);
            if (result == null) return NotFound();
            if (!await IsAdminOrOwner(result, true)) return NotFound();
            return View(result);
        }

        // POST: Expences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _expencesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
