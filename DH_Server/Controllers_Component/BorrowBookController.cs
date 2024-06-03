using DatabaseGateway;
using UseCases.DTOs;
using UseCases.UseCase;
using UseCases;

namespace Controllers
{
    public class BorrowBookController : AbstractController
    {

        private int MemberId;
        private int BookId;

        public BorrowBookController(
            int memberId, 
            int bookId, 
            AbstractPresenter presenter) : base(presenter)
        {
            this.MemberId = memberId;
            this.BookId = bookId;
        }

        public override IDto ExecuteUseCase()
        {
            return new BorrowBook_UseCase(MemberId, BookId, new DatabaseGatewayFacade()).Execute();
        }
    }
}
