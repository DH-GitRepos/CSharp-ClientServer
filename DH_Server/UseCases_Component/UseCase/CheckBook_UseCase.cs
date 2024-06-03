using UseCases.DTOs;
using Entities;
using System;

namespace UseCases.UseCase
{
    public class CheckBook_UseCase : AbstractUseCase
    {

        private readonly int bookId;
        private readonly int memberId;

        public CheckBook_UseCase(int bookId, IDatabaseGatewayFacade gatewayFacade) : base(gatewayFacade)
        {
            this.bookId = bookId;
        }

        public override IDto Execute()
        {
            try
            {
                BookConverter bookConverter = new BookConverter();

                Book b = gatewayFacade.FindBook(bookId);

                if (b == null)
                {
                    return new MessageDTO("Book not found");
                }

                if (b.Borrow())
                {
                    return new MessageDTO("AVAILABLE");
                }
                else
                {
                    return new MessageDTO("UNAVAILABLE");
                }
            }
            catch (Exception e)
            {
                return new MessageDTO(e.Message);
            }
        }
    }
}
