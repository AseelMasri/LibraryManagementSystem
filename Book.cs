using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal class Book : LibraryItem
    {
        #region Attributes
        public string Author { get; set; }
        public int Pages { get; set; }
        #endregion

        #region Constructors
        public Book(int id, string title, string author, int pages)
            : base(id, title)
        {
            Author = author;
            Pages = pages;
        }
        #endregion

        #region Methods
        public override decimal CalculateLateFee(int daysLate)
        {
            if (daysLate <= 0)
                return 0;

            return daysLate * 1.00m;
        }

        public override int GetLoanPeriod()
        {
            return 14;
        }

        public override string ToString()
        {
            return base.ToString() +
                   $" | Author: {Author} | Pages: {Pages}";
        }
        #endregion
    }
}
