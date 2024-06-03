using System.Collections.Generic;
using UseCases.DTOs;
using UseCases;

namespace DH_Server.Presenters
{
    class AllBooksViewData : IViewData
    {
        public List<BookDTO> ViewData { get; }

        public AllBooksViewData(List<BookDTO> viewData)
        {
            ViewData = viewData;
        }
    }
}
