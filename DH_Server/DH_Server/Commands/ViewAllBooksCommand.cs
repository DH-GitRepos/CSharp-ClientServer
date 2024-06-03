using Controllers;
using DH_Server.Presenters;

namespace DH_Server.Commands
{
    class ViewAllBooksCommand : Command
    {

        public ViewAllBooksCommand()
        {
        }

        public AllBooksViewData Execute_Books()
        {
            ViewAllBooksController controller =
                new ViewAllBooksController(
                        new AllBooksPresenter());

            AllBooksViewData data =
                (AllBooksViewData)controller.Execute();

            return data;
        }

        public CommandLineViewData Execute_CheckBook(int bookID)
        {
            throw new System.NotImplementedException();
        }

        public AllMembersViewData Execute_Members()
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
