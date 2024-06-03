namespace DH_GUIClient.DTO
{
    [Serializable]
    public class NullDTO
    {
        public string Message;

        public NullDTO() 
        { 
            this.Message = "No data, null object.";
        }
    }
}
