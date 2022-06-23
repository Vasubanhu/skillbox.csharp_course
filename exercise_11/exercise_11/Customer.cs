using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise_11
{
    internal class Customer
    {
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _phoneNumber;
        private string? _passportDetails;

        public Customer(string firstName, string middleName, string lastName, string phoneNumber, string passportDetails)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            PassportDetails = passportDetails;
        }

        public string LastName { get => _lastName; set => _lastName = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string MiddleName { get => _middleName; set => _middleName = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string PassportDetails { get => _passportDetails; set => _passportDetails = value; }
    }
}
