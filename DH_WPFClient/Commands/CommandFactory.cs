using System.Collections.Generic;

namespace DH_GUICommands
{
    public class CommandFactory
    {

        public CommandFactory()
        {
        }

        public static Command CreateCommand(int menuChoice, List<int> parameters = null)
        {
            switch (menuChoice)
            {
                case RequestUseCase.BORROW_BOOK:
                    return new BorrowBookCommand(parameters[0], parameters[1]);

                case RequestUseCase.INITIALISE_DATABASE:
                    return new InitialiseDatabaseCommand();

                case RequestUseCase.RENEW_LOAN:
                    return new RenewLoanCommand(parameters[0], parameters[1]);

                case RequestUseCase.RETURN_BOOK:
                    return new ReturnBookCommand(parameters[0], parameters[1]);

                case RequestUseCase.VIEW_ALL_BOOKS:
                    return new ViewAllBooksCommand();

                case RequestUseCase.VIEW_ALL_MEMBERS:
                    return new ViewAllMembersCommand();

                case RequestUseCase.VIEW_CURRENT_LOANS:
                    return new ViewCurrentLoansCommand();

                default:
                    return new NullCommand();
            }
        }
    }
}
