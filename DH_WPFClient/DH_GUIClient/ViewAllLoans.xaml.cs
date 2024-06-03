using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DH_GUIClient.DTO;

namespace DH_GUIClient
{
    /// <summary>
    /// Interaction logic for ViewAllLoans.xaml
    /// </summary>
 
    public partial class ViewAllLoans : Window
    {
        private WPFClient client { get; set; }
        private Task clientTask { get; set; }
        private Dispatcher dispatcher { get; set; }
        private string clientID { get; set; }
        private bool acceptsCommandsFlag { get; set; }
        private bool acceptsMessagesFlag { get; set; }
        private ObservableCollection<LoanDTO> loanCollection { get; set; }

        private const string CLIENT_FUNCTION = "view_all_loans";

        public ViewAllLoans(string ClientID)
        {
            InitializeComponent();
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.acceptsCommandsFlag = true;
            this.acceptsMessagesFlag = false;
            this.clientID = ClientID;
            this.loanCollection = new ObservableCollection<LoanDTO>();

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
        }

        private void DisplayResponseData(ResponseDTO data)
        {
            bool operationSuccess = data.Status;
            Dispatcher.Invoke(() => DisplayStatusMessage(operationSuccess, data.StatusMessage));

            if (operationSuccess)
            {
                loanCollection = new ObservableCollection<LoanDTO>((IEnumerable<LoanDTO>)data.Loans);
                Dispatcher.Invoke(() => Loans_Datagrid.ItemsSource = loanCollection);
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
                    dispatcher.Invoke(() =>
                    {
                        loanCollection.Add(incomingLoan);
                        Loans_Datagrid.ItemsSource = loanCollection;
                    });
                }
            }
            // Otherwise, a loan has been removed. Refresh the collection.
            else
            {
                Dispatcher.Invoke(() =>
                {
                    loanCollection = new ObservableCollection<LoanDTO>((IEnumerable<LoanDTO>)data.Loans);
                    Loans_Datagrid.ItemsSource = loanCollection;
                });
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

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            client.Stop();
            this.Close();
        }
    }
}
