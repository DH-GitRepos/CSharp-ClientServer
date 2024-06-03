using System.Collections.Generic;
using System;

namespace UseCases.DTOs
{
    [Serializable]
    public class MemberDTO_List : IDto
    {
        public List<MemberDTO> List { get; }

        public MemberDTO_List(List<MemberDTO> list)
        {
            List = list;
        }
    }
}
