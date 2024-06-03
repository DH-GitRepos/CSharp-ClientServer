using System;

namespace DH_GUIClientComms.DTOs
{
    [Serializable]
    public class MessageDTO : IDto
    {
        public string Message { get; }

        public MessageDTO(string msg)
        {
            this.Message = msg;
        }
    }
}
