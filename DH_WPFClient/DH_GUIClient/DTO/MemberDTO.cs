namespace DH_GUIClient.DTO
{
    [Serializable]
    public class MemberDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public MemberDTO(int id, string name) 
        { 
            this.ID = id;
            this.Name = name;
        }
    }
}
