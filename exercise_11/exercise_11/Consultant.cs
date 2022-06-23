using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise_11
{
    internal class Consultant
    {
        internal virtual List<Customer> GetInfo(List<Customer> list)
        {
            foreach (Customer customer in list)
            {
                customer.PassportDetails = customer.PassportDetails != null ? "** *******" : "n/a";
            }

            return list;
        }

        //internal virtual Customer Edit(Customer customer)
        //{

        //}
    }
}
