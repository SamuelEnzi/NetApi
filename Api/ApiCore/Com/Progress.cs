using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Com
{
    public static class Progress
    {
        public delegate void OnConnectionChangedEventHandler(HttpClient httpClient, Request request);

        public static event OnConnectionChangedEventHandler? OnConnectionOpen;
        public static event OnConnectionChangedEventHandler? OnConnectionClosed;

        public static List<HttpClient> Connections = new List<HttpClient>();

        public static void Register(HttpClient client, Request sender)
        {
            Connections.Add(client);
            OnConnectionOpen?.Invoke(client, sender);
        }

        public static void Unregiser(HttpClient client, Request sender)
        {
            Connections.Remove(client);
            OnConnectionClosed?.Invoke(client, sender);
        }
    }
}
