using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliasAnyType
{
    public class Employee
    {
        // Field
        private string Name;

        // Constructor
        public Employee(string name)
        {
            Name = name;
        }

        // Method to get the name of the employee
        public string GetName()
        {
            return Name;
        }
    }
}
