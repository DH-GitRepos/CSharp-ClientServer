using System.Collections.Generic;
using System;

namespace DH_GUIClientComms.DTOs
{
    [Serializable]
    public class BookDTO_List : IDto
    {
        public List<BookDTO> List { get; }

        public BookDTO_List(List<BookDTO> list)
        {
            List = list;
        }
    }
}
