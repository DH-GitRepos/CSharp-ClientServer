using DH_GUIClientComms.DTOs;

namespace DH_GUIPresenters
{
    public abstract class AbstractPresenter
    {

        public IDto DataToPresent { get; set; }

        public abstract IViewData ViewData { get; }
    }
}
