using DH_Server.Presenters;

namespace DH_Server.Commands
{
    interface Command
    {
        AllBooksViewData Execute_Books();
        AllMembersViewData Execute_Members();
        CurrentLoansViewData Execute_Loans();
        CommandLineViewData Execute_CheckBook(int bookID);
        CommandLineViewData Execute_BorrowBook(int memberID, int bookID);
        CommandLineViewData Execute_ReturnBook(int memberID, int bookID);
        CommandLineViewData Execute_RenewLoan(int memberID, int bookID);
        CommandLineViewData Execute_InitialiseDatabase();
    }
}
