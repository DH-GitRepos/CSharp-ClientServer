using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DH_GUIClient.DTO;

namespace DH_GUIClient
{
    /// <summary>
    /// Interaction logic for ViewAllMembers.xaml
    /// </summary>

    public partial class ViewAllMembers : Window
    {
        private WPFClient client { get; set; }
        private Task clientTask { get; set; }
        private Dispatcher dispatcher { get; set; }
        private string clientID { get; set; }
        private bool acceptsCommandsFlag { get; set; }
        private bool acceptsMessagesFlag { get; set; }
        private ObservableCollection<MemberDTO> memberCollection { get; set; }

        private const string CLIENT_FUNCTION = "view_all_members";

        public ViewAllMembers(string ClientID)
        {
            InitializeComponent();
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.acceptsCommandsFlag = false;
            this.acceptsMessagesFlag = false;
            this.clientID = ClientID;
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
        }

        private void DisplayResponseData(ResponseDTO data)
        {
            bool operationSuccess = data.Status;
            Dispatcher.Invoke(() => DisplayStatusMessage(operationSuccess, data.StatusMessage));

            if (operationSuccess)
            {
                memberCollection = new ObservableCollection<MemberDTO>((IEnumerable<MemberDTO>)data.Members);
                Dispatcher.Invoke(() => Members_Datagrid.ItemsSource = memberCollection);
            }            
        }

        private void ProcessViewUpdateCommand(ResponseDTO data) { /* NOT IMPLEMENTED FOR VIEWALLMEMBERS */ }

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
