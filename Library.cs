using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem
{
    internal class Library
    {
        #region Attributes

        private List<LibraryItem> items;
        private List<Member> members;
        private List<Employee> employees;
        private List<Loan> loans;
        private List<FinePayment> payments;

        private int nextItemId;
        private int nextMemberId;
        private int nextLoanId;

        #endregion

        #region Constructors

        public Library()
        {
            items = new List<LibraryItem>();
            members = new List<Member>();
            employees = new List<Employee>();
            loans = new List<Loan>();
            payments = new List<FinePayment>();

            nextItemId = 1;
            nextMemberId = 1;
            nextLoanId = 1;

            employees.Add(
                new Employee("Library Admin", "admin", "admin123"));

            AddItem(new Book(
                0, "Clean Code", "Robert C. Martin", 464));

            AddItem(new Magazine(
                0, "Science Today", 1));

            AddItem(new ResearchPaper(
                0,
                "Artificial Intelligence",
                "John Smith",
                "Computer Science"));
        }

        #endregion

        #region Login

        public Employee? LoginEmployee(string username, string password)
        {
            foreach (Employee employee in employees)
            {
                if (employee.Username == username &&
                    employee.CheckPassword(password))
                {
                    return employee;
                }
            }

            return null;
        }

        public Member? LoginMember(
            int membershipNumber,
            string password)
        {
            foreach (Member member in members)
            {
                if (member.MembershipNumber == membershipNumber &&
                    member.CheckPassword(password) &&
                    member.IsActive)
                {
                    return member;
                }
            }

            return null;
        }

        #endregion

        #region Members

        public Member RegisterMember(string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Member name cannot be empty.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(
                    "Password cannot be empty.", nameof(password));
            }

            Member member = new Member(
                nextMemberId++,
                name.Trim(),
                password);

            members.Add(member);

            return member;
        }

        public List<Member> GetAllMembers()
        {
            return new List<Member>(members);
        }

        public Member? FindMember(int membershipNumber)
        {
            foreach (Member member in members)
            {
                if (member.MembershipNumber == membershipNumber)
                {
                    return member;
                }
            }

            return null;
        }

        public List<Member> SearchMembers(int membershipNumber)
        {
            List<Member> results = new List<Member>();

            Member? member = FindMember(membershipNumber);

            if (member != null)
            {
                results.Add(member);
            }

            return results;
        }

        public bool DisableMember(int membershipNumber)
        {
            Member? member = FindMember(membershipNumber);

            if (member == null || !member.IsActive)
            {
                return false;
            }

            member.Disable();

            return true;
        }

        public bool SetMemberBorrowLimit(
            int membershipNumber,
            int limit)
        {
            Member? member = FindMember(membershipNumber);

            if (member == null || limit <= 0)
            {
                return false;
            }

            member.SetBorrowLimit(limit);

            return true;
        }

        #endregion

        #region Library Items

        public void AddItem(LibraryItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (string.IsNullOrWhiteSpace(item.Title))
            {
                throw new ArgumentException(
                    "Item title cannot be empty.", nameof(item));
            }

            if (item is Book book)
            {
                if (string.IsNullOrWhiteSpace(book.Author) ||
                    book.Pages <= 0)
                {
                    throw new ArgumentException(
                        "Book author and page count must be valid.");
                }

                items.Add(new Book(
                    nextItemId++,
                    book.Title.Trim(),
                    book.Author.Trim(),
                    book.Pages));
            }
            else if (item is Magazine magazine)
            {
                if (magazine.IssueNumber <= 0)
                {
                    throw new ArgumentException(
                        "Magazine issue number must be positive.");
                }

                items.Add(new Magazine(
                    nextItemId++,
                    magazine.Title.Trim(),
                    magazine.IssueNumber));
            }
            else if (item is ResearchPaper paper)
            {
                if (string.IsNullOrWhiteSpace(paper.Researcher) ||
                    string.IsNullOrWhiteSpace(paper.ResearchField))
                {
                    throw new ArgumentException(
                        "Researcher and research field cannot be empty.");
                }

                items.Add(new ResearchPaper(
                    nextItemId++,
                    paper.Title.Trim(),
                    paper.Researcher.Trim(),
                    paper.ResearchField.Trim()));
            }
            else
            {
                throw new ArgumentException(
                    "Unsupported library item type.", nameof(item));
            }
        }

        public List<LibraryItem> GetAllItems()
        {
            return new List<LibraryItem>(items);
        }

        public List<LibraryItem> GetAvailableItems()
        {
            List<LibraryItem> availableItems = new List<LibraryItem>();

            foreach (LibraryItem item in items)
            {
                if (item.IsAvailable())
                {
                    availableItems.Add(item);
                }
            }

            return availableItems;
        }

        public LibraryItem? FindItem(int id)
        {
            foreach (LibraryItem item in items)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public List<LibraryItem> SearchItems(string searchTerm)
        {
            List<LibraryItem> results = new List<LibraryItem>();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return results;
            }

            searchTerm = searchTerm.Trim();

            foreach (LibraryItem item in items)
            {
                if (item.IsActive &&
                    item.Title.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(item);
                }
            }

            return results;
        }

        public List<LibraryItem> SearchItems(int itemId)
        {
            List<LibraryItem> results = new List<LibraryItem>();

            LibraryItem? item = FindItem(itemId);

            if (item != null && item.IsActive)
            {
                results.Add(item);
            }

            return results;
        }

        public LibraryItem? GetItemDetails(int id)
        {
            return FindItem(id);
        }

        public bool EditItem(
            int id,
            string newTitle,
            string? author = null,
            int? pages = null,
            int? issueNumber = null,
            string? researcher = null,
            string? researchField = null)
        {
            LibraryItem? item = FindItem(id);

            if (item == null ||
                !item.IsActive ||
                string.IsNullOrWhiteSpace(newTitle))
            {
                return false;
            }

            newTitle = newTitle.Trim();

            if (item is Book book)
            {
                if (string.IsNullOrWhiteSpace(author) ||
                    pages == null ||
                    pages <= 0)
                {
                    return false;
                }

                book.Title = newTitle;
                book.Author = author.Trim();
                book.Pages = pages.Value;

                return true;
            }

            if (item is Magazine magazine)
            {
                if (issueNumber == null || issueNumber <= 0)
                {
                    return false;
                }

                magazine.Title = newTitle;
                magazine.IssueNumber = issueNumber.Value;

                return true;
            }

            if (item is ResearchPaper paper)
            {
                if (string.IsNullOrWhiteSpace(researcher) ||
                    string.IsNullOrWhiteSpace(researchField))
                {
                    return false;
                }

                paper.Title = newTitle;
                paper.Researcher = researcher.Trim();
                paper.ResearchField = researchField.Trim();

                return true;
            }

            return false;
        }

        public bool EditItemTitle(int id, string newTitle)
        {
            LibraryItem? item = FindItem(id);

            if (item == null ||
                !item.IsActive ||
                string.IsNullOrWhiteSpace(newTitle))
            {
                return false;
            }

            item.Title = newTitle.Trim();

            return true;
        }

        public bool DisableItem(int id)
        {
            LibraryItem? item = FindItem(id);

            if (item == null ||
                item.IsBorrowed ||
                !item.IsActive)
            {
                return false;
            }

            item.Disable();

            return true;
        }

        #endregion

        #region Borrowing and Returning

        public bool BorrowItem(int membershipNumber, int itemId)
        {
            Member? member = FindMember(membershipNumber);
            LibraryItem? item = FindItem(itemId);

            if (member == null ||
                !member.IsActive ||
                item == null ||
                !item.IsAvailable())
            {
                return false;
            }

            if (GetMemberOutstandingFines(membershipNumber) > 0)
            {
                return false;
            }

            int currentLoans = 0;

            foreach (Loan loan in loans)
            {
                if (loan.Member.MembershipNumber == membershipNumber &&
                    !loan.IsReturned())
                {
                    currentLoans++;
                }

                if (loan.Item.Id == itemId && !loan.IsReturned())
                {
                    return false;
                }
            }

            if (currentLoans >= member.MaxBorrowLimit)
            {
                return false;
            }

            item.Borrow();

            Loan newLoan = new Loan(
                nextLoanId++,
                member,
                item);

            loans.Add(newLoan);

            return true;
        }

        public bool ReturnItem(int membershipNumber, int loanId)
        {
            foreach (Loan loan in loans)
            {
                if (loan.Id == loanId &&
                    loan.Member.MembershipNumber == membershipNumber &&
                    !loan.IsReturned())
                {
                    loan.ReturnItem();
                    return true;
                }
            }

            return false;
        }

        public List<Loan> GetMemberLoans(int membershipNumber)
        {
            List<Loan> result = new List<Loan>();

            foreach (Loan loan in loans)
            {
                if (loan.Member.MembershipNumber == membershipNumber)
                {
                    result.Add(loan);
                }
            }

            return result;
        }

        public List<Loan> GetAllLoans()
        {
            return new List<Loan>(loans);
        }

        #endregion

        #region Fine Payments

        public bool PayFine(
            int membershipNumber,
            int loanId,
            decimal amount,
            int batchNumber)
        {
            if (batchNumber <= 0 || IsBatchNumberExists(batchNumber))
            {
                return false;
            }

            Loan? selectedLoan = null;

            foreach (Loan loan in loans)
            {
                if (loan.Id == loanId &&
                    loan.Member.MembershipNumber == membershipNumber)
                {
                    selectedLoan = loan;
                    break;
                }
            }

            if (selectedLoan == null ||
                !selectedLoan.IsReturned() ||
                amount <= 0 ||
                amount > selectedLoan.GetOutstandingFine())
            {
                return false;
            }

            FinePayment payment = new FinePayment(
                batchNumber,
                selectedLoan.Member,
                selectedLoan,
                amount);

            selectedLoan.PaidAmount += amount;
            payments.Add(payment);

            return true;
        }

        private bool IsBatchNumberExists(int batchNumber)
        {
            foreach (FinePayment payment in payments)
            {
                if (payment.BatchNumber == batchNumber)
                {
                    return true;
                }
            }

            return false;
        }

        public List<FinePayment> GetAllPayments()
        {
            return new List<FinePayment>(payments);
        }

        public decimal GetMemberTotalFines(int membershipNumber)
        {
            decimal total = 0;

            foreach (Loan loan in loans)
            {
                if (loan.Member.MembershipNumber == membershipNumber)
                {
                    total += loan.FineAmount;
                }
            }

            return total;
        }

        public decimal GetMemberTotalPaid(int membershipNumber)
        {
            decimal total = 0;

            foreach (FinePayment payment in payments)
            {
                if (payment.Member.MembershipNumber == membershipNumber)
                {
                    total += payment.Amount;
                }
            }

            return total;
        }

        public decimal GetMemberOutstandingFines(int membershipNumber)
        {
            decimal total = 0;

            foreach (Loan loan in loans)
            {
                if (loan.Member.MembershipNumber == membershipNumber)
                {
                    total += loan.GetOutstandingFine();
                }
            }

            return total;
        }

        public decimal GetTotalFinesCollected()
        {
            decimal total = 0;

            foreach (FinePayment payment in payments)
            {
                total += payment.Amount;
            }

            return total;
        }

        #endregion
    }
}
