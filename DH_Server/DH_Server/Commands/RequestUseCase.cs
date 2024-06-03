namespace DH_Server.Commands
{
    interface RequestUseCase
    {
        public const int INITIALISE_DATABASE = 0;
        public const int BORROW_BOOK = 1;
        public const int CHECK_BORROW_BOOK = 11;
        public const int RETURN_BOOK = 2;
        public const int RETURN_BOOK_UPDATE = 22;
        public const int RENEW_LOAN = 3;
        public const int RENEW_LOAN_UPDATE = 33;
        public const int VIEW_ALL_BOOKS = 4;
        public const int VIEW_ALL_MEMBERS = 5;
        public const int VIEW_CURRENT_LOANS = 6;
        public const int BROADCAST_MESSAGE = 7;
        public const int EXIT = 8;
        public const int MAIN_WINDOW = 9;
        public const int REGISTER_CLIENT = 99;
    }
}
