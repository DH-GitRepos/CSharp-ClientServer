using DH_GUIClientComms.DTOs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace DH_GUIClientComms
{
    public class ClientComms
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private bool clientRunning;

        private ConcurrentQueue<List<string>> messages;
        private BlockingCollection<char> commands;         

        private DisplayBroadcastMessage DisplayBroadcastMessage;
        private GetMessageToBroadcast GetMessageToBroadcast;
        private ShowMessage ShowMessage;
        private DisplayVerse DisplayVerse;
 
        public ClientComms(DisplayBroadcastMessage DisplayBroadcastMessage, GetMessageToBroadcast GetMessageToBroadcast, ShowMessage ShowMessage, DisplayVerse DisplayVerse)
        {
            tcpClient = new TcpClient();
            messages = new ConcurrentQueue<List<string>>();
            commands = new BlockingCollection<char>();

            this.DisplayBroadcastMessage = DisplayBroadcastMessage;
            this.GetMessageToBroadcast = GetMessageToBroadcast;
            this.ShowMessage = ShowMessage;
            this.DisplayVerse = DisplayVerse;

        }

        public void Run()
        {
            clientRunning = true;

            if (Connect("localhost", 4444))
            {
                Task.Run(ReadFromServer);
                Task.Run(DisplayMessages);

                while (clientRunning)
                {
                    char userInput = commands.Take();

                    string msgToBroadcast = "";
                    if (userInput == 'B')
                    {
                        msgToBroadcast = GetMessageToBroadcast();
                    }
                    WriteToServer(userInput, msgToBroadcast);
                }
            }
            else
            {
                ShowMessage("ERROR: Connection to server not successful");
            }
            tcpClient.Close();
        }

        private bool Connect(string url, int portNumber)
        {
            try
            {
                tcpClient.Connect(url, portNumber);
                stream = tcpClient.GetStream();
                reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                writer = new StreamWriter(stream, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                ShowMessage("Exception: " + e.Message);
                return false;
            }
            return true;
        }

        private void SendCommandToServer(ServerCommandDTO serverCommand)
        {
            string svrCommand = JsonSerializer.Serialize(serverCommand);
            writer.WriteLine(svrCommand);
            writer.Flush();
            // string svrResponse = reader.ReadLine();
        }


        private void WriteToServer(char userChoice, string broadcastMessage)
        {
            if (userChoice == 'X')
            {
                clientRunning = false;
            }
            else if (userChoice == 'B' && broadcastMessage.Length > 0)
            {
                writer.WriteLine("" + userChoice);
                writer.WriteLine(broadcastMessage);
                writer.Flush();
            }
            else if (userChoice != 'B')
            {
                writer.WriteLine("" + userChoice);
                writer.Flush();
            }
        }

        private void ReadFromServer()
        {
            while (clientRunning)
            {
                string serverResponse = reader.ReadLine();
                char code = serverResponse.ToUpper()[0];
                List<string> msg = JsonSerializer.Deserialize<List<string>>(serverResponse.Substring(1));
                if (code == 'B')
                {
                    messages.Enqueue(msg);
                }
                else if (code == 'V')
                {
                    DisplayVerse(msg);
                }
            }
        }

        private void DisplayMessages()
        {
            while (clientRunning)
            {
                List<string> msgList = null;

                if (messages.TryDequeue(out msgList))
                {
                    DisplayBroadcastMessage(msgList);
                }
            }
        }

        public void AddCommand(char command)
        {
            commands.Add(command);
        }
    }
}