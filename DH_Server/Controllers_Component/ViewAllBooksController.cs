using DatabaseGateway;
using UseCases.DTOs;
using UseCases.UseCase;
using UseCases;

namespace Controllers
{
    public class ViewAllBooksController : AbstractController
    {

        public ViewAllBooksController(
            AbstractPresenter presenter) : base(presenter)
        {
        }

        public override IDto ExecuteUseCase()
        {
            return new ViewAllBooks_UseCase(new DatabaseGatewayFacade()).Execute();
        }
    }
}
