namespace DH_GUIClient
{
    internal interface ReturnOpCode
    {
        public const string INITIALISE_DATABASE = "0";
        public const string BORROW_BOOK = "1";
        public const string RETURN_BOOK = "2";
        public const string RETURN_BOOK_UPDATE = "22";
        public const string RENEW_LOAN = "3";
        public const string RENEW_LOAN_UPDATE = "33";
        public const string VIEW_ALL_BOOKS = "4";
        public const string VIEW_ALL_MEMBERS = "5";
        public const string VIEW_CURRENT_LOANS = "6";
        public const string EXIT = "8";
        public const string MAIN_WINDOW = "9";
        public const string BROADCAST_MESSAGE = "B";
        public const string UPDATE_VIEW_COMMAND = "UV";
        public const string ERROR = "E";
        public const string CLIENT_INITIALISED = "CI";
    }
}
