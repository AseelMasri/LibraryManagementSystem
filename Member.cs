using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal class Member : Person, IAuthenticatable
    {
        #region Attributes
        public int MembershipNumber { get; set; }
        public string Password { get; set; }
        public int MaxBorrowLimit { get; set; }
        public bool IsActive { get; set; }
        #endregion

        #region Constructors
        public Member(int membershipNumber, string name, string password) : base(name)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(
                    "Password cannot be empty.", nameof(password));
            }

            MembershipNumber = membershipNumber;
            Password = password;
            MaxBorrowLimit = 3;
            IsActive = true;
        }
        #endregion

        #region Methods
        public bool CheckPassword(string password)
        {
            return Password == password;
        }

        public void SetBorrowLimit(int limit)
        {
            if (limit > 0)
            {
                MaxBorrowLimit = limit;
            }
        }

        public void Disable()
        {
            IsActive = false;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Membership Number: {MembershipNumber} | Active: {IsActive}";
        }
        #endregion

    }
}
