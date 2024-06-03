using UseCases.DTOs;
using DH_Server.Presenters;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DH_Server.Commands
{
    public interface ICommandInterface
    {
        // USE CASE METHODS
        public static bool BorrowBook(int memberID, int bookID)
        {
            CommandFactory factory = new CommandFactory();

            CommandLineViewData borrow_book_status = factory
                .CreateCommand(RequestUseCase.BORROW_BOOK)
                .Execute_BorrowBook(memberID, bookID);

            return borrow_book_status.Status;
        }

        public static bool CheckBookIsAvailable(int bookID)
        {
            bool canBorrow = false;

            CommandFactory factory = new CommandFactory();

            CommandLineViewData check_book_status = factory
                .CreateCommand(RequestUseCase.CHECK_BORROW_BOOK)
                .Execute_CheckBook(bookID);

            // Remove whitespace from the response
            string responseStripped = Regex.Replace(check_book_status.ViewData[0], @"\s+", "");

            if (responseStripped == "AVAILABLE")
            {
                canBorrow = true;
            }

            return canBorrow;
        }

        public static bool ReturnBook(int memberID, int bookID)
        {
            CommandFactory factory = new CommandFactory();

            CommandLineViewData return_book_status = factory
                .CreateCommand(RequestUseCase.RETURN_BOOK)
                .Execute_ReturnBook(memberID, bookID);

            return return_book_status.Status;
        }

        public static bool RenewLoan(int memberID, int bookID)
        {
            CommandFactory factory = new CommandFactory();

            CommandLineViewData renew_loan_status = factory
                .CreateCommand(RequestUseCase.RENEW_LOAN)
                .Execute_RenewLoan(memberID, bookID);

            return renew_loan_status.Status;
        }

        public static List<BookDTO> GetBooksList()
        {
            CommandFactory factory = new CommandFactory();
            try
            {
                AllBooksViewData books = factory
                    .CreateCommand(RequestUseCase.VIEW_ALL_BOOKS)
                    .Execute_Books();

                List<BookDTO> data = books.ViewData;
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR(GetBooksList): " + e.Message);
                return new List<BookDTO>();
            }
        }

        public static List<MemberDTO> GetMembersList()
        {
            CommandFactory factory = new CommandFactory();
            try
            {
                AllMembersViewData members = factory
                    .CreateCommand(RequestUseCase.VIEW_ALL_MEMBERS)
                    .Execute_Members();

                List<MemberDTO> data = members.ViewData;
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR(GetMembersList): " + e.Message);
                return new List<MemberDTO>();
            }
        }

        public static List<LoanDTO> GetLoansByMemberID(int memberID)
        {
            List<LoanDTO> loans_all = GetLoansList();
            List<LoanDTO> loans_filtered = new();

            foreach (LoanDTO loan in loans_all)
            {
                if (loan.Member.ID == memberID)
                {
                    loans_filtered.Add(loan);
                }
            }
            return loans_filtered;
        }

        public static LoanDTO GetCurrentLoan(int memberID, int bookID)
        {
            List<LoanDTO> loans_all = GetLoansList();
            List<LoanDTO> loans_filtered = new();

            foreach (LoanDTO loan in loans_all)
            {
                if (loan.Member.ID == memberID && loan.Book.ID == bookID)
                {
                    loans_filtered.Add(loan);
                }
            }
            return loans_filtered[0];
        }

        public static List<LoanDTO> GetLoansList()
        {
            CommandFactory factory = new CommandFactory();
            try
            {
                CurrentLoansViewData loans = factory
                    .CreateCommand(RequestUseCase.VIEW_CURRENT_LOANS)
                    .Execute_Loans();

                List<LoanDTO> data = loans.ViewData;
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR(GetLoansList): " + e.Message);
                return new List<LoanDTO>();
            }
        }














    }
}
