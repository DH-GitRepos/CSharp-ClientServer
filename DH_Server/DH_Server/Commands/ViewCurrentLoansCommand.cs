using Controllers;
using DH_Server.Presenters;

namespace DH_Server.Commands
{
    class ViewCurrentLoansCommand : Command
    {
        public ViewCurrentLoansCommand()
        {
        }
        
        public CurrentLoansViewData Execute_Loans()
        {
            ViewCurrentLoansController controller =
                new ViewCurrentLoansController(
                        new CurrentLoansPresenter());

            CurrentLoansViewData data =
                (CurrentLoansViewData)controller.Execute();

            return data;
            // ConsoleWriter.WriteStrings(data.ViewData);
        }

        public CommandLineViewData Execute_CheckBook(int bookID)
        {
            throw new System.NotImplementedException();
        }

        public AllBooksViewData Execute_Books()
        {
            throw new System.NotImplementedException();
        }

        public AllMembersViewData Execute_Members()
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
