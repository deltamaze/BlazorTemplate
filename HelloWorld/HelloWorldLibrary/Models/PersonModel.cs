using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorldLibrary.Models
{
    public class PersonModel
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double HeightInInches { get; set; }

        public string FullName
        {
            get
            {
                return $"{ FirstName } { LastName }";
            }
        }
    }
}
