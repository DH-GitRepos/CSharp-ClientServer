using System.Collections.Generic;
using UseCases;

namespace DH_Server.Presenters
{
    class CommandLineViewData : IViewData
    {
        public List<string> ViewData { get; }
        public bool Status { get; set; }    

        public CommandLineViewData(List<string> viewData, bool status)
        {
            ViewData = viewData;
            Status = status;
        }
    }
}
