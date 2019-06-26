using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Models.Common
{
    public class Person : IHaveName, IHaveLongId, IHaveUser
    {
        public long Id { get; set; }
        public string Name { get; private set; }
        private string fullName;
        public string FullName
        {
            get
            {
                return fullName;
            }
            private set
            {
                fullName = value?.Trim();
                Name = FullName;
            }
        }

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value?.Trim();
                FullName = CombineNames(firstName, lastName);
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
                lastName = value?.Trim();
                FullName = CombineNames(firstName, lastName);
            }
        }
        private string CombineNames(string _firstName, string _lastName)
        {
            return string.Join(' ', new[] { _firstName, _lastName });
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
