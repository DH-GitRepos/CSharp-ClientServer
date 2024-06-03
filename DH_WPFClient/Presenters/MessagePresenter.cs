using DH_GUIClientComms.DTOs;
using System.Collections.Generic;
using UseCase;

namespace DH_GUIPresenters
{
    public class MessagePresenter : AbstractPresenter
    {

        public override IViewData ViewData 
        { 
            get
            {
                List<string> lines = new List<string>(1);
                lines.Add("\n" + ((MessageDTO)DataToPresent).Message);
                return new CommandLineViewData(lines);
            }
        }
    }
}
