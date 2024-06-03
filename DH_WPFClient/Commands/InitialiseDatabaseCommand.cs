// using Controllers;
using DH_GUIClientComms;
using DH_GUIClientComms.DTOs;
using DH_GUIPresenters;
using System.Collections.Generic;
using System;

namespace DH_GUICommands
{
    public class InitialiseDatabaseCommand : Command
    {

        public InitialiseDatabaseCommand()
        {
        }

        // public CommandLineViewData Execute()
        public void Execute()
        {
            // ROUTE COMMAND OUTPUT TO THE CLIENTCOMMS COMPONENT
            // Dictionary should contain eg: books: List<BookDTO>, members: List<MemberDTO>

            /*
                InitialiseDatabaseController controller =
                    new InitialiseDatabaseController(
                            new MessagePresenter());

                CommandLineViewData data =
                    (CommandLineViewData)controller.Execute();

                // ConsoleWriter.WriteStrings(data.ViewData);

                return data;
            */

            int taskID = RequestUseCase.INITIALISE_DATABASE;
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
