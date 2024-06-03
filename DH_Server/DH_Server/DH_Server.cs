using DH_Server.Commands;
using DH_Server.Presenters;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DH_Server
{
    delegate void RemoveClient(DH_Client_Service c);
    delegate void BroadcastMessageToAllClients(string message);
    delegate void BroadcastCommandToAllClients(string command);

    class DH_Server_App
    {
        private TcpListener tcpListener;
        private ConcurrentDictionary<DH_Client_Service, bool> clientServices;
        private bool DBInitialised;

        public DH_Server_App()
        {
            IPAddress ipAddress = IPAddress.Loopback;
            this.tcpListener = new TcpListener(ipAddress, 4444);
            this.clientServices = new ConcurrentDictionary<DH_Client_Service, bool>();
            this.DBInitialised = false;
        }

        // START THE SERVER
        public async Task StartAsync()
        {
            if (!InitialiseDatabase())
            {
                Console.WriteLine("Database initialisation FAILED!");
            }
            else
            {
                this.DBInitialised = true;
                Console.WriteLine("Database initialised!");                
            }

            tcpListener.Start();
            Console.WriteLine("Listening....");            

            while (true)
            {
                Socket socket = await tcpListener.AcceptSocketAsync();
                DH_Client_Service clientService = new DH_Client_Service(socket, RemoveClient, BroadcastMessageToAllClientsVoid, BroadcastCommandToAllClientsVoid);
                clientServices.TryAdd(clientService, true);
                _ = clientService.RunAsync(); // don't need to wait for the task to complete, ignore the returned task with a discard (_):
            }
        }

        // Used to avoid the warning about not awaiting the task
        private void BroadcastMessageToAllClientsVoid(string msg)
        {
            // ignore the returned task with a discard (_):
            _ = BroadcastMessageToAllClientsAsync(msg);
        }

        private void BroadcastCommandToAllClientsVoid(string msg)
        {
            _ = BroadcastCommandToAllClientsAsync(msg);
        }

        // STOP THE SERVER
        public void Stop()
        {
            tcpListener.Stop();
        }

        // INITIALISE THE DATABASE
        public bool InitialiseDatabase()
        {
            CommandFactory factory = new CommandFactory();

            CommandLineViewData initialise_database_status = factory
            .CreateCommand(RequestUseCase.INITIALISE_DATABASE)
                .Execute_InitialiseDatabase();

            return initialise_database_status.Status;
        }


        // REMOVE A CLIENT
        private void RemoveClient(DH_Client_Service client)
        {
            Console.WriteLine("REMOVING CLIENT");
            bool ignored;
            clientServices.TryRemove(client, out ignored);
        }

        // BROADCAST A MESSAGE
        private async Task BroadcastMessageToAllClientsAsync(string msg)
        {
            if (clientServices.Count > 0)
            {
                Console.WriteLine($"BROADCAST MESSAGE:\n{msg}\n");
                var tasks = clientServices.Keys
                    .Where(clientService => clientService.AcceptsMessagesFlag)
                    .Select(clientService =>
                    {
                        Console.WriteLine($"SENDING MESSAGE -> {clientService.ClientType}@{clientService.ClientID}");
                        return Task.Run(() => clientService.SendBroadcastMessageToClient(msg));
                    });
                await Task.WhenAll(tasks);
            }
            else
            {
                Console.WriteLine("NOT BROADCASTING (NO CLIENTS)");
            }
        }

        // BROADCAST A COMMAND
        private async Task BroadcastCommandToAllClientsAsync(string command)
        {
            if (clientServices.Count > 0)
            {
                Console.WriteLine("BROADCAST COMMAND: " + command);
                var tasks = clientServices.Keys
                    .Where(clientService => clientService.AcceptsCommandsFlag)
                    .Select(clientService =>
                    {
                        Console.WriteLine($"SENDING COMMAND -> {clientService.ClientType}@{clientService.ClientID}");
                        return Task.Run(() => clientService.AddResponse(command));
                    });
                await Task.WhenAll(tasks);
            }
            else
            {
                Console.WriteLine("NOT BROADCASTING (NO CLIENTS)");
            }
        }
    }
}
