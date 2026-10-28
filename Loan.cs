using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal class Loan
    {
        #region Attributes
        public int Id { get; set; }
        public Member Member { get; set; }
        public LibraryItem Item { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int DaysLate { get; set; }
        public decimal FineAmount { get; set; }
        public decimal PaidAmount { get; set; }
        #endregion

        #region Constructors
        public Loan(int id, Member member, LibraryItem item)
        {
            Id = id;
            Member = member;
            Item = item;
            BorrowDate = DateTime.Now;
            DueDate = BorrowDate.AddDays(item.GetLoanPeriod());
            // For Fine Testing
            // BorrowDate = DateTime.Now.AddDays(-10);
            // DueDate = BorrowDate.AddDays(5);
            ReturnDate = null;
            DaysLate = 0;
            FineAmount = 0;
            PaidAmount = 0;
        }
        #endregion

        #region Methods
        public bool IsReturned()
        {
            return ReturnDate != null;
        }

        public decimal GetOutstandingFine()
        {
            return Math.Max(0, FineAmount - PaidAmount);
        }

        public void ReturnItem()
        {
            if (IsReturned())
                return;

            ReturnDate = DateTime.Now;

            DaysLate = Math.Max(0, (ReturnDate.Value.Date - DueDate.Date).Days);

            FineAmount = Item.CalculateLateFee(DaysLate);

            Item.Return();
        }

        public override string ToString()
        {
            string status = IsReturned() ? "Returned" : "Borrowed";

            return $"Loan ID: {Id} | Member: {Member.Name} | " +
                   $"Item: {Item.Title} | Borrow Date: {BorrowDate:dd/MM/yyyy} | " +
                   $"Due Date: {DueDate:dd/MM/yyyy} | Status: {status} | " +
                   $"Fine: {FineAmount} | Paid: {PaidAmount}";
        }
        #endregion
    }
}
