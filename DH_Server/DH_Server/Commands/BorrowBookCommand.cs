﻿using Controllers;
using DH_Server.Presenters;
using System;
using System.Collections.Generic;

namespace DH_Server.Commands
{
    class BorrowBookCommand : Command
    {

        public BorrowBookCommand()
        {
        }

        public CommandLineViewData Execute_BorrowBook(int memberID, int bookID)
        {
            BorrowBookController controller = 
                new BorrowBookController(
                    memberID,
                    bookID,    
                    new MessagePresenter());

            try {
                CommandLineViewData data =
                    (CommandLineViewData)controller.Execute();
                data.Status = true;                
                return data;
            }
            catch (Exception e)
            {
                CommandLineViewData data = new CommandLineViewData(new List<string>(), false);
                data.ViewData.Add(e.Message);
                return data;
            }
        }

        public CommandLineViewData Execute_CheckBook(int bookID)
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

        public AllBooksViewData Execute_Books()
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

        public CommandLineViewData Execute_InitialiseDatabase()
        {
            throw new System.NotImplementedException();
        }
    }
}
