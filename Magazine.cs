using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal class Magazine : LibraryItem
    {
        #region Attributes
        public int IssueNumber { get; set; }
        #endregion

        #region Constructors
        public Magazine(int id, string title, int issueNumber) : base(id, title)
        {
            IssueNumber = issueNumber;
        }
        #endregion

        #region Methods
        public override decimal CalculateLateFee(int daysLate)
        {
            if (daysLate <= 0)
                return 0;

            return daysLate * 0.50m;
        }

        public override int GetLoanPeriod()
        {
            return 7;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Issue Number: {IssueNumber}";
        }
        #endregion
    }
}
