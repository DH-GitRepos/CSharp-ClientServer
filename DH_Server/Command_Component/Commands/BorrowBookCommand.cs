using Controllers;
using Presenters;

namespace Commands
{
    class BorrowBookCommand : Command
    {

        public BorrowBookCommand()
        {
        }

        public void Execute()
        {
            BorrowBookController controller = 
                new BorrowBookController(
                    ConsoleReader.ReadInt("\nMember ID"),
                    ConsoleReader.ReadInt("Book ID"),
                    new MessagePresenter());

            CommandLineViewData data = 
                (CommandLineViewData)controller.Execute();

            ConsoleWriter.WriteStrings(data.ViewData);
        }
    }
}
