using System;
using System.Collections.Generic;
namespace LibraryManagementSystem
{
    internal class Program
    {
        #region Attributes

        static Library library = new Library();
        static LibraryReports reports = new LibraryReports(library);

        #endregion

        #region Main

        static void Main(string[] args)
        {
            int choice = 0;

            do
            {
                Console.WriteLine("\n===== Library Management System =====");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register New Member");
                Console.WriteLine("3. Browse Library");
                Console.WriteLine("4. Search for Items");
                Console.WriteLine("5. Exit");
                Console.Write("Choose: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Login();
                        break;

                    case 2:
                        RegisterMember();
                        break;

                    case 3:
                        BrowseLibrary();
                        break;

                    case 4:
                        SearchItems();
                        break;

                    case 5:
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

            } while (choice != 5);
        }

        #endregion

        #region Login

        static void Login()
        {
            int choice = 0;

            do
            {
                Console.WriteLine("\n===== Login =====");
                Console.WriteLine("1. Employee Login");
                Console.WriteLine("2. Member Login");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Choose: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        EmployeeLogin();
                        break;

                    case 2:
                        MemberLogin();
                        break;

                    case 3:
                        Console.WriteLine("Returning to Main Menu...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

            } while (choice != 3);
        }

        static void EmployeeLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            Employee? employee = library.LoginEmployee(username, password);

            if (employee == null)
            {
                Console.WriteLine("Invalid username or password.");
                return;
            }

            Console.WriteLine($"Welcome, {employee.Name}!");
            AdminMenu();
        }

        static void MemberLogin()
        {
            Console.Write("Membership Number: ");

            if (!int.TryParse(Console.ReadLine(), out int membershipNumber))
            {
                Console.WriteLine("Invalid membership number.");
                return;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            Member? member = library.LoginMember(membershipNumber, password);

            if (member == null)
            {
                Console.WriteLine("Invalid login or inactive membership.");
                return;
            }

            Console.WriteLine($"Welcome, {member.Name}!");
            MemberMenu(member);
        }

        #endregion

        #region Register Member

        static void RegisterMember()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Name and password are required.");
                return;
            }

            Member member = library.RegisterMember(name, password);

            Console.WriteLine("Member registered successfully.");
            Console.WriteLine(
                $"Your Membership Number is: {member.MembershipNumber}");
        }

        #endregion

        #region Browse and Search

        static void BrowseLibrary()
        {
            List<LibraryItem> items = library.GetAllItems();

            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                return;
            }

            Console.WriteLine("\n===== Library Items =====");

            foreach (LibraryItem item in items)
            {
                Console.WriteLine(item);
            }
        }

        static void SearchItems()
        {
            Console.WriteLine("\n===== Search Items =====");
            Console.WriteLine("1. Search by Title");
            Console.WriteLine("2. Search by Item ID");
            Console.Write("Choose search method: ");

            if (!int.TryParse(Console.ReadLine(), out int searchChoice))
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            List<LibraryItem> results;

            switch (searchChoice)
            {
                case 1:
                    Console.Write("Enter item title: ");
                    string title = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.WriteLine("Please enter a title.");
                        return;
                    }

                    results = library.SearchItems(title);
                    break;

                case 2:
                    Console.Write("Enter Item ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int itemId))
                    {
                        Console.WriteLine("Invalid item ID.");
                        return;
                    }

                    results = library.SearchItems(itemId);
                    break;

                default:
                    Console.WriteLine("Invalid search method.");
                    return;
            }

            if (results.Count == 0)
            {
                Console.WriteLine("No matching items found.");
                return;
            }

            Console.WriteLine("\n===== Search Results =====");

            foreach (LibraryItem item in results)
            {
                Console.WriteLine(item);
            }
        }

        #endregion

        #region Employee Menu

        static void AdminMenu()
        {
            int choice = 0;

            do
            {
                Console.WriteLine("\n===== Employee Menu =====");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Add Magazine");
                Console.WriteLine("3. Add Research Paper");
                Console.WriteLine("4. View All Items");
                Console.WriteLine("5. Search Items");
                Console.WriteLine("6. View Item Details");
                Console.WriteLine("7. Edit Item Details");
                Console.WriteLine("8. Disable Item");
                Console.WriteLine("9. View Members");
                Console.WriteLine("10. Disable Member");
                Console.WriteLine("11. Set Member Borrow Limit");
                Console.WriteLine("12. View All Loans");
                Console.WriteLine("13. View Fine Payments");
                Console.WriteLine("14. View Available Items");
                Console.WriteLine("15. View Borrowed Items");
                Console.WriteLine("16. Most Borrowed Material");
                Console.WriteLine("17. Overdue Members");
                Console.WriteLine("18. Members with Outstanding Fines");
                Console.WriteLine("19. Total Fines Collected");
                Console.WriteLine("20. View Member Operations");
                Console.WriteLine("21. Search Member and View Details");
                Console.WriteLine("22. Logout");
                Console.Write("Choose: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddBook();
                        break;

                    case 2:
                        AddMagazine();
                        break;

                    case 3:
                        AddResearchPaper();
                        break;

                    case 4:
                        BrowseLibrary();
                        break;

                    case 5:
                        SearchItems();
                        break;

                    case 6:
                        ViewItemDetails();
                        break;

                    case 7:
                        EditItemDetails();
                        break;

                    case 8:
                        DisableItem();
                        break;

                    case 9:
                        ViewMembers();
                        break;

                    case 10:
                        DisableMember();
                        break;

                    case 11:
                        SetMemberBorrowLimit();
                        break;

                    case 12:
                        ViewAllLoans();
                        break;

                    case 13:
                        ViewFinePayments();
                        break;

                    case 14:
                        ViewAvailableItems();
                        break;

                    case 15:
                        ViewBorrowedItems();
                        break;

                    case 16:
                        ViewMostBorrowedItem();
                        break;

                    case 17:
                        ViewOverdueMembers();
                        break;

                    case 18:
                        ViewMembersWithOutstandingFines();
                        break;

                    case 19:
                        ViewTotalFinesCollected();
                        break;

                    case 20:
                        ViewMemberOperations();
                        break;

                    case 21:
                        ViewMemberDetails();
                        break;

                    case 22:
                        Console.WriteLine("Logging out...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

            } while (choice != 22);
        }

        #endregion

        #region Add Materials

        static void AddBook()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Author: ");
            string author = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Title and author are required.");
                return;
            }

            Console.Write("Pages: ");

            if (!int.TryParse(Console.ReadLine(), out int pages) || pages <= 0)
            {
                Console.WriteLine("Invalid number of pages.");
                return;
            }

            library.AddItem(new Book(0, title, author, pages));
            Console.WriteLine("Book added successfully.");
        }

        static void AddMagazine()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title is required.");
                return;
            }

            Console.Write("Issue Number: ");

            if (!int.TryParse(Console.ReadLine(), out int issueNumber) ||
                issueNumber <= 0)
            {
                Console.WriteLine("Invalid issue number.");
                return;
            }

            library.AddItem(new Magazine(0, title, issueNumber));
            Console.WriteLine("Magazine added successfully.");
        }

        static void AddResearchPaper()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Researcher: ");
            string researcher = Console.ReadLine() ?? "";

            Console.Write("Research Field: ");
            string researchField = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(researcher) ||
                string.IsNullOrWhiteSpace(researchField))
            {
                Console.WriteLine("All fields are required.");
                return;
            }

            library.AddItem(
                new ResearchPaper(0, title, researcher, researchField));

            Console.WriteLine("Research paper added successfully.");
        }

        #endregion

        #region Manage Materials

        static void ViewItemDetails()
        {
            Console.Write("Enter Item ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid item ID.");
                return;
            }

            LibraryItem? item = library.GetItemDetails(id);

            if (item == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            Console.WriteLine("\n===== Item Details =====");
            Console.WriteLine($"ID: {item.Id}");
            Console.WriteLine($"Title: {item.Title}");
            Console.WriteLine($"Borrowed: {(item.IsBorrowed ? "Yes" : "No")}");
            Console.WriteLine($"Active: {(item.IsActive ? "Yes" : "No")}");

            if (item is Book book)
            {
                Console.WriteLine("Type: Book");
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"Pages: {book.Pages}");
            }
            else if (item is Magazine magazine)
            {
                Console.WriteLine("Type: Magazine");
                Console.WriteLine($"Issue Number: {magazine.IssueNumber}");
            }
            else if (item is ResearchPaper paper)
            {
                Console.WriteLine("Type: Research Paper");
                Console.WriteLine($"Researcher: {paper.Researcher}");
                Console.WriteLine($"Research Field: {paper.ResearchField}");
            }

            Console.WriteLine($"Loan Period: {item.GetLoanPeriod()} days");
        }

        static void EditItemDetails()
        {
            Console.Write("Enter Item ID to edit: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid item ID.");
                return;
            }

            LibraryItem? item = library.GetItemDetails(id);

            if (item == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            if (!item.IsActive)
            {
                Console.WriteLine("Cannot edit a disabled item.");
                return;
            }

            Console.WriteLine("\nCurrent item details:");
            Console.WriteLine(item);

            Console.Write("New Title: ");
            string title = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }

            bool success = false;

            if (item is Book)
            {
                Console.Write("New Author: ");
                string author = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Author cannot be empty.");
                    return;
                }

                Console.Write("New Pages: ");

                if (!int.TryParse(Console.ReadLine(), out int pages) || pages <= 0)
                {
                    Console.WriteLine("Invalid number of pages.");
                    return;
                }

                success = library.EditItem(
                    id, title, author: author, pages: pages);
            }
            else if (item is Magazine)
            {
                Console.Write("New Issue Number: ");

                if (!int.TryParse(Console.ReadLine(), out int issueNumber) ||
                    issueNumber <= 0)
                {
                    Console.WriteLine("Invalid issue number.");
                    return;
                }

                success = library.EditItem(
                    id, title, issueNumber: issueNumber);
            }
            else if (item is ResearchPaper)
            {
                Console.Write("New Researcher: ");
                string researcher = Console.ReadLine() ?? "";

                Console.Write("New Research Field: ");
                string researchField = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(researcher) ||
                    string.IsNullOrWhiteSpace(researchField))
                {
                    Console.WriteLine("Researcher and research field are required.");
                    return;
                }

                success = library.EditItem(
                    id,
                    title,
                    researcher: researcher,
                    researchField: researchField);
            }

            Console.WriteLine(
                success
                    ? "Item details updated successfully."
                    : "Could not update item details.");

            if (success)
            {
                Console.WriteLine("Updated details:");
                Console.WriteLine(library.GetItemDetails(id));
            }
        }

        static void DisableItem()
        {
            Console.Write("Item ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid item ID.");
                return;
            }

            Console.WriteLine(
                library.DisableItem(id)
                    ? "Item disabled successfully."
                    : "Could not disable item. It may not exist, may already be disabled, or may currently be borrowed.");
        }

        #endregion

        #region Manage Members

        static void ViewMembers()
        {
            List<Member> members = library.GetAllMembers();

            if (members.Count == 0)
            {
                Console.WriteLine("No members registered.");
                return;
            }

            foreach (Member member in members)
            {
                Console.WriteLine(member);
            }
        }

        static void ViewMemberDetails()
        {
            Console.Write("Membership Number: ");

            if (!int.TryParse(Console.ReadLine(), out int membershipNumber))
            {
                Console.WriteLine("Invalid membership number.");
                return;
            }

            Member? member = library.FindMember(membershipNumber);

            if (member == null)
            {
                Console.WriteLine("Member not found.");
                return;
            }

            Console.WriteLine("\n===== Member Details =====");
            Console.WriteLine(member);
            Console.WriteLine(
                $"Total Fines: {reports.GetMemberTotalFines(membershipNumber)}");
            Console.WriteLine(
                $"Total Paid: {reports.GetMemberTotalPaid(membershipNumber)}");
            Console.WriteLine(
                $"Outstanding Fines: {reports.GetMemberOutstandingFines(membershipNumber)}");

            Console.WriteLine("\n===== Borrowing History =====");

            List<Loan> loans = reports.GetMemberOperations(membershipNumber);

            if (loans.Count == 0)
            {
                Console.WriteLine("No borrowing history found.");
                return;
            }

            foreach (Loan loan in loans)
            {
                Console.WriteLine(loan);
            }
        }

        static void DisableMember()
        {
            Console.Write("Membership Number: ");

            if (!int.TryParse(Console.ReadLine(), out int membershipNumber))
            {
                Console.WriteLine("Invalid membership number.");
                return;
            }

            Console.WriteLine(
                library.DisableMember(membershipNumber)
                    ? "Member disabled successfully."
                    : "Could not disable member.");
        }

        static void SetMemberBorrowLimit()
        {
            Console.Write("Membership Number: ");

            if (!int.TryParse(Console.ReadLine(), out int membershipNumber))
            {
                Console.WriteLine("Invalid membership number.");
                return;
            }

            Console.Write("New Borrow Limit: ");

            if (!int.TryParse(Console.ReadLine(), out int limit) || limit <= 0)
            {
                Console.WriteLine("Invalid borrow limit.");
                return;
            }

            Console.WriteLine(
                library.SetMemberBorrowLimit(membershipNumber, limit)
                    ? "Borrow limit updated successfully."
                    : "Could not update borrow limit.");
        }

        #endregion

        #region Loans and Payments

        static void ViewAllLoans()
        {
            List<Loan> loans = library.GetAllLoans();

            if (loans.Count == 0)
            {
                Console.WriteLine("No loans found.");
                return;
            }

            foreach (Loan loan in loans)
            {
                Console.WriteLine(loan);
            }
        }

        static void ViewFinePayments()
        {
            List<FinePayment> payments = library.GetAllPayments();

            if (payments.Count == 0)
            {
                Console.WriteLine("No fine payments found.");
                return;
            }

            foreach (FinePayment payment in payments)
            {
                Console.WriteLine(payment);
            }
        }

        #endregion

        #region Reports

        static void ViewAvailableItems()
        {
            List<LibraryItem> items = reports.GetAvailableItems();

            if (items.Count == 0)
            {
                Console.WriteLine("No available items.");
                return;
            }

            foreach (LibraryItem item in items)
            {
                Console.WriteLine(item);
            }
        }

        static void ViewBorrowedItems()
        {
            List<LibraryItem> items = reports.GetBorrowedItems();

            if (items.Count == 0)
            {
                Console.WriteLine("No borrowed items.");
                return;
            }

            foreach (LibraryItem item in items)
            {
                Console.WriteLine(item);
            }
        }

        static void ViewMostBorrowedItem()
        {
            LibraryItem? item = reports.GetMostBorrowedItem();

            if (item == null)
            {
                Console.WriteLine("No borrowing records found.");
                return;
            }

            Console.WriteLine("Most Borrowed Material:");
            Console.WriteLine(item);
        }

        static void ViewOverdueMembers()
        {
            List<Member> members = reports.GetOverdueMembers();

            if (members.Count == 0)
            {
                Console.WriteLine("No members with overdue items.");
                return;
            }

            foreach (Member member in members)
            {
                Console.WriteLine(member);
            }
        }

        static void ViewMembersWithOutstandingFines()
        {
            List<Member> members = reports.GetMembersWithOutstandingFines();

            if (members.Count == 0)
            {
                Console.WriteLine("No members with outstanding fines.");
                return;
            }

            foreach (Member member in members)
            {
                decimal outstanding = reports.GetMemberOutstandingFines(
                    member.MembershipNumber);

                Console.WriteLine(
                    $"{member} | Outstanding Fines: {outstanding}");
            }
        }

        static void ViewTotalFinesCollected()
        {
            decimal total = reports.GetTotalFinesCollected();
            Console.WriteLine($"Total Fines Collected: {total}");
        }

        static void ViewMemberOperations()
        {
            Console.Write("Membership Number: ");

            if (!int.TryParse(Console.ReadLine(), out int membershipNumber))
            {
                Console.WriteLine("Invalid membership number.");
                return;
            }

            Member? member = library.FindMember(membershipNumber);

            if (member == null)
            {
                Console.WriteLine("Member not found.");
                return;
            }

            List<Loan> operations = reports.GetMemberOperations(membershipNumber);

            Console.WriteLine($"Operations for {member.Name}:");

            if (operations.Count == 0)
            {
                Console.WriteLine("No operations found.");
                return;
            }

            foreach (Loan loan in operations)
            {
                Console.WriteLine(loan);
            }
        }

        #endregion

        #region Member Menu

        static void MemberMenu(Member member)
        {
            int choice = 0;

            do
            {
                Console.WriteLine("\n===== Member Menu =====");
                Console.WriteLine("1. Browse Available Items");
                Console.WriteLine("2. Search Items");
                Console.WriteLine("3. Borrow Item");
                Console.WriteLine("4. Return Item");
                Console.WriteLine("5. View My Loans");
                Console.WriteLine("6. Pay Fine");
                Console.WriteLine("7. View Outstanding Fines");
                Console.WriteLine("8. Logout");
                Console.Write("Choose: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ViewAvailableItems();
                        break;

                    case 2:
                        SearchItems();
                        break;

                    case 3:
                        BorrowItem(member);
                        break;

                    case 4:
                        ReturnItem(member);
                        break;

                    case 5:
                        ViewMemberLoans(member);
                        break;

                    case 6:
                        PayFine(member);
                        break;

                    case 7:
                        Console.WriteLine(
                            $"Outstanding fines: " +
                            $"{library.GetMemberOutstandingFines(member.MembershipNumber)}");
                        break;

                    case 8:
                        Console.WriteLine("Logging out...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

            } while (choice != 8);
        }

        static void BorrowItem(Member member)
        {
            Console.Write("Item ID: ");

            if (!int.TryParse(Console.ReadLine(), out int itemId))
            {
                Console.WriteLine("Invalid item ID.");
                return;
            }

            bool success = library.BorrowItem(member.MembershipNumber, itemId);

            Console.WriteLine(
                success
                    ? "Item borrowed successfully."
                    : "Could not borrow item. Check availability, membership status, outstanding fines, and borrow limit.");
        }

        static void ReturnItem(Member member)
        {
            Console.Write("Loan ID: ");

            if (!int.TryParse(Console.ReadLine(), out int loanId))
            {
                Console.WriteLine("Invalid loan ID.");
                return;
            }

            Console.WriteLine(
                library.ReturnItem(member.MembershipNumber, loanId)
                    ? "Item returned successfully."
                    : "Could not return item. Check the loan ID.");
        }

        static void ViewMemberLoans(Member member)
        {
            List<Loan> loans = library.GetMemberLoans(member.MembershipNumber);

            if (loans.Count == 0)
            {
                Console.WriteLine("No loans found.");
                return;
            }

            foreach (Loan loan in loans)
            {
                Console.WriteLine(loan);
            }
        }

        static void PayFine(Member member)
        {
            Console.Write("Loan ID: ");

            if (!int.TryParse(Console.ReadLine(), out int loanId))
            {
                Console.WriteLine("Invalid loan ID.");
                return;
            }

            Console.Write("Amount to pay: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) ||
                amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            Console.Write("Payment Batch Number: ");

            if (!int.TryParse(Console.ReadLine(), out int batchNumber) ||
                batchNumber <= 0)
            {
                Console.WriteLine("Invalid batch number.");
                return;
            }

            Console.WriteLine(
                library.PayFine(
                    member.MembershipNumber,
                    loanId,
                    amount,
                    batchNumber)
                    ? "Payment successful."
                    : "Payment failed. Check the loan, return status, outstanding fine, or whether the batch number was already used.");
        }

        #endregion
    }
}
