using DH_GUIClientComms.DTOs;
using System.Collections.Generic;
using UseCase;
using System.Diagnostics;
using System;

namespace DH_GUIPresenters
{
    public class AllBooksPresenter : AbstractPresenter
    {

        public override IViewData ViewData
        {
            get
            {
                List<BookDTO> books = ((BookDTO_List)DataToPresent).List;
                return new CommandLineViewData(books);
            }
        }

        private string DisplayBook(BookDTO b)
        {
            return string.Format(
                "\t{0, -4} {1, -20} {2, -20} {3, -15} {4}",
                b.Id,
                b.Title,
                b.Author,
                b.ISBN,
                b.State);
        }
    }
}
