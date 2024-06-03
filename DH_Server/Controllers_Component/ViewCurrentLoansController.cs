using DatabaseGateway;
using UseCases.DTOs;
using UseCases.UseCase;
using UseCases;

namespace Controllers
{
    public class ViewCurrentLoansController : AbstractController
    {

        public ViewCurrentLoansController(
            AbstractPresenter presenter) : base(presenter)
        {
        }

        public override IDto ExecuteUseCase()
        {
            return new ViewCurrentLoans_UseCase(new DatabaseGatewayFacade()).Execute();
        }
    }
}
