using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DH_GUIClient.DTO;

namespace DH_GUIClient
{
    /// <summary>
    /// Interaction logic for MultiClientWindow.xaml
    /// </summary>
    ///  

    public partial class MultiClientWindow : Window
    {
        private WPFClient client1 { get; set; }
        private WPFClient client2 { get; set; }
        private Task clientTask1 { get; set; }        
        private Task clientTask2 { get; set; }
        private string clientID1 { get; set; }
        private string clientID2 { get; set; }
        private Dispatcher dispatcher { get; set; }
        private bool acceptsCommandsFlag { get; set; }
        private bool acceptsMessagesFlag { get; set; }

        private const string CLIENT_FUNCTION_1 = "test_client_1";

        private const string CLIENT_FUNCTION_2 = "test_client_2";

        public MultiClientWindow()
        {
            InitializeComponent();
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.acceptsCommandsFlag = false;
            this.acceptsMessagesFlag = true;

            this.client1 = new WPFClient(DisplayResponseDataC1,
                                   DisplayBroadcastMessageC1Async,
                                   SendBroadcastMessageC1,
                                   ShowMessage,
                                   DisplayStatusMessageC1,
                                   ProcessViewUpdateCommandC1,
                                   dispatcher,
                                   CLIENT_FUNCTION_1,
                                   acceptsCommandsFlag,
                                   acceptsMessagesFlag);

            this.client2 = new WPFClient(DisplayResponseDataC2,
                                   DisplayBroadcastMessageC2Async,
                                   SendBroadcastMessageC2,
                                   ShowMessage,
                                   DisplayStatusMessageC2,
                                   ProcessViewUpdateCommandC2,
                                   dispatcher,
                                   CLIENT_FUNCTION_2,
                                   acceptsCommandsFlag,
                                   acceptsMessagesFlag);

            this.clientTask1 = Task.Run(client1.RunAsync);
            this.clientTask2 = Task.Run(client2.RunAsync);

            this.clientID1 = client1.clientID;
            CID1_ID.Content = clientID1;

            this.clientID2 = client2.clientID;
            CID2_ID.Content = clientID2;
        }

        // CLIENT 1 IMPLEMENTATIONS
        private void DisplayResponseDataC1(ResponseDTO data)
        {
            if (data.OpCode == ReturnOpCode.CLIENT_INITIALISED)
            {
                Dispatcher.Invoke(() => DisplayStatusMessageC1(true, data.StatusMessage));
            }
        }
       
        private async void DisplayBroadcastMessageC1Async(List<string> msg_list)
        {
            foreach (var msg in msg_list)
            {
                 await DispatcherBeginInvokeAsync(() => Display_BroadcastC1_Text.Text += $"{msg}\n");
            }
            await DispatcherBeginInvokeAsync(() => Display_BroadcastC1_Text.ScrollToEnd());
        }
        


        private void SendBroadcastMessageC1()
        { /* NOT IMPLMENTED IN MULTI-CLIENT WINDOW */ }

        private void DisplayStatusMessageC1(bool operationSuccess, string msg)
        {
            if (operationSuccess)
            {
                Dispatcher.Invoke(() => Main_SystemMessagesC1.Foreground = new SolidColorBrush(Colors.MediumSeaGreen));
            }
            else
            {
                Dispatcher.Invoke(() => Main_SystemMessagesC1.Foreground = new SolidColorBrush(Colors.Red));
            }
            Dispatcher.Invoke(() => Main_SystemMessagesC1.Text = msg);
        }

        private void ProcessViewUpdateCommandC1(ResponseDTO data)
        { /* NOT IMPLMENTED IN MULTI-CLIENT WINDOW */ }



        // CLIENT 2 IMPLEMENTATIONS
        private void DisplayResponseDataC2(ResponseDTO data)
        {
            if (data.OpCode == ReturnOpCode.CLIENT_INITIALISED)
            {
                Dispatcher.Invoke(() => DisplayStatusMessageC2(true, data.StatusMessage));
            }
        }
        
        private async void DisplayBroadcastMessageC2Async(List<string> msg_list)
        {
            foreach (var msg in msg_list)
            {
                await DispatcherBeginInvokeAsync(() => Display_BroadcastC2_Text.Text += $"{msg}\n");
            }
            await DispatcherBeginInvokeAsync(() => Display_BroadcastC2_Text.ScrollToEnd());
        }
        
        private void SendBroadcastMessageC2()
        { /* NOT IMPLMENTED IN MULTI-CLIENT WINDOW */ }

        private void DisplayStatusMessageC2(bool operationSuccess, string msg)
        {
            if (operationSuccess)
            {
                Dispatcher.Invoke(() => Main_SystemMessagesC2.Foreground = new SolidColorBrush(Colors.MediumSeaGreen));
            }
            else
            {
                Dispatcher.Invoke(() => Main_SystemMessagesC2.Foreground = new SolidColorBrush(Colors.Red));
            }
            Dispatcher.Invoke(() => Main_SystemMessagesC2.Text = msg);
        }

        private void ProcessViewUpdateCommandC2(ResponseDTO data)
        { /* NOT IMPLMENTED IN MULTI-CLIENT WINDOW */ }

        private void ShowMessage(string msg) 
        {
            MessageBox.Show(msg);
        }        

        public void Click_RapidRequests(object sender, RoutedEventArgs e)
        {
            Task.Run(Do_RapidRequests);
        }

        public void Do_RapidRequests()
        {

            RequestDTO c1_request_initialise_view_all_books = new RequestDTO(
                clientID1, "view_all_books", acceptsCommandsFlag, acceptsMessagesFlag, RequestUseCase.REGISTER_CLIENT, true, 0, 0, "");
            RequestDTO c1_request_initialise_view_all_members = new RequestDTO(
                clientID1, "borrow_book", acceptsCommandsFlag, acceptsMessagesFlag, RequestUseCase.REGISTER_CLIENT, true, 0, 0, "");

            string c1_request_initialise_view_all_membersString = client1.serialiseRequestDTO(c1_request_initialise_view_all_members);
            string c1_request_initialise_view_all_booksString = client1.serialiseRequestDTO(c1_request_initialise_view_all_books);

            RequestDTO c2_request_initialise_view_all_books = new RequestDTO(
                clientID2, "view_all_books", acceptsCommandsFlag, acceptsMessagesFlag, RequestUseCase.REGISTER_CLIENT, true, 0, 0, "");
            RequestDTO c2_request_initialise_view_all_members = new RequestDTO(
                clientID2, "renew_loan", acceptsCommandsFlag, acceptsMessagesFlag, RequestUseCase.REGISTER_CLIENT, true, 0, 0, "");

            string c2_request_initialise_view_all_membersString = client2.serialiseRequestDTO(c2_request_initialise_view_all_members);
            string c2_request_initialise_view_all_booksString = client2.serialiseRequestDTO(c2_request_initialise_view_all_books);

            for (int i = 1; i < 251; i++)
            {
                RequestDTO c1_request_broadcast = new RequestDTO(
                clientID1, CLIENT_FUNCTION_1, acceptsCommandsFlag, acceptsMessagesFlag, RequestUseCase.BROADCAST_MESSAGE, false, -1, -1, $"CLIENT(1):TEST-{i}");
                string c1_request_broadcastString = client2.serialiseRequestDTO(c1_request_broadcast);

                RequestDTO c2_request_broadcast = new RequestDTO(
                clientID2, CLIENT_FUNCTION_1, acceptsCommandsFlag, acceptsMessagesFlag, RequestUseCase.BROADCAST_MESSAGE, false, -1, -1, $"CLIENT(2):TEST-{i}");
                string c2_request_broadcastString = client2.serialiseRequestDTO(c2_request_broadcast);

                client1.AddRequest(c1_request_initialise_view_all_membersString);
                client2.AddRequest(c2_request_initialise_view_all_membersString);
                client1.AddRequest(c1_request_initialise_view_all_booksString);
                client2.AddRequest(c2_request_initialise_view_all_booksString);
                client1.AddRequest(c1_request_broadcastString);
                client2.AddRequest(c2_request_broadcastString);

                Task.Yield();
            }

        }

        // Helper function for DisplayBroadcastMessage(C1/C2)Async calls
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

        public void MessagePopup(string msg)
        {
            MessageBox.Show(this, msg);
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            client1.Stop();
            client2.Stop();
            this.Close();
        }
    }
}