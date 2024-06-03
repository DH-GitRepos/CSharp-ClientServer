using System.ComponentModel;

namespace DH_GUIClient.DTO
{
    [Serializable]
    public class LoanDTO : INotifyPropertyChanged
    {
        private DateTime? dueDate;
        private DateTime? returnDate;
        private int? numberOfRenewals;
        private string? loanState;

        public int ID { get; set; }
        public MemberDTO Member { get; set; }
        public BookDTO Book { get; set; }
        public DateTime? DueDate
        {
            get { return dueDate; }

            set
            {
                if (dueDate != value)
                {
                    dueDate = value;
                    OnPropertyChanged(nameof(DueDate));
                }
            }
        }

        public DateTime LoanDate { get; set; }
        public int? NumberOfRenewals
        {
            get { return numberOfRenewals; }

            set
            {
                if (numberOfRenewals != value)
                {
                    numberOfRenewals = value;
                    OnPropertyChanged(nameof(NumberOfRenewals));
                }
            }
        }

        public DateTime? ReturnDate
        {
            get { return returnDate; }

            set
            {
                if (returnDate != value)
                {
                    returnDate = value;
                    OnPropertyChanged(nameof(ReturnDate));
                }
            }
        }

        public string? LoanState
        {
            get { return loanState; }

            set
            {
                if (loanState != value)
                {
                    loanState = value;
                    OnPropertyChanged(nameof(LoanState));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LoanDTO(int id, MemberDTO member, BookDTO book, DateTime? dueDate, DateTime loanDate, int? numberOfRenewals, DateTime? returnDate, string? loanState)
        {
            this.ID = id;
            this.Member = member;
            this.Book = book;
            this.DueDate = dueDate;
            this.LoanDate = loanDate;
            this.NumberOfRenewals = numberOfRenewals;
            this.ReturnDate = returnDate;
            this.LoanState = loanState;
        }
    }
}
