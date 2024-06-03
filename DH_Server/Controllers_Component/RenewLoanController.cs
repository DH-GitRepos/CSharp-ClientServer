using DatabaseGateway;
using UseCases.DTOs;
using UseCases.UseCase;
using UseCases;

namespace Controllers
{
    public class RenewLoanController : AbstractController
    {

        private int memberId;
        private int bookId;

        public RenewLoanController(
            int memberId, 
            int bookId, 
            AbstractPresenter presenter) : base(presenter)
        {
            this.memberId = memberId;
            this.bookId = bookId;
        }

        public override IDto ExecuteUseCase()
        {
            return new RenewLoan_UseCase(memberId, bookId, new DatabaseGatewayFacade()).Execute();
        }
    }
}
