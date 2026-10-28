using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal class FinePayment
    {
        #region Attributes
        public int BatchNumber { get; set; }
        public Member Member { get; set; }
        public Loan Loan { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        #endregion

        #region Constructors
        public FinePayment(int batchNumber, Member member, Loan loan, decimal amount)
        {
            BatchNumber = batchNumber;
            Member = member;
            Loan = loan;
            Amount = amount;
            PaymentDate = DateTime.Now;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Payment Batch: {BatchNumber} | Member: {Member.Name} | " +
                   $"Loan ID: {Loan.Id} | Amount: {Amount} | " +
                   $"Payment Date: {PaymentDate:dd/MM/yyyy}";
        }
        #endregion
    }
}
