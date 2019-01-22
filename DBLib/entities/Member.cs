using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.entities
{
    public enum UserCategory
    {
        Student,
        EmployedFullTime,
        EmployedPartTime,
        Unemployed
    }

    public class Member
    {
        public string Name { get; set; }
        public string email { get; set; }
        public string PhoneNumber { get; set; }
        public UserCategory userCategory { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public string DateOfBirth { get; set; }
        public string Password { get; set; }
        public DateTime DateOfRegistration { get; private set; }

        public Member(string name, string email, string phoneNumber, UserCategory uC, DateTime doa, string password, DateTime dor) {
            Name = name;
            this.email = email;
            PhoneNumber = phoneNumber;
            userCategory = uC;
            DateOfBirth = doa;
            Password = password;
            DateOfRegistration = dor;
        }
    }
}
