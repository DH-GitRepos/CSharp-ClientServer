using System;
using System.Collections.Generic;

namespace UseCases.DTOs
{
    [Serializable]
    public class ResponseDTO(string OpCode, string ClientID, List<BookDTO> Books, List<MemberDTO> Members, List<LoanDTO> Loans, BookDTO Book, MemberDTO Member, LoanDTO Loan, bool Status, string StatusMessage, List<string> BroadcastMessages)
    {
        public string OpCode { get; set; } = OpCode;
        public string ClientID { get; set; } = ClientID;
        public List<BookDTO> Books { get; set; } = Books;
        public List<MemberDTO> Members { get; set; } = Members;
        public List<LoanDTO> Loans { get; set; } = Loans;
        public BookDTO Book { get; set; } = Book;
        public MemberDTO Member { get; set; } = Member;
        public LoanDTO Loan { get; set; } = Loan;
        public bool Status { get; set; } = Status;
        public string StatusMessage { get; set; } = StatusMessage;
        public List<string> BroadcastMessages { get; set; } = BroadcastMessages;
    }
}
