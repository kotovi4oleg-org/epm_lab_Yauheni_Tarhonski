using System;
using System.Collections.Generic;


namespace TinyERP4Fun.Models.Expenses
{
    public class ExpencesFiltredModel
    {
        public PaginatedList<Expences> Expences { get; set; }
        public Expences SingleRecord { get; set; }
        public ICollection<long> CurrencyFilter { get; set; }
        public DateTime? FromFilter { get; set; }
        public DateTime? ToFilter { get; set; }
        public ICollection<long> OurCompanyFilter { get; set; }
        public ICollection<long> CompanyFilter { get; set; }
        public bool ApprovedFilter { get; set; }
        public bool DeclinedFilter { get; set; }
        public bool NotProcessedFilter { get; set; }
        public bool AdmFilter { get; set; }
        public string Total { get; set; }
    }
}
