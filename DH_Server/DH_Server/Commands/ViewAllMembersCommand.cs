using Controllers;
using DH_Server.Presenters;

namespace DH_Server.Commands
{
    class ViewAllMembersCommand : Command
    {
        public ViewAllMembersCommand()
        {
        }

        public AllBooksViewData Execute_Books()
        {
            throw new System.NotImplementedException();
        }

        public AllMembersViewData Execute_Members()
        {
            ViewAllMembersController controller =
                new ViewAllMembersController(
                        new AllMembersPresenter());

            AllMembersViewData data =
                (AllMembersViewData)controller.Execute();

            return data;
            // ConsoleWriter.WriteStrings(data.ViewData);
        }

        public CommandLineViewData Execute_CheckBook(int bookID)
        {
            throw new System.NotImplementedException();
        }

        public CurrentLoansViewData Execute_Loans()
        {
            throw new System.NotImplementedException();
        }

        public CommandLineViewData Execute_BorrowBook(int memberID, int bookID)
        {
            throw new System.NotImplementedException();
        }

        public CommandLineViewData Execute_ReturnBook(int memberID, int bookID)
        {
            throw new System.NotImplementedException();
        }

        public CommandLineViewData Execute_RenewLoan(int memberID, int bookID)
        {
            throw new System.NotImplementedException();
        }

        public CommandLineViewData Execute_InitialiseDatabase()
        {
            throw new System.NotImplementedException();
        }
    }
}
