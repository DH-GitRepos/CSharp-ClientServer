using System.ComponentModel;

namespace DH_GUIClient.DTO
{
    [Serializable]
    public class BookDTO : INotifyPropertyChanged
    {
        private string? state;
        public int ID { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string? State
        {
            get { return state; }

            set
            {
                if (state != value)
                {
                    state = value;
                    OnPropertyChanged(nameof(State));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BookDTO(int id, string author, string title, string isbn, string? state)
        {
            this.ID = id;
            this.Author = author;
            this.ISBN = isbn;
            this.Title = title;
            this.State = state;
        }
    }
}
