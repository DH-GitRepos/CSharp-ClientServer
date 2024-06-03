using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using DH_GUIClient.DTO;

namespace DH_GUIClient
{
    /// <summary>
    /// Interaction logic for BorrowBookWindow.xaml
    /// </summary>
    /// 
    public partial class BorrowBookWindow : Window
    {
        private WPFClient client { get; set; }
        private Task clientTask { get; set; }
        private Dispatcher dispatcher { get; set; }
        private int book_id { get; set; }
        private int member_id { get; set; }
        private string member_name { get; set; }
        private string book_author { get; set; }
        private string book_title { get; set; }
        private string book_ISBN { get; set; }
        private string book_State { get; set; }
        private string clientID { get; set; }
        private bool acceptsCommandsFlag { get; set; }
        private bool acceptsMessagesFlag { get; set; }
        private ObservableCollection<BookDTO> bookCollection { get; set; }
        private ObservableCollection<MemberDTO> memberCollection { get; set; }

        private const string CLIENT_FUNCTION = "borrow_book";

        public BorrowBookWindow(string ClientID)
        {
            InitializeComponent();
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.acceptsCommandsFlag = true;
            this.acceptsMessagesFlag = false;
            this.clientID = ClientID;
            this.bookCollection = new ObservableCollection<BookDTO>();
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
            this.book_author = String.Empty;
            this.book_title = String.Empty;
            this.book_ISBN = String.Empty;
            this.book_State = String.Empty;
            this.member_name = String.Empty;
        }

        private void DisplayResponseData(ResponseDTO data)
        {
            bool operationSuccess = data.Status;
            Dispatcher.Invoke(() => DisplayStatusMessage(operationSuccess, data.StatusMessage));
            
            if (operationSuccess)
            {
                memberCollection = new ObservableCollection<MemberDTO>((IEnumerable<MemberDTO>)data.Members);
                Dispatcher.Invoke(() => Members_Datagrid.ItemsSource = memberCollection);
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

        public void BorrowBook_Click(object sender, RoutedEventArgs e) 
        {
            BookDTO? selectedBook = Books_Datagrid.SelectedItem as BookDTO;
            string fields_not_selected_msg = string.Empty;
            bool both_fields_selected = true;

            MemberDTO? selectedMember = Members_Datagrid.SelectedItem as MemberDTO;

            if (selectedMember != null)
            {
                member_id = selectedMember.ID;
                member_name = selectedMember.Name;
            }
            else
            {
                both_fields_selected = false;
                fields_not_selected_msg += "Please select a member.";
            }

            if (selectedBook != null)
            {
                book_id = selectedBook.ID;
                book_author = selectedBook.Author;
                book_title = selectedBook.Title;
                book_ISBN = selectedBook.ISBN;
                book_State = selectedBook.State;
            } 
            else
            {
                both_fields_selected = false;
                fields_not_selected_msg += " Please select a book.";
            }        

            if (both_fields_selected)
            {
                try 
                { 
                    DoBorrowBook();
                } 
                catch (Exception ex) 
                {
                    Dispatcher.Invoke(() => DisplayStatusMessage(false, "\nERROR: " + ex.Message));
                }

            } else
            {
                Dispatcher.Invoke(() => DisplayStatusMessage(false, fields_not_selected_msg));
            }  
        }

        public void DoBorrowBook()
        {
            try
            {
                RequestDTO request = new RequestDTO(clientID, CLIENT_FUNCTION, false, acceptsMessagesFlag, RequestUseCase.BORROW_BOOK, false, member_id, book_id, "");
                string requestString = client.serialiseRequestDTO(request);
                client.AddRequest(requestString);
            }
            catch (Exception e)
            {
                Dispatcher.Invoke(() => DisplayStatusMessage(false, "ERROR: " + e.Message));
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            client.Stop();
            this.Close();
        }

    }
}
