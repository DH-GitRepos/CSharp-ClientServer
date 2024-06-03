using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using DH_GUIClient.DTO;

namespace DH_GUIClient
{
    /// <summary>
    /// Interaction logic for RenewLoanWindow.xaml
    /// </summary>
    public partial class RenewLoanWindow : Window
    {
        private WPFClient client { get; set; }
        private Task clientTask { get; set; }
        private int book_id { get; set; }
        private int member_id { get; set; }
        private Dispatcher dispatcher { get; set; }
        private MemberDTO loan_member { get; set; }
        private BookDTO loan_book { get; set; }
        private DateTime loan_date { get; set; }
        private DateTime loan_due { get; set; }
        private DateTime loan_return { get; set; }
        private int loan_renewals { get; set; }
        private int selected_member_id { get; set; }
        private string member_name { get; set; }
        private int number_of_active_loans { get; set; }
        private string clientID { get; set; }
        private bool acceptsCommandsFlag { get; set; }
        private bool acceptsMessagesFlag { get; set; }
        private ObservableCollection<LoanDTO> loanCollection { get; set; }
        private ObservableCollection<LoanDTO> filteredLoanCollection { get; set; }
        private ObservableCollection<MemberDTO> memberCollection { get; set; }

        private const string CLIENT_FUNCTION = "renew_loan";

        public RenewLoanWindow(string ClientID)
        {
            InitializeComponent();
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.acceptsCommandsFlag = true;
            this.acceptsMessagesFlag = false;
            this.clientID = ClientID;
            this.loanCollection = new ObservableCollection<LoanDTO>();
            this.filteredLoanCollection = new ObservableCollection<LoanDTO>();
            this.memberCollection = new ObservableCollection<MemberDTO>();

            this.client = new WPFClient(DisplayResponseData,
                                   DisplayBroadcastMessageAsync,
                                   SendBroadcastMessage,
                                   ShowMessage,
                                   DisplayStatusMessage,
                                   ProcessViewUpdateCommand,
                                   dispatcher,
                                   CLIENT_FUNCTION,
                                   acceptsCommandsFlag,
                                   acceptsMessagesFlag,
                                   clientID);

            this.clientTask = Task.Run(client.RunAsync);
            CID_ID.Content = clientID;

            this.book_id = 0;
            this.member_id = 0;
            this.loan_member = new MemberDTO(-1, "");
            this.loan_book = new BookDTO(-1, "", "", "", "");
            this.member_name= "";
            this.selected_member_id = -1;
        }

        private void DisplayResponseData(ResponseDTO data)
        {
            int member_id = selected_member_id;
            bool operationSuccess = data.Status;
            Dispatcher.Invoke(() => DisplayStatusMessage(operationSuccess, data.StatusMessage));

            memberCollection = new ObservableCollection<MemberDTO>((IEnumerable<MemberDTO>)data.Members);
            Dispatcher.Invoke(() => Members_Datagrid.ItemsSource = memberCollection);
            selected_member_id = member_id;

            if (operationSuccess)
            {
                loanCollection = new ObservableCollection<LoanDTO>((IEnumerable<LoanDTO>)data.Loans);
                var filteredLoanCollection = new ObservableCollection<LoanDTO>(loanCollection.Where(loan => loan.Member.ID == selected_member_id));
                Dispatcher.Invoke(() => Loans_Datagrid.ItemsSource = filteredLoanCollection);
            }
            else
            {
                Dispatcher.Invoke(() => Loans_Datagrid.ItemsSource = null);
            }
        }

        private void ProcessViewUpdateCommand(ResponseDTO data)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, $"{DateTime.Now:HH:mm:ss}: Data update received."));

            LoanDTO incomingLoan = data.Loan;

            // If the incoming loan has a valid ID, it is an update to an existing loan or new loan.
            if (incomingLoan != null && incomingLoan.ID != -1)
            {
                bool loanExists = loanCollection.Any(loan => loan.ID == incomingLoan.ID);

                // If the loan exists, update the number of renewals.
                if (loanExists)
                {
                    LoanDTO? loanToUpdate = loanCollection.FirstOrDefault(loan => loan.ID == incomingLoan.ID);
                    if (loanToUpdate != null)
                    {
                        loanToUpdate.NumberOfRenewals = incomingLoan.NumberOfRenewals;
                    }
                }
                // Otherwise, add the new loan to the collection.
                else
                {
                    // loanCollection = new ObservableCollection<LoanDTO>((IEnumerable<LoanDTO>)data.Loans);
                    loanCollection.Add(incomingLoan);
                    var filteredLoanCollection = new ObservableCollection<LoanDTO>(loanCollection.Where(loan => loan.Member.ID == selected_member_id));
                    Dispatcher.Invoke(() => Loans_Datagrid.ItemsSource = filteredLoanCollection);
                }
            }
            // Otherwise, a loan has been removed. Refresh the collection.
            else
            {
                loanCollection = new ObservableCollection<LoanDTO>((IEnumerable<LoanDTO>)data.Loans);
                var filteredLoanCollection = new ObservableCollection<LoanDTO>(loanCollection.Where(loan => loan.Member.ID == selected_member_id));
                Dispatcher.Invoke(() => Loans_Datagrid.ItemsSource = filteredLoanCollection);
            }
        }

        private void DisplayBroadcastMessageAsync(List<string> msg)
        {
            /* ONLY IMPLMENTED IN MAIN WINDOW */
            // return Task.CompletedTask;
        }

        private void SendBroadcastMessage() { /* ONLY IMPLMENTED IN MAIN WINDOW */ }

        private void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        private void DisplayStatusMessage(bool operationSuccess, string msg)
        {
            Dispatcher.Invoke(() => Main_SystemMessages.Foreground = new SolidColorBrush(Colors.MediumSeaGreen));
            string? current_SystemMessage = Dispatcher.Invoke(() => Main_SystemMessages.Content.ToString());
            string? current_ErrorMessage = Dispatcher.Invoke(() => Main_ErrorMessages.Content.ToString());

            if (operationSuccess)
            {
                Dispatcher.Invoke(() => Main_SystemMessages.Content = msg);
                Dispatcher.Invoke(() => Main_ErrorMessages.Content = "");
            }
            else
            {
                Dispatcher.Invoke(() => Main_SystemMessages.Content = "");
                Dispatcher.Invoke(() => Main_ErrorMessages.Content = msg);
            }
        }

        public void Members_Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MemberDTO? selectedMember = Members_Datagrid.SelectedItem as MemberDTO;

            selected_member_id = selectedMember != null ? selectedMember.ID : 0;

            if (selectedMember != null)
            {
                try
                {
                    int memberId = selectedMember.ID;

                    RequestDTO request = new RequestDTO(clientID, CLIENT_FUNCTION, acceptsCommandsFlag, acceptsMessagesFlag, RequestUseCase.RENEW_LOAN_UPDATE, false, memberId, -1, "");
                    string requestString = client.serialiseRequestDTO(request);
                    client.AddRequest(requestString);
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() => DisplayStatusMessage(false, "ERROR: " + ex.Message));
                }
            }
            else
            {
                Loans_Datagrid.ItemsSource = null;
            }
        }

        public void RenewLoan_Click(object sender, RoutedEventArgs e)
        {
            var selectedLoan = Loans_Datagrid.SelectedItem as LoanDTO;

            if (selectedLoan != null)
            {
                member_id = selectedLoan.Member.ID;
                book_id = selectedLoan.Book.ID;
                DoRenewLoan();
            }
            else
            {
                Dispatcher.Invoke(() => DisplayStatusMessage(false, " Please select an active loan."));
            }
        }

        public void DoRenewLoan()
        {
            try
            {
                RequestDTO request = new RequestDTO(clientID, CLIENT_FUNCTION, acceptsCommandsFlag, acceptsMessagesFlag, RequestUseCase.RENEW_LOAN, false, member_id, book_id, "");
                string requestString = client.serialiseRequestDTO(request);
                client.AddRequest(requestString);
            }
            catch (Exception e)
            {
                Dispatcher.Invoke(() => DisplayStatusMessage(false, "ERROR: " + e.Message));
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            client.Stop();
            this.Close();
        }
    }
}
