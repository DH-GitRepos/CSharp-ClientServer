using UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using DH_Server.Commands;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Text;

namespace DH_Server
{
    class DH_Client_Service
    {
        // CONNECTION PROPERTIES
        private Socket socket { get; set; }
        private NetworkStream stream { get; set; }
        public StreamReader reader { get; private set; }
        public StreamWriter writer { get; private set; }
        private BlockingCollection<string> responses { get; set; }
        private BlockingCollection<string> requests { get; set; }
        private BlockingCollection<string> broadcastMessages { get; set; }
        private RemoveClient removeMe { get; set; }
        private BroadcastMessageToAllClients BroadcastMessage { get; set; }
        private BroadcastCommandToAllClients BroadcastCommand { get; set; }
        private JsonSerializerOptions options { get; set; }
        private bool serviceRunning { get; set; }
        private bool updateViewsFlag { get; set; }
        public bool AcceptsCommandsFlag { get; set; }
        public bool AcceptsMessagesFlag { get; set; }
        public string ClientID { get; set; }
        public string ClientType { get; set; }

        private const int MAXIMUM_RENEWALS = 3;

        public DH_Client_Service(Socket socket, RemoveClient rc, BroadcastMessageToAllClients broadcastMessage, BroadcastCommandToAllClients broadcastCommand)
        {
            this.socket = socket;
            this.removeMe = rc;
            this.BroadcastMessage = broadcastMessage;
            this.BroadcastCommand = broadcastCommand;
  
            this.options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };

            this.stream = new NetworkStream(socket);
            this.reader = new StreamReader(stream, Encoding.UTF8);
            this.writer = new StreamWriter(stream, Encoding.UTF8);
            this.responses = new BlockingCollection<string>();
            this.requests = new BlockingCollection<string>();
            this.broadcastMessages = new BlockingCollection<string>();

            this.serviceRunning = false;
            this.updateViewsFlag = false;
            this.ClientID = "";
            this.ClientType = "";
            this.AcceptsCommandsFlag = false;
            this.AcceptsMessagesFlag = false;
        }

        // INTERACT WITH CLIENT
        public async Task RunAsync()
        {
            serviceRunning = true;
            
            _ = Task.Run(ProcessClientRequestsAsync); // THREAD TO PROCESS REQUESTS
            _ = Task.Run(ProcessServerResponsesAsync); // THREAD TO PROCESS RESPONSES
            _ = Task.Run(ProcessBroadcastMessagesAsync); // THREAD TO PROCESS BROADCAST MESSAGES
             
            try
            {
                string clientRequest = await reader.ReadLineAsync();
                while (clientRequest != null)
                {
                    AddRequest(clientRequest);
                    clientRequest = await reader.ReadLineAsync();
                }
                
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            catch (Exception e) 
            {             
                // This will catch any other exceptions
                Console.WriteLine("An error occurred: " + e.Message);
            }
            finally // executed regardless of whether an exception was thrown or not
            {
                string clientEndpoint = socket.RemoteEndPoint.ToString();
                Console.WriteLine($"GOODBYE: From \"{ClientType}\" on client ID:{ClientID} @ {clientEndpoint}\n");
                Close();
            }            
        }


        private async Task ProcessClientRequestsAsync()
        {
            foreach (var request in requests.GetConsumingEnumerable())
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"SERVER SAYS:\nPROCESSING CLIENT REQUEST.\nQueue currently contains {requests.Count} requests.\n");

                    RequestDTO requestDTO = DeserialiseRequestDTO(request);

                    if (requestDTO.OpCode == RequestUseCase.BROADCAST_MESSAGE)
                    {
                        string incomingMessage = requestDTO.Message;
                        string broadcastMessage = string.Format(
                                "(CLIENT): {0}: {1}",
                                socket.RemoteEndPoint.ToString(),
                                incomingMessage);

                        AddBroadcastMessage(broadcastMessage);
                    }
                    else
                    {
                        ResponseDTO responseDTO = ProcessClientMessage(requestDTO);
                        string response = SerialiseResponseDTO(responseDTO);
                        AddResponse(response);
                    }
                });
            }
        }

        private async Task ProcessServerResponsesAsync()
        {
            foreach (var response in responses.GetConsumingEnumerable())
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"SERVER SAYS:\nPROCESSING SERVER REQUEST.\nQueue currently contains {responses.Count} requests.\n");

                    lock (writer)
                    {
                        writer.WriteLine(response);
                        writer.Flush();
                    }
                });
            }
        }

        private async Task ProcessBroadcastMessagesAsync()
        {
            foreach (var broadcastMessage in broadcastMessages.GetConsumingEnumerable())
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"SERVER SAYS:\nPROCESSING BROADCAST MESSAGES.\nQueue currently contains {broadcastMessages.Count} requests.\n");

                    BroadcastMessage(broadcastMessage);
                });
            }
        }

        // ADD RESPONSE DTOS TO THE RESPONSE QUEUE FOR PUSHING TO THE CLIENT
        public void AddResponse(string response)
        {
            responses.Add(response);
        }

        // ADD REQUEST DTOS TO THE RESPONSE QUEUE FOR SERVER PROCESSING
        public void AddRequest(string request)
        {
            requests.Add(request);
        }

        // ADD BROADCAST MESSAGES TO THE BROADCAST QUEUE FOR PUSHING TO THE CLIENT
        public void AddBroadcastMessage(string message)
        {
            broadcastMessages.Add(message);
        }

        // BROADCAST MESSAGE
        public void SendBroadcastMessageToClient(string message)
        {
            ResponseDTO responseData = BlankRepsonseDTO();
            responseData.OpCode = ReturnOpCode.BROADCAST_MESSAGE;
            responseData.Status = true;
            responseData.StatusMessage = "BROADCAST MESSAGE: ";
            responseData.BroadcastMessages = new List<string>() { message };

            string responseString = SerialiseResponseDTO(responseData);
            AddResponse(responseString);
        }

        // PROCESS CLIENT MESSAGE
        private ResponseDTO ProcessClientMessage(RequestDTO requestDTO)
        {
            ResponseDTO response = BlankRepsonseDTO();
                        
            try
            {
                // PROCESS THE REQUEST BASED ON THE OPCODE
                // - DO SCREEN INITIALISATION TASKS
                if (requestDTO.OpCode == RequestUseCase.REGISTER_CLIENT)
                {
                    // ASSIGN CLIENT PARAMETERS TO THE ENVIRONMENT
                    ClientID = requestDTO.ClientID;
                    ClientType = requestDTO.ClientType;
                    AcceptsCommandsFlag = requestDTO.AcceptingCommands;
                    AcceptsMessagesFlag = requestDTO.AcceptingMessages;

                    // SET RETURN OPCODE
                    response.OpCode = ReturnOpCode.CLIENT_INITIALISED;
                    response.ClientID = ClientID;
                    response.Status = true;

                    switch (requestDTO.ClientType)
                    {
                        case "main_menu":
                        case "test_client_1":
                        case "test_client_2":
                            response.StatusMessage = $"Client ID:( {requestDTO.ClientID} ) initialised";
                            break;

                        case "borrow_book":
                            response.Books = ICommandInterface.GetBooksList();
                            response.Members = ICommandInterface.GetMembersList();
                            response.StatusMessage = "Borrow book initialised";
                            break;

                        case "return_book":
                            response.Members = ICommandInterface.GetMembersList();
                            response.StatusMessage = "Return book initialised";
                            break;

                        case "renew_loan":
                            response.Members = ICommandInterface.GetMembersList();
                            response.StatusMessage = "Renew loan initialised";
                            break;

                        case "view_all_books":
                            response.Books = ICommandInterface.GetBooksList();
                            if (response.Books.Count < 1)
                            {
                                response.StatusMessage = "No books found.";
                                response.Status = false;
                            }
                            else
                            {
                                response.StatusMessage = $"{response.Books.Count} books(s) found.";
                            }
                            break;

                        case "view_all_members":
                            response.Members = ICommandInterface.GetMembersList();
                            if (response.Members.Count < 1)
                            {
                                response.StatusMessage = "No members found.";
                                response.Status = false;
                            }
                            else
                            {
                                response.StatusMessage = $"{response.Members.Count} member(s) found.";
                            }
                            break;

                        case "view_all_loans":
                            response.Loans = ICommandInterface.GetLoansList();
                            if(response.Loans.Count < 1)
                            {
                                response.StatusMessage = "No active loans.";
                                response.Status = false;                                
                            }
                            else
                            {
                                response.StatusMessage = $"{response.Loans.Count} active loan(s).";
                            }
                            break;

                        default:
                            response.OpCode = ReturnOpCode.ERROR;
                            response.Status = false;
                            response.StatusMessage = "Invalid OpCode";
                            break;
                    }
                }
                else
                {
                    // PROCESS THE REQUEST BASED ON THE OPCODE
                    // - PROCESS OPERATION REQUESTS

                    switch (requestDTO.OpCode)
                    {
                        case RequestUseCase.BORROW_BOOK:
                            AcceptsCommandsFlag = false;
                            response.OpCode = ReturnOpCode.BORROW_BOOK;
                            bool canBorrowBook = ICommandInterface.CheckBookIsAvailable(requestDTO.BookID);

                            if (canBorrowBook)
                            {
                                bool BookBorrowedSuccessfully = ICommandInterface.BorrowBook(requestDTO.MemberID, requestDTO.BookID);

                                if (BookBorrowedSuccessfully)
                                {
                                    response.StatusMessage = "Book borrowed.";
                                    response.Status = true;
                                    response.Members = ICommandInterface.GetMembersList();
                                    response.Books = ICommandInterface.GetBooksList();

                                    // SEND UPDATE BROADCAST COMMAND HERE 
                                    List<LoanDTO> loanList = ICommandInterface.GetLoansByMemberID(requestDTO.MemberID);
                                    LoanDTO loanToUpdate = loanList.Find(l => l.Book.ID == requestDTO.BookID);
                                    BookDTO bookToUpdate = response.Books.Find(b => b.ID == requestDTO.BookID);

                                    ResponseDTO updateViewCommandDTO = BlankRepsonseDTO();
                                    updateViewCommandDTO.OpCode = ReturnOpCode.UPDATE_VIEW_COMMAND;
                                    updateViewCommandDTO.Book = bookToUpdate;
                                    updateViewCommandDTO.Loan = loanToUpdate;
                                    string updateViewCommandString = SerialiseResponseDTO(updateViewCommandDTO);
                                    BroadcastCommand(updateViewCommandString);
                                }
                                else
                                {
                                    response.StatusMessage = "Borrow book failed.";
                                    response.Status = false;
                                }
                            }
                            else
                            {
                                response.StatusMessage = "Book unavailable for loan.";
                                response.Status = false;
                            }

                            AcceptsCommandsFlag = true;
                            break;

                        case RequestUseCase.RETURN_BOOK:
                            AcceptsCommandsFlag = false;
                            response.OpCode = ReturnOpCode.RETURN_BOOK;
                            bool BookReturnedSuccessfully = ICommandInterface.ReturnBook(requestDTO.MemberID, requestDTO.BookID);

                            if (BookReturnedSuccessfully)
                            {
                                response.StatusMessage = "Book returned.";
                                response.Status = true;
                                response.Members = ICommandInterface.GetMembersList();
                                response.Loans = ICommandInterface.GetLoansList();

                                // SEND UPDATE BROADCAST COMMAND HERE 
                                List<LoanDTO> loanList = ICommandInterface.GetLoansList();
                                List<BookDTO> bookList = ICommandInterface.GetBooksList();
                                BookDTO bookToUpdate = bookList.Find(b => b.ID == requestDTO.BookID);
                                LoanDTO loanToUpdate = loanList.Find(l => l.Book.ID == requestDTO.BookID);

                                ResponseDTO updateViewCommandDTO = BlankRepsonseDTO();
                                updateViewCommandDTO.OpCode = ReturnOpCode.UPDATE_VIEW_COMMAND;
                                updateViewCommandDTO.Book = bookToUpdate;
                                updateViewCommandDTO.Loan = loanToUpdate;
                                updateViewCommandDTO.Loans = ICommandInterface.GetLoansList();

                                string updateViewCommandString = SerialiseResponseDTO(updateViewCommandDTO);
                                BroadcastCommand(updateViewCommandString);
                            }
                            else
                            {                                
                                response.StatusMessage = "Return book failed.";
                                response.Status = false;
                                response.Members = ICommandInterface.GetMembersList();
                                response.Loans = ICommandInterface.GetLoansList();
                            }
                            AcceptsCommandsFlag = true;
                            break;

                        case RequestUseCase.RETURN_BOOK_UPDATE:
                            AcceptsCommandsFlag = false;
                            response.OpCode = ReturnOpCode.RETURN_BOOK_UPDATE;
                            response.Members = ICommandInterface.GetMembersList();
                            response.Loans = ICommandInterface.GetLoansByMemberID(requestDTO.MemberID);

                            if (response.Loans.Count < 1)
                            {
                                response.StatusMessage = $"No active loans for user ID:{requestDTO.MemberID}.";
                                response.Status = false;
                            }
                            else
                            {
                                response.StatusMessage = $"{response.Loans.Count} active loan(s) for user ID:{requestDTO.MemberID}.";
                                response.Status = true;
                            }
                            AcceptsCommandsFlag = true;
                            break;

                        case RequestUseCase.RENEW_LOAN:
                            AcceptsCommandsFlag = false;
                            response.OpCode = ReturnOpCode.RENEW_LOAN;
                            LoanDTO currentLoan = ICommandInterface.GetCurrentLoan(requestDTO.MemberID, requestDTO.BookID);

                            if (currentLoan.NumberOfRenewals >= MAXIMUM_RENEWALS)
                            {
                                response.Members = ICommandInterface.GetMembersList();
                                response.Loans = ICommandInterface.GetLoansByMemberID(requestDTO.MemberID);
                                response.StatusMessage = $"Maximum renewals reached for book ID:{requestDTO.BookID}.";
                                response.Status = false;
                                break;
                            }
                            else
                            {
                                bool BookRenewedSuccessfully = ICommandInterface.RenewLoan(requestDTO.MemberID, requestDTO.BookID);

                                if (BookRenewedSuccessfully)
                                {
                                    response.Members = ICommandInterface.GetMembersList();
                                    response.Loans = ICommandInterface.GetLoansList();
                                    response.StatusMessage = $"Book loan renewed for book ID:{requestDTO.BookID}.";
                                    response.Status = true;

                                    // SEND UPDATE BROADCAST COMMAND HERE 
                                    List<LoanDTO> loanList = ICommandInterface.GetLoansByMemberID(requestDTO.MemberID);
                                    List<BookDTO> bookList = ICommandInterface.GetBooksList();
                                    BookDTO bookToUpdate = bookList.Find(b => b.ID == requestDTO.BookID);
                                    LoanDTO loanToUpdate = loanList.Find(l => l.Book.ID == requestDTO.BookID);

                                    ResponseDTO updateViewCommandDTO = BlankRepsonseDTO();
                                    updateViewCommandDTO.OpCode = ReturnOpCode.UPDATE_VIEW_COMMAND;
                                    updateViewCommandDTO.Book = bookToUpdate;
                                    updateViewCommandDTO.Loan = loanToUpdate;
                                    updateViewCommandDTO.Loans = ICommandInterface.GetLoansList();

                                    string updateViewCommandString = SerialiseResponseDTO(updateViewCommandDTO);
                                    BroadcastCommand(updateViewCommandString);
                                }
                                else
                                {
                                    response.Members = ICommandInterface.GetMembersList();
                                    response.Loans = ICommandInterface.GetLoansByMemberID(requestDTO.MemberID);
                                    response.StatusMessage = "Renew loan failed.";
                                    response.Status = false;
                                }
                            }
                            AcceptsCommandsFlag = true;
                            break;

                        case RequestUseCase.RENEW_LOAN_UPDATE:
                            AcceptsCommandsFlag = false;
                            response.OpCode = ReturnOpCode.RENEW_LOAN_UPDATE;
                            response.Members = ICommandInterface.GetMembersList();
                            response.Loans = ICommandInterface.GetLoansByMemberID(requestDTO.MemberID);
                            if (response.Loans.Count < 1)
                            {
                                response.StatusMessage = $"No active loans for user ID:{requestDTO.MemberID}.";
                                response.Status = false;
                            }
                            else
                            {
                                response.StatusMessage = $"{response.Loans.Count} active loan(s) for user ID:{requestDTO.MemberID}.";
                                response.Status = true;
                            }
                            AcceptsCommandsFlag = true;
                            break;
                    }
                }

                return response;

            }
            catch (KeyNotFoundException e)
            {
                response.OpCode = ReturnOpCode.ERROR;
                response.Status = false;
                response.StatusMessage = e.Message;
                return response;
            }
        }

        // DESERIALISE CLIENT REQUEST
        public RequestDTO DeserialiseRequestDTO(string clientRequestString)
        {
            Console.WriteLine($"CLIENT REQUEST:\n{clientRequestString}\n");
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            try
            {
                RequestDTO output = JsonSerializer.Deserialize<RequestDTO>(clientRequestString, options);
                return output;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR(DeserialiseRequestDTO): " + e.Message);
                return null;
            }
        }

        // SERIALISE SERVER RESPONSE
        public string SerialiseResponseDTO(ResponseDTO responseDTO)
        {
            string response = JsonSerializer.Serialize(responseDTO);
            Console.WriteLine($"SERVER SAYS:\n{response}\n");
            return response;
        }
        
        // CLOSE THE CONNECTION
        public void Close()
        {
            try
            {
                serviceRunning = false;
                removeMe(this);
                socket.Shutdown(SocketShutdown.Both);
            }
            finally
            {
                socket.Close();
            }
        }

        public ResponseDTO BlankRepsonseDTO()
        {
            string opCode = "";
            string clientID = "";
            List<BookDTO> books = new List<BookDTO>();
            List<MemberDTO> members = new List<MemberDTO>();
            List<LoanDTO> loans = new List<LoanDTO>();
            BookDTO book = new BookDTO(-1, "", "", "", "");
            MemberDTO member = new MemberDTO(-1, "");
            LoanDTO loan = new LoanDTO(-1, member, book, new DateTime(), new DateTime(), new DateTime(), -1);
            bool status = false;
            string statusMessage = "";
            List<string> braodcastMessages = new List<string>();

            ResponseDTO response = new ResponseDTO(opCode, clientID, books, members, loans, book, member, loan, status, statusMessage, braodcastMessages);
            return response;
        }
    }
}
