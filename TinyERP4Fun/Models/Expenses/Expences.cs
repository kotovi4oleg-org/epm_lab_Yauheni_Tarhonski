using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Models.Expenses
{
    public class Expences
    {
        public long Id { get; set; }
        
        public string DocumentNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime DocumentDate { get; set; }
        public long DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }
        public long? PersonId { get; set; }
        public Person Person { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public long OurCompanyId { get; set; }
        public Company OurCompany { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
        [DataType(DataType.Date)]
        public DateTime DesiredPaymentDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ApprovedPaymentDate { get; set; }
        //Поле для расширения функционала
        //public DateTime? PaymentDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountOfPayment { get; set; }
        public long CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public bool Approved { get; set; }
        public bool Declined { get; set; }
        public string PurposeOfPayment { get; set; }
        
    }
}
