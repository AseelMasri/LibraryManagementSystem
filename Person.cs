using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal class Person
    {
        #region Attributes
        public string Name { get; set; }
        #endregion

        #region Constructors
        protected Person(string name)
        {
            Name = name;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Name: {Name}";
        }
        #endregion


    }
}
