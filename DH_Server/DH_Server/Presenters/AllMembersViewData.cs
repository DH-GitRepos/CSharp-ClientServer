using System.Collections.Generic;
using UseCases.DTOs;
using UseCases;

namespace DH_Server.Presenters
{
    class AllMembersViewData : IViewData
    {
        public List<MemberDTO> ViewData { get; }

        public AllMembersViewData(List<MemberDTO> viewData)
        {
            ViewData = viewData;
        }
    }
}
