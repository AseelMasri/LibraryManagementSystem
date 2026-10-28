using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal class ResearchPaper : LibraryItem
    {
        #region Attributes
        public string Researcher { get; set; }
        public string ResearchField { get; set; }
        #endregion

        #region Constructors
        public ResearchPaper(int id, string title, string researcher, string researchField) : base(id, title)
        {
            Researcher = researcher;
            ResearchField = researchField;
        }
        #endregion

        #region Methods
        public override decimal CalculateLateFee(int daysLate)
        {
            if (daysLate <= 0)
                return 0;

            return daysLate * 2.00m;
        }

        public override int GetLoanPeriod()
        {
            return 21;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Researcher: {Researcher} | Field: {ResearchField}";
        }
        #endregion
    }
}
