// using Controllers;
using DH_GUIClientComms;
using DH_GUIClientComms.DTOs;
using DH_GUIPresenters;
using System.Collections.Generic;
using System;

namespace DH_GUICommands
{
    public class RenewLoanCommand : Command
    {
        int member_id;
        int book_id;

        public RenewLoanCommand(int member_id, int book_id)
        {
            this.member_id = member_id;
            this.book_id = book_id;
        }

        // public CommandLineViewData Execute()
        public void Execute()
        {
            // ROUTE COMMAND OUTPUT TO THE CLIENTCOMMS COMPONENT
            // Dictionary should contain eg: books: List<BookDTO>, members: List<MemberDTO>

            /*
                RenewLoanController controller = 
                    new RenewLoanController(
                        member_id,
                        book_id,
                        new MessagePresenter());

                CommandLineViewData data =
                    (CommandLineViewData)controller.Execute();

                // ConsoleWriter.WriteStrings(data.ViewData);

                return data;
            */

            int taskID = RequestUseCase.RENEW_LOAN;
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
