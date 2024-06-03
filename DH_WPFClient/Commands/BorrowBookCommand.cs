// using Controllers;
using DH_GUIClientComms;
using DH_GUIClientComms.DTOs;
using DH_GUIPresenters;
using System.Collections.Generic;
using System;

namespace DH_GUICommands
{
    public class BorrowBookCommand : Command
    {
        int member_id;
        int book_id;

        public BorrowBookCommand(int member_id, int book_id)
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
                BorrowBookController controller = 
                    new BorrowBookController(
                        member_id,
                        book_id,                
                        new MessagePresenter());

                CommandLineViewData data = 
                    (CommandLineViewData)controller.Execute();

                // ConsoleWriter.WriteStrings(data.ViewData);

                return data;
            */

            int taskID = RequestUseCase.BORROW_BOOK;
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
