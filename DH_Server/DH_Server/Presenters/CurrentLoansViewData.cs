using System.Collections.Generic;
using UseCases.DTOs;
using UseCases;

namespace DH_Server.Presenters
{
    class CurrentLoansViewData : IViewData
    {
        public List<LoanDTO> ViewData { get; }

        public CurrentLoansViewData(List<LoanDTO> viewData)
        {
            ViewData = viewData;
        }
    }
}
