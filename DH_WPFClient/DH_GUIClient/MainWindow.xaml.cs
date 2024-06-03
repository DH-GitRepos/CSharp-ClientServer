using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DH_GUIClient.DTO;

namespace DH_GUIClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///  

    public partial class MainWindow : Window
    {
        private WPFClient client { get; set; }
        private Task clientTask { get; set; }
        private Dispatcher dispatcher { get; set; }
        private string clientID { get; set; }
        private bool acceptsCommandsFlag { get; set; }
        private bool acceptsMessagesFlag { get; set; }

        private const string CLIENT_FUNCTION = "main_menu";

        public MainWindow()
        {
            InitializeComponent();
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.acceptsCommandsFlag = false;
            this.acceptsMessagesFlag = true;
            
            this.client = new WPFClient(DisplayResponseData,
                                   DisplayBroadcastMessageAsync,
                                   SendBroadcastMessage,
                                   ShowMessage,
                                   DisplayStatusMessage,
                                   ProcessViewUpdateCommand,
                                   dispatcher,
                                   CLIENT_FUNCTION,
                                   acceptsCommandsFlag,
                                   acceptsMessagesFlag);

            this.clientTask = Task.Run(client.RunAsync);

            this.clientID = client.clientID;
            CID_ID.Content = clientID;
        }

        private void DisplayResponseData(ResponseDTO data)
        {
            if (data.OpCode == ReturnOpCode.CLIENT_INITIALISED)
            {
                Dispatcher.Invoke(() => DisplayStatusMessage(true, data.StatusMessage));
            }
        }

        private void ProcessViewUpdateCommand(ResponseDTO data) { /* NOT IMPLMENTED IN MAIN WINDOW */ }

        private void UpdateDataTables(ResponseDTO data)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, "RUNNING -> UPDATING DATA TABLES"));
        }

        private async void DisplayBroadcastMessageAsync(List<string> msg_list)
        {
            foreach (var msg in msg_list)
            {
                await DispatcherBeginInvokeAsync(() => Display_Broadcast_Text.Text += $"{msg}\n");
            }
            await DispatcherBeginInvokeAsync(() => Display_Broadcast_Text.ScrollToEnd());
        }

        // Helper function for DisplayBroadcastMessageAsync calls
        private Task DispatcherBeginInvokeAsync(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();
            var dispatcherOperation = Dispatcher.BeginInvoke(action);
            dispatcherOperation.Completed += (s, e) =>
            {
                tcs.SetResult(true);
            };
            return tcs.Task;
        }

        private void SendBroadcastMessage()
        {
            string msg = Send_Broadcast_Text.Text;
            
            RequestDTO request = new RequestDTO(
                clientID, CLIENT_FUNCTION, acceptsCommandsFlag, acceptsMessagesFlag, RequestUseCase.BROADCAST_MESSAGE,false,-1,-1,msg);

            string requestString = client.serialiseRequestDTO(request);
            client.AddRequest(requestString);
        }

        private void ShowMessage(string msg) 
        {
            MessageBox.Show(msg);
        }

        private void DisplayStatusMessage(bool operationSuccess, string msg)
        {
            if (operationSuccess)
            {
                Dispatcher.Invoke(() => Main_SystemMessages.Foreground = new SolidColorBrush(Colors.MediumSeaGreen));
            }
            else
            {
                Dispatcher.Invoke(() => Main_SystemMessages.Foreground = new SolidColorBrush(Colors.Red));
            }
            Dispatcher.Invoke(() => Main_SystemMessages.Text = msg);
        }

        public void Click_SendBroadcastMessage(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, "OPENED -> SENDING BROADCAST MESSAGE"));
            SendBroadcastMessage();        
        }

        public void Click_BorrowBook(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, "OPENED -> BORROW BOOK FUNCTION"));
            BorrowBookWindow borrowBookWindow = new(clientID);
            borrowBookWindow.Show(); // Use ShowDialog() if you want the window to be modal.

            // Disable the button, re-enable the button when the new window is closed
            Loans_BorrowBook.IsEnabled = false;
            borrowBookWindow.Closed += (s, args) => Loans_BorrowBook.IsEnabled = true;
        }

        public void Click_ReturnBook(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, "OPENED -> RETURN BOOK FUNCTION"));
            ReturnBookWindow returnBookWindow = new(clientID);
            returnBookWindow.Show();

            Loans_ReturnBook.IsEnabled = false;
            returnBookWindow.Closed += (s, args) => Loans_ReturnBook.IsEnabled = true;
        }

        public void Click_RenewLoan(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, "OPENED -> RENEW LOAN FUNCTION"));
            RenewLoanWindow renewLoanWindow = new(clientID);
            renewLoanWindow.Show();

            Loans_RenewLoan.IsEnabled = false;
            renewLoanWindow.Closed += (s, args) => Loans_RenewLoan.IsEnabled = true;
        }

        public void Click_ViewAllBooks(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, "OPENED -> VIEW ALL BOOKS REPORT"));
            ViewAllBooks viewAllBooks = new(clientID);
            viewAllBooks.Show();

            Reports_ViewAllBooks.IsEnabled = false;
            viewAllBooks.Closed += (s, args) => Reports_ViewAllBooks.IsEnabled = true;
        }

        public void Click_ViewAllMembers(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, "OPENED -> VIEW ALL MEMBERS REPORT"));
            ViewAllMembers viewAllMembers = new(clientID);
            viewAllMembers.Show();

            Reports_ViewAllMembers.IsEnabled = false;
            viewAllMembers.Closed += (s, args) => Reports_ViewAllMembers.IsEnabled = true;
        }

        public void Click_ViewAllLoans(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, "OPENED -> VIEW ALL LOANS REPORT"));
            ViewAllLoans viewAllLoans = new(clientID);
            viewAllLoans.Show();

            Reports_ViewAllLoans.IsEnabled = false;
            viewAllLoans.Closed += (s, args) => Reports_ViewAllLoans.IsEnabled = true;
        }

        public void Click_MultiClient(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, "OPENED -> MULTI-CLIENT TEST"));
            MultiClientWindow multiClientWindow = new();
            multiClientWindow.Show();

            MultiClientTest_Button.IsEnabled = false;
            multiClientWindow.Closed += (s, args) => MultiClientTest_Button.IsEnabled = true;
        }

        public void Click_ExitApplication(object sender, RoutedEventArgs e)
        {
            var ev = new CancelEventArgs();
            ConfirmClosing(ev);

            if (ev.Cancel)
            {
                ShutdownApplication();
            }
            else
            {
                OnClosed(EventArgs.Empty);
            }
        }        

        public void MessagePopup(string msg)
        {
            MessageBox.Show(this, msg);
        }

        private void ConfirmClosing(CancelEventArgs e)
        {
            MessageBoxResult result =
                MessageBox.Show(
                    this,
                    "Are you sure?",
                    "Confirm closure",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            e.Cancel = result == MessageBoxResult.Yes;
        }

        private void ShutdownApplication()
        {
            client.Stop();
            Application.Current.Shutdown();
            // this.Close(); ///--> This just closes the current window, does not terminate the whole application.
        }
    }
}