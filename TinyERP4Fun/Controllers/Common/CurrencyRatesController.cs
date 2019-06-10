using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Controllers
{
    public class CurrencyRatesController : Controller
    {
        private readonly DefaultContext _context;

        public CurrencyRatesController(DefaultContext context)
        {
            _context = context;
        }

        [DataContract]
        class NBRBCur
        {
            [DataMember]
            // Microsoft.CodeAnalysis.CSharp.Workspaces ругается варнингами на непроинициализированные поля, поэтому.... 
            // проинициализирую их явно, хоть и неявное поведение мне было понятно и оно меня устраивало.
            public int Cur_ID = default;
            [DataMember]
            public string Cur_Abbreviation = default;
            [DataMember]
            public int Cur_Scale = default;
            [DataMember]
            public string Cur_DateStart = default;
            [DataMember]
            public string Cur_DateEnd = default;
        }
        [DataContract]
        class NBRBRate
        {
            [DataMember]
            public int Cur_Scale = default;
            [DataMember]
            public double Cur_OfficialRate = default;
        }

        private static async Task<string> SendGetRequestAsync(Uri url)
        {
            var content = new MemoryStream();
            try //http
            {
                var webReq = WebRequest.Create(url);
                Task<WebResponse> responseTask = webReq.GetResponseAsync();
                using (WebResponse response = await responseTask)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        await responseStream.CopyToAsync(content);
                    }
                }
            }
            catch //https
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var webReq = WebRequest.Create(url);
                Task<WebResponse> responseTask = webReq.GetResponseAsync();
                using (WebResponse response = await responseTask)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        await responseStream.CopyToAsync(content);
                    }
                }
            }
            return Encoding.ASCII.GetString(content.ToArray());
        }

        internal async Task UpdateBYNVoid()
        {
            Currency baseCurrency = _context.Currency.Where(x => x.Code == Constants.BYNCode).FirstOrDefault();
            var currencyList = _context.Currency.Where(x => x.Code != Constants.BYNCode && x.Active);
            string jsonCurList = await SendGetRequestAsync(new Uri(Constants.BYN_CURRENCY_LIST_URL));
            DataContractJsonSerializer jsonFormatterNBRBCurAr = new DataContractJsonSerializer(typeof(NBRBCur[]));
            DataContractJsonSerializer jsonFormatterNBRBRate = new DataContractJsonSerializer(typeof(NBRBRate));
            NBRBCur[] curArray = (NBRBCur[])jsonFormatterNBRBCurAr.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonCurList)));

            //var currencyDate = DateTime.Today;
            foreach (var currency in currencyList)
            {
                for (var currencyDate = Constants.baseDate; currencyDate <= DateTime.Today; currencyDate = currencyDate.AddDays(1))
                {

                    if (_context.CurrencyRates.Where(x => x.DateRate == currencyDate && x.Currency.Code == currency.Code).FirstOrDefault() != null)
                        continue;

                    var curNBRB = curArray.FirstOrDefault(x => x.Cur_Abbreviation == currency.Code &&
                                                               DateTime.Parse(x.Cur_DateStart) <= currencyDate &&
                                                               DateTime.Parse(x.Cur_DateEnd) >= currencyDate
                                                               );

                    if (curNBRB == null)
                        continue;

                    string urlRate = Constants.BYN_CURRENCY_RATE_URL + curNBRB.Cur_ID + Constants.BYN_ATTR_DATE + currencyDate;
                    string jsonRate = await SendGetRequestAsync(new Uri(urlRate));
                    NBRBRate curRate = jsonFormatterNBRBRate.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonRate))) as NBRBRate;
                    CurrencyRates currencyRate = new CurrencyRates
                    {
                        BaseCurrencyId = baseCurrency.Id,
                        CurrencyId = currency.Id,
                        DateRate = currencyDate,
                        CurrecyScale = curRate.Cur_Scale,
                        CurrencyRate = (decimal)curRate.Cur_OfficialRate
                    };
                    _context.Add(currencyRate);
                }
            }
            await _context.SaveChangesAsync();
        }

        [Authorize(Roles = Constants.rolesCommon_Admin)]
        public async Task<IActionResult> UpdateBYN()
        {
            await UpdateBYNVoid();
            return RedirectToAction(nameof(Index));

        }

        // GET: CurrencyRates
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var defaultContext = _context.CurrencyRates.Include(c => c.BaseCurrency)
                                                       .Include(c => c.Currency)
                                                       .OrderByDescending(x => x.DateRate)
                                                       .ThenBy(x => x.Currency.Name);
            return View(await PaginatedList<CurrencyRates>.CreateAsync(defaultContext.AsNoTracking(), pageNumber ?? 1, Constants.pageSize));
        }

        // GET: CurrencyRates/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyRates = await _context.CurrencyRates
                .Include(c => c.BaseCurrency)
                .Include(c => c.Currency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currencyRates == null)
            {
                return NotFound();
            }

            return View(currencyRates);
        }
        [Authorize(Roles = Constants.adminRoleName)]
        // GET: CurrencyRates/Create
        public IActionResult Create()
        {
            ViewData["BaseCurrencyId"] = new SelectList(_context.Currency, "Id", "Code");
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code");
            return View();
        }
        [Authorize(Roles = Constants.adminRoleName)]
        // POST: CurrencyRates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CurrecyScale,CurrencyRate,DateRate,CurrencyId,BaseCurrencyId")] CurrencyRates currencyRates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currencyRates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BaseCurrencyId"] = new SelectList(_context.Currency, "Id", "Code", currencyRates.BaseCurrencyId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code", currencyRates.CurrencyId);
            return View(currencyRates);
        }
        [Authorize(Roles = Constants.adminRoleName)]
        // GET: CurrencyRates/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyRates = await _context.CurrencyRates.FindAsync(id);
            if (currencyRates == null)
            {
                return NotFound();
            }
            ViewData["BaseCurrencyId"] = new SelectList(_context.Currency, "Id", "Id", currencyRates.BaseCurrencyId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Id", currencyRates.CurrencyId);
            return View(currencyRates);
        }
        [Authorize(Roles = Constants.adminRoleName)]
        // POST: CurrencyRates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CurrecyScale,CurrencyRate,DateRate,CurrencyId,BaseCurrencyId")] CurrencyRates currencyRates)
        {
            if (id != currencyRates.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currencyRates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyRatesExists(currencyRates.Id))
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
            ViewData["BaseCurrencyId"] = new SelectList(_context.Currency, "Id", "Code", currencyRates.BaseCurrencyId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code", currencyRates.CurrencyId);
            return View(currencyRates);
        }
        [Authorize(Roles = Constants.adminRoleName)]
        // GET: CurrencyRates/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyRates = await _context.CurrencyRates
                .Include(c => c.BaseCurrency)
                .Include(c => c.Currency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currencyRates == null)
            {
                return NotFound();
            }

            return View(currencyRates);
        }
        [Authorize(Roles = Constants.adminRoleName)]
        // POST: CurrencyRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var currencyRates = await _context.CurrencyRates.FindAsync(id);
            _context.CurrencyRates.Remove(currencyRates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyRatesExists(long id)
        {
            return _context.CurrencyRates.Any(e => e.Id == id);
        }
    }
}
