using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Models.Expenses;

namespace TinyERP4Fun.ViewModels
{
    public class ExpencesViewModel
    {
        public PaginatedList<Expences> Expences { get; set; }
        public SelectList CurrencyFilter { get; set; }
        public DateTime? FromFilter { get; set; }
        public DateTime? ToFilter { get; set; }
        public SelectList OurCompanyFilter { get; set; }
        public SelectList CompanyFilter { get; set; }
        public bool ApprovedFilter { get; set; }
        public bool DeclinedFilter { get; set; }
        public bool NotProcessedFilter { get; set; }
        public string Total { get; set; }

    }
}
