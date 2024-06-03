using DatabaseGateway;
using UseCases.DTOs;
using UseCases.UseCase;
using UseCases;

namespace Controllers
{
    public class CheckBookController : AbstractController
    {
        private int BookId;

        public CheckBookController(
            int bookId, 
            AbstractPresenter presenter) : base(presenter)
        {
            this.BookId = bookId;
        }

        public override IDto ExecuteUseCase()
        {
            return new CheckBook_UseCase(BookId, new DatabaseGatewayFacade()).Execute();
        }
    }
}
