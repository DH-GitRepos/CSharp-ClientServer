using System.Collections.Generic;
using UseCases.DTOs;
using UseCases;

namespace DH_Server.Presenters
{
    class MessagePresenter : AbstractPresenter
    {

        public override IViewData ViewData 
        { 
            get
            {
                List<string> lines = new List<string>(1);
                lines.Add("\n" + ((MessageDTO)DataToPresent).Message);
                return new CommandLineViewData(lines, true);
            }
        }
    }
}
