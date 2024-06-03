using DatabaseGateway;
using UseCases.DTOs;
using UseCases.UseCase;
using UseCases;

namespace Controllers
{
    public class InitialiseDatabaseController : AbstractController
    {
        public InitialiseDatabaseController(
            AbstractPresenter presenter) : base(presenter)
        {
        }

        public override IDto ExecuteUseCase()
        {
            return new InitialiseDatabase_UseCase(new DatabaseGatewayFacade()).Execute();
        }
    }
}
