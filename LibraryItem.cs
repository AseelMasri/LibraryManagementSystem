using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal abstract class LibraryItem
    {
        #region Attributes
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsBorrowed { get; set; }
        public bool IsActive { get; set; }
        #endregion

        #region Constructors
        public LibraryItem(int id, string title)
        {
            Id = id;
            Title = title;
            IsBorrowed = false;
            IsActive = true;
        }
        #endregion

        #region Methods
        public bool IsAvailable()
        {
            return IsActive && !IsBorrowed;
        }

        public void Borrow()
        {
            if (IsAvailable())
            {
                IsBorrowed = true;
            }
        }

        public void Return()
        {
            IsBorrowed = false;
        }

        public void Disable()
        {
            IsActive = false;
        }

        public abstract decimal CalculateLateFee(int daysLate);

        public abstract int GetLoanPeriod();

        public override string ToString()
        {
            string status;

            if (!IsActive)
                status = "Inactive";
            else if (IsBorrowed)
                status = "Borrowed";
            else
                status = "Available";

            return $"ID: {Id} | Title: {Title} | Status: {status}";
        }
        #endregion
    }

}

