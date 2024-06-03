using DH_GUIClient.DTO;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Text;
using System.Windows.Threading;
using System.Diagnostics;

namespace DH_GUIClient
{
    delegate void DisplayBroadcastMessageAsync(List<string> msg); // For displaying broadcast messages
    delegate void SendBroadcastMessage(); // For getting messages to broadcast from the user
    delegate void ShowMessage(string msg);
    delegate void DisplayStatusMessage(bool isError, string msg);
    delegate void DisplayResponseData(ResponseDTO data); // For displaying entity data
    delegate void ProcessViewUpdateCommand(ResponseDTO data); // For processing view update commands

    class WPFClient
    {
        // CONNECTION PROPERTIES
        private TcpClient tcpClient;
        private NetworkStream? stream;
        private StreamReader? reader;
        private StreamWriter? writer;
        private bool clientRunning;
        private Dispatcher dispatcher;

        // REQUEST/RESPONSE HANLING PROPERTIES
        public BlockingCollection<List<string>> messages;
        // private ConcurrentQueue<ResponseDTO> viewUpdates;
        private BlockingCollection<string> requests;
        private BlockingCollection<string> responses;

        // CALLBACK PROPERTIES
        private DisplayBroadcastMessageAsync DisplayBroadcastMessageAsync; // Callback function sent in by action screen
        private SendBroadcastMessage SendBroadcastMessage; // Callback function sent in by action screen
        private ShowMessage ShowMessage; // Callback function sent in by action screen
        private DisplayStatusMessage DisplayStatusMessage; // Callback function sent in by action screen
        private DisplayResponseData DisplayResponseData; // Callback function sent in by action screen
        private ProcessViewUpdateCommand ProcessViewUpdateCommand; // Callback function sent in by action screen

        // CLIENT ID PROPERTY
        public string clientID;
        public string clientFunction;
        public bool acceptsCommandsFlag;
        public bool acceptsMessagesFlag;

        public WPFClient(DisplayResponseData DisplayResponseData,
                         DisplayBroadcastMessageAsync DisplayBroadcastMessageAsync,
                         SendBroadcastMessage SendBroadcastMessage, 
                         ShowMessage ShowMessage,
                         DisplayStatusMessage DisplayStatusMessage,
                         ProcessViewUpdateCommand ProcessViewUpdateCommand,
                         Dispatcher Dispatcher,
                         string ClientFunction,
                         bool AcceptsCommandsFlag,
                         bool AcceptsMessagesFlag,
                         string ClientID = "") 
        {
            this.tcpClient = new TcpClient();
            this.stream = null;
            this.reader = null;
            this.writer = null;

            this.messages = new BlockingCollection<List<string>>();
            this.requests = new BlockingCollection<string>();
            this.responses = new BlockingCollection<string>();

            this.clientFunction = ClientFunction;
            this.acceptsCommandsFlag = AcceptsCommandsFlag;
            this.acceptsMessagesFlag = AcceptsMessagesFlag;

            if (ClientID != "")
            {
                this.clientID = ClientID;
            }
            else
            {
                this.clientID = GenerateRandomClientIDString();
            }            

            this.DisplayResponseData = DisplayResponseData;
            this.DisplayBroadcastMessageAsync = DisplayBroadcastMessageAsync;
            this.SendBroadcastMessage = SendBroadcastMessage;
            this.ShowMessage = ShowMessage;
            this.DisplayStatusMessage = DisplayStatusMessage;
            this.dispatcher = Dispatcher;
            this.ProcessViewUpdateCommand = ProcessViewUpdateCommand;
        }        

        // RUN THE CLIENT SERVICE
        public async Task RunAsync()
        {
            this.clientRunning = true;

            if (Connect("localhost", 4444))
            {
                // Start the tasks for processing client requests and server responses
                var processClientRequestsTask = Task.Run(ProcessClientRequestsAsync);
                var processServerResponsesTask = Task.Run(ProcessServerResponsesAsync);
                var processBroadcastMessagesTask = Task.Run(ProcessBroadcastMessagesAsync);

                // INITIALISE CLIENT
                RequestDTO initialiseRequest = new RequestDTO(clientID, clientFunction, acceptsCommandsFlag, acceptsMessagesFlag, RequestUseCase.REGISTER_CLIENT, true, 0, 0, "");
                string initReqStr = serialiseRequestDTO(initialiseRequest);
                AddRequest(initReqStr);
                // dispatcher.Invoke(() => DisplayStatusMessage(true, "Client ID:{} Connected to server."));

                // Read responses from the server and add them to the responses collection
                string? serverResponse;
                while (reader != null && (serverResponse = await reader.ReadLineAsync()) != null)
                {
                    bool added = responses.TryAdd(serverResponse);
                    if (!added)
                    {
                        // Handle the case where there was no request to take from the collection
                        // Wait for a short period of time before trying again
                        await Task.Delay(500); // Wait for 0.5 second
                    }
                }

                // Wait for the tasks to complete
                await processClientRequestsTask;
                await processServerResponsesTask;
                await processBroadcastMessagesTask;
            }
            else
            {
                dispatcher.Invoke(() => DisplayStatusMessage(false, "ERROR(Run): Server connection failed."));
            }
            tcpClient.Close();
        }

        // CREATE A CONNECTION TO THE SERVER
        private bool Connect(string url, int portNumber)
        {
            try
            {
                tcpClient.Connect(url, portNumber);
                stream = tcpClient.GetStream();
                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(stream, Encoding.UTF8);
            }
            catch (Exception)
            {
                DisplayStatusMessage(false, "ERROR(Connect): Unable to connect to server.");
                return false;
            }
            return true;
        }

        // READ INCOMING COMMS FROM THE SERVER
        public async Task ProcessServerResponsesAsync()
        {
            // Timer for updating the UI at a limited rate
            var uiUpdateTimer = Stopwatch.StartNew();
            var uiUpdateInterval = TimeSpan.FromMilliseconds(100);

            while (clientRunning)
            {
                if (responses.TryTake(out string? response))
                {
                    ResponseDTO? responseData = deserialiseResponseDTO(response);

                    if (responseData != null)
                    {
                        //  Process server responses on a background thread
                        // await Task.Run(() => 
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            switch (responseData.OpCode)
                            {
                                case ReturnOpCode.CLIENT_INITIALISED:
                                    if (responseData.Status)
                                    {
                                        // Update the UI at a limited rate
                                        if (uiUpdateTimer.Elapsed >= uiUpdateInterval)
                                        {
                                            // Update the UI with the response data from the background thread
                                            dispatcher.Invoke(() => DisplayResponseData(responseData));
                                        }
                                    }
                                    else
                                    {
                                        if (uiUpdateTimer.Elapsed >= uiUpdateInterval)
                                        {
                                            dispatcher.Invoke(() => DisplayStatusMessage(true, responseData.StatusMessage));
                                        }
                                    }
                                    break;

                                case ReturnOpCode.BROADCAST_MESSAGE:
                                    if (uiUpdateTimer.Elapsed >= uiUpdateInterval)
                                    {
                                        AddBroadcastMessage(responseData.BroadcastMessages);
                                    }
                                    break;

                                case ReturnOpCode.UPDATE_VIEW_COMMAND:
                                    if (uiUpdateTimer.Elapsed >= uiUpdateInterval)
                                    {
                                        dispatcher.Invoke(() => ProcessViewUpdateCommand(responseData));
                                    }
                                    break;

                                default:
                                    if (uiUpdateTimer.Elapsed >= uiUpdateInterval)
                                    {
                                        dispatcher.Invoke(() => DisplayResponseData(responseData));
                                    }
                                    break;
                            }
                        });
                    }
                }
                else
                {
                    // Handle the case where there was no request to take from the collection
                    // Wait for a short period of time before trying again
                    await Task.Delay(500); // Wait for 0.5 second
                }
            }
        }

        // PROCESS CLIENT REQUESTS TO THE SERVER
        public async Task ProcessClientRequestsAsync()
        {
            while (clientRunning)
            {
                if (requests.TryTake(out string? request))
                {
                    await Task.Run(async () =>
                    {
                        await SendRequestToServerAsync(request);
                    });
                }
                else
                {
                    // Handle the case where there was no request to take from the collection
                    // Wait for a short period of time before trying again
                    await Task.Delay(500); // Wait for 0.5 second
                }
            }
        }

        // DISPLAY BROADCAST MESSAGES FROM THE SERVER
        public async Task ProcessBroadcastMessagesAsync()
        {
            while (clientRunning)
            {
                List<string> messageList = await Task.Run(() => messages.Take());
               
                await Task.Run(() =>
                {
                    dispatcher.Invoke(() => DisplayBroadcastMessageAsync(messageList));
                });
                
            }
        }

        // SEND SERIALISED REQUESTS TO THE SERVER
        public async Task SendRequestToServerAsync(string request)
        {
            try
            {
                if (writer != null)
                {
                    await Task.Run(async () =>
                    {
                        await writer.WriteAsync(request + "\n");
                        await writer.FlushAsync();
                    });
                }
            }
            catch (Exception e)
            {
                DisplayStatusMessage(false, "ERROR(sendRequestToServer): " + e.Message);
            }
        }

        // ADD RESPONSE DTOS TO THE RESPONSE QUEUE FOR PUSHING TO THE CLIENT
        public void AddResponse(string responseStr)
        {
            responses.Add(responseStr);
        }

        // ADD REQUEST DTOS TO THE RESPONSE QUEUE FOR SERVER PROCESSING
        public void AddRequest(string requestStr)
        {
            requests.Add(requestStr);
        }

        // ADD BROADCAST MESSAGES TO THE BROADCAST QUEUE FOR PUSHING TO THE CLIENT
        public void AddBroadcastMessage(List<string> msg)
        {
            messages.Add(msg);
        }

        // DESERIALISE SERVER RESPONSES
        public ResponseDTO? deserialiseResponseDTO(string serverResponseString)
        {
            try
            {
                ResponseDTO? output = JsonSerializer.Deserialize<ResponseDTO>(serverResponseString);
                return output;
            }
            catch(Exception e)
            {
                dispatcher.Invoke(() => DisplayStatusMessage(false, "ERROR(deserialiseResponseDTO): " + e.Message));
                return null;
            } 
        }

        // SERIALISE SERVER REQUESTS
        public string serialiseRequestDTO(RequestDTO requestDTO)
        {
            string request = JsonSerializer.Serialize(requestDTO);
            return request;
        }

        // STOP THE CLIENT SERVICE
        public void Stop()
        {
            clientRunning = false;
            tcpClient.Close();
        }

        // GENERATE CLIENT ID
        public string GenerateRandomClientIDString()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string rand_chars_1 = ShuffleString(chars);
            var stringChars_1 = new char[4];
            var stringChars_2 = new char[4];
            var random = new Random();

            for (int i = 0; i < stringChars_1.Length; i++)
            {
                stringChars_1[i] = rand_chars_1[random.Next(rand_chars_1.Length)];
            }

            string rand_chars_2 = ShuffleString(rand_chars_1);

            for (int i = 0; i < stringChars_2.Length; i++)
            {
                stringChars_2[i] = rand_chars_2[random.Next(rand_chars_2.Length)];
            }

            string chars1 = new string(stringChars_1);
            string chars2 = new string(stringChars_2);

            string returnString = $"{chars1}-{chars2}";
            return returnString;
        }

        // SHUFFLE A STRING
        // - Fisher-Yates shuffle algorithm
        public string ShuffleString(string input)
        {
            var array = input.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }
    }
}
