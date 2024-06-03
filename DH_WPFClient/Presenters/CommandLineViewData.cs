using System.Collections.Generic;
using UseCase;
using DH_GUIClientComms.DTOs;

namespace DH_GUIPresenters
{
    public class CommandLineViewData : IViewData
    {
        public List<LoanDTO> ViewLoanData { get; }

        public List<MemberDTO> ViewMemberData { get; }

        public List<BookDTO> ViewBookData { get; }

        public List<string> ViewMessageData { get; }


        public CommandLineViewData(List<LoanDTO> viewData)
        {
            ViewLoanData = viewData;
        }

        public CommandLineViewData(List<MemberDTO> viewData)
        {
            ViewMemberData = viewData;
        }

        public CommandLineViewData(List<BookDTO> viewData)
        {
            ViewBookData = viewData;
        }

        public CommandLineViewData(List<string> viewData)
        {
            ViewMessageData = viewData;
        }
    }
}
