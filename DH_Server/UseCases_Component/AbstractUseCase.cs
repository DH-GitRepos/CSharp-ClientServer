using UseCases.DTOs;

namespace UseCases
{

    public abstract class AbstractUseCase
    {

        protected readonly IDatabaseGatewayFacade gatewayFacade;

        protected AbstractUseCase(IDatabaseGatewayFacade gatewayFacade)
        {
            this.gatewayFacade = gatewayFacade;
        }

        public abstract IDto Execute();

    }
}
