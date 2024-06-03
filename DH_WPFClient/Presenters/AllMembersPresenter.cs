using DH_GUIClientComms.DTOs;
using System.Collections.Generic;
using UseCase;

namespace DH_GUIPresenters
{
    public class AllMembersPresenter : AbstractPresenter
    {

        public override IViewData ViewData
        {
            get
            {
                List<MemberDTO> members = ((MemberDTO_List)DataToPresent).List;

                return new CommandLineViewData(members);
            }
        }

        private string DisplayMember(MemberDTO m)
        {
            return string.Format(
                "\t{0, -4} {1}",
                m.ID,
                m.Name);
        }
    }
}
