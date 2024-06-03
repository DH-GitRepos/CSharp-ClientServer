using DH_GUIClientComms.DTOs;
using DH_GUIClientComms;
using DH_GUIPresenters;
using System.Collections.Generic;
using System;

namespace DH_GUICommands
{
    public class ViewAllBooksCommand : Command
    {

        public ViewAllBooksCommand()
        {
        }

        // public CommandLineViewData Execute()
        public Dictionary<string, object> Execute()
        {
            // ROUTE COMMAND OUTPUT TO THE CLIENTCOMMS COMPONENT
            // Dictionary should contain eg: books: List<BookDTO>, members: List<MemberDTO>

            /*
                ViewAllBooksController controller =
                    new ViewAllBooksController(
                            new AllBooksPresenter());

                CommandLineViewData data =
                    (CommandLineViewData)controller.Execute();

                // ConsoleWriter.WriteStrings(data.ViewData);

                return data; 
            */

            int taskID = RequestUseCase.VIEW_ALL_BOOKS;
            Dictionary<string, object> params_list = null;
            ServerCommandDTO serverCommand = new ServerCommandDTO(taskID, params_list);

            try
            {
                SendCommandToServer(serverCommand);

            }
            catch (Exception ex)
            {
                // DO SOMETHING WITH THE EXCEPTION
            }

        }

    }
}
