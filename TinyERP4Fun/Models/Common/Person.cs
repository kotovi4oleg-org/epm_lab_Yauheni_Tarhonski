using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun.Models.Common
{
    public class Person : IHaveName, IHaveLongId, IHaveUser
    {
        public long Id { get; set; }
        public string Name { get; private set; }
        public string FullName { get; private set; }
        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                FullName = string.Join(' ', new[] { firstName, lastName });
                Name = FullName;
            }
        }
        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                FullName = string.Join(' ', new[] { firstName, lastName });
                Name = FullName;
            }
        }
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
        //[Index(IsClustered = true, IsUnique = false)]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        //Пока с этим списком не заморачиваюсь
        public List<Communication> Communications { get; set; }
        public bool IsEmployee { get; set; }
        public long? CompanyId { get; set; }
        public Company Company { get; set; }
        public bool IsChief { get; set; }
    }


}
