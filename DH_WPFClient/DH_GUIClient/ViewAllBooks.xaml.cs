using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DH_GUIClient.DTO;

namespace DH_GUIClient
{
    /// <summary>
    /// Interaction logic for ViewAllBooks.xaml
    /// </summary>  

    public partial class ViewAllBooks : Window
    {
        private WPFClient client { get; set; }
        private Task clientTask { get; set; }
        private Dispatcher dispatcher { get; set; }
        private string clientID { get; set; }
        private bool acceptsCommandsFlag { get; set; }
        private bool acceptsMessagesFlag { get; set; }
        private ObservableCollection<BookDTO> bookCollection { get; set; }

        private const string CLIENT_FUNCTION = "view_all_books";

        public ViewAllBooks(string ClientID)
        {
            InitializeComponent();
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.acceptsCommandsFlag = true;
            this.acceptsMessagesFlag = false;
            this.clientID = ClientID;
            this.bookCollection = new ObservableCollection<BookDTO>();

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
                bookCollection = new ObservableCollection<BookDTO>((IEnumerable<BookDTO>)data.Books);
                Dispatcher.Invoke(() => Books_Datagrid.ItemsSource = bookCollection);
            }
        }

        private void ProcessViewUpdateCommand(ResponseDTO data)
        {
            Dispatcher.Invoke(() => DisplayStatusMessage(true, $"{DateTime.Now:HH:mm:ss}: Data update received."));

            BookDTO incomingBook = data.Book;
            List<BookDTO> incomingBookCollection = data.Books;

            // If the incoming book has a valid ID, it is an update to an existing book or new book.
            if (incomingBook != null && incomingBook.ID != -1)
            {
                bool bookExists = bookCollection.Any(book => book.ID == incomingBook.ID);

                // If the book exists, update the state.
                if (bookExists)
                {
                    BookDTO? bookToUpdate = bookCollection.FirstOrDefault(book => book.ID == incomingBook.ID);
                    if (bookToUpdate != null)
                    {
                        bookToUpdate.State = incomingBook.State;
                    }
                }
                // Otherwise, add the new book to the collection.
                else
                {
                    dispatcher.Invoke(() =>
                    {
                        bookCollection.Add(incomingBook);
                        Books_Datagrid.ItemsSource = bookCollection;
                    });
                }
            }
            // Otherwise, a book has been removed. Refresh the collection.
            else if (incomingBookCollection.Count > 0)
            {
                Dispatcher.Invoke(() =>
                {
                    bookCollection = new ObservableCollection<BookDTO>((IEnumerable<BookDTO>)incomingBookCollection);
                    Books_Datagrid.ItemsSource = bookCollection;
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
