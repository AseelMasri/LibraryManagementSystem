using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal class LibraryReports
    {
        #region Attributes

        private Library library;

        #endregion

        #region Constructors

        public LibraryReports(Library library)
        {
            this.library = library;
        }

        #endregion

        #region Materials Reports

        
        public List<LibraryItem> GetAvailableItems()
        {
            List<LibraryItem> result = new List<LibraryItem>();

            foreach (LibraryItem item in library.GetAllItems())
            {
                if (item.IsAvailable())
                {
                    result.Add(item);
                }
            }

            return result;
        }

        
        public List<LibraryItem> GetBorrowedItems()
        {
            List<LibraryItem> result = new List<LibraryItem>();

            foreach (LibraryItem item in library.GetAllItems())
            {
                if (item.IsBorrowed)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public LibraryItem? GetMostBorrowedItem()
        {
            LibraryItem? mostBorrowedItem = null;
            int highestCount = 0;

            List<Loan> allLoans = library.GetAllLoans();

            foreach (LibraryItem item in library.GetAllItems())
            {
                int count = 0;

                foreach (Loan loan in allLoans)
                {
                    if (loan.Item.Id == item.Id)
                    {
                        count++;
                    }
                }

                if (count > highestCount)
                {
                    highestCount = count;
                    mostBorrowedItem = item;
                }
            }

            return mostBorrowedItem;
        }

        #endregion

        #region Members Reports

        public List<Member> GetOverdueMembers()
        {
            List<Member> result = new List<Member>();

            foreach (Loan loan in library.GetAllLoans())
            {
                if (!loan.IsReturned() &&
                    loan.DueDate.Date < DateTime.Today)
                {
                    bool alreadyAdded = false;

                    foreach (Member member in result)
                    {
                        if (member.MembershipNumber ==
                            loan.Member.MembershipNumber)
                        {
                            alreadyAdded = true;
                            break;
                        }
                    }

                    if (!alreadyAdded)
                    {
                        result.Add(loan.Member);
                    }
                }
            }

            return result;
        }

        public List<Member> GetMembersWithOutstandingFines()
        {
            List<Member> result = new List<Member>();

            foreach (Member member in library.GetAllMembers())
            {
                if (library.GetMemberOutstandingFines(
                    member.MembershipNumber) > 0)
                {
                    result.Add(member);
                }
            }

            return result;
        }

        public List<Loan> GetMemberOperations(int membershipNumber)
        {
            return library.GetMemberLoans(membershipNumber);
        }

        #endregion

        #region Fine Reports

        public decimal GetTotalFinesCollected()
        {
            return library.GetTotalFinesCollected();
        }

        public decimal GetMemberTotalFines(int membershipNumber)
        {
            return library.GetMemberTotalFines(membershipNumber);
        }

        public decimal GetMemberTotalPaid(int membershipNumber)
        {
            return library.GetMemberTotalPaid(membershipNumber);
        }

        public decimal GetMemberOutstandingFines(int membershipNumber)
        {
            return library.GetMemberOutstandingFines(membershipNumber);
        }

        #endregion

    }
}
