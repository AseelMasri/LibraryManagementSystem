using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal class Employee : Person, IAuthenticatable
    {
        #region Attributes
        public string Username { get; set; }
        public string Password { get; set; }
        #endregion

        #region Constructors
        public Employee(string name, string username, string password) : base(name)
        {
            Username = username;
            Password = password;
        }
        #endregion

        #region Methods
        public bool CheckPassword(string password)
        {
            return Password == password;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Username: {Username}";
        }
        #endregion
    }
}
