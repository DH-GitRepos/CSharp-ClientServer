using UseCases.DTOs;

namespace UseCases
{
    public abstract class AbstractPresenter
    {

        public IDto DataToPresent { get; set; }

        public abstract IViewData ViewData { get; }
    }
}
