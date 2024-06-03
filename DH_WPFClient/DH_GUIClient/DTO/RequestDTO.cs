namespace DH_GUIClient.DTO
{
    [Serializable]
    public class RequestDTO
    {
        public string ClientID { get; set; }
        public string ClientType { get; set; }
        public bool AcceptingCommands { get; set; }
        public bool AcceptingMessages { get; set; }
        public int OpCode { get; set; }
        public bool Initialising { get; set; }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public string Message { get; set; }

        public RequestDTO(string ClientID,
                          string ClientType,
                          bool AcceptingCommands,
                          bool AcceptingMessages,
                          int OpCode,
                          bool Initialising,
                          int MemberID,
                          int BookID,
                          string Message)
        {
            this.ClientID = ClientID;
            this.ClientType = ClientType;
            this.AcceptingCommands = AcceptingCommands;
            this.AcceptingMessages = AcceptingMessages;
            this.OpCode = OpCode;
            this.Initialising = Initialising;
            this.MemberID = MemberID;
            this.BookID = BookID;
            this.Message = Message;
        }
    }
}
