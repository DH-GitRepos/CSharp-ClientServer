using DH_GUIClientComms.DTOs;
using DH_GUIPresenters.Visitor;
using System.Collections.Generic;
using UseCase;

namespace DH_GUIPresenters
{
    public class CurrentLoansPresenter : AbstractPresenter
    {

        public override IViewData ViewData
        {
            get
            {
                List<LoanDTO> loans = ((LoanDTO_List)DataToPresent).List;

                return new CommandLineViewData(loans);
            }
        }

        private List<VisitableLoan> GetVisitableLoans(List<LoanDTO> loans)
        {
            List<VisitableLoan> list = new List<VisitableLoan>(loans.Count);

            loans.ForEach(loan => list.Add(new VisitableLoan(loan)));

            return list;
        }
    }
}
