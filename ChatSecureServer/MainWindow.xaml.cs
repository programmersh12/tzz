using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatServerWPF
{
    public partial class MainWindow : Window
    {
        private TcpListener server;
        private List<ClientHandler> clients = new List<ClientHandler>();

        public MainWindow()
        {
            InitializeComponent();
            StartServer();
        }

        private async void StartServer()
        {
            try
            {
                server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
                server.Start();
                AppendLog("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = await server.AcceptTcpClientAsync();
                    AppendLog("Клиент подключился.");

                    var handler = new ClientHandler(tcpClient, BroadcastMessage, AppendLog);
                    clients.Add(handler);

                    _ = handler.ProcessAsync();
                }
            }
            catch (Exception ex)
            {
                AppendLog($"Ошибка сервера: {ex.Message}");
            }
        }

        private void BroadcastMessage(string message)
        {
            foreach (var client in clients)
            {
                client.SendAsync(message);
            }

            Dispatcher.Invoke(() =>
            {
                var tb = new TextBlock
                {
                    Text = $"[Broadcast]: {message}",
                    Foreground = System.Windows.Media.Brushes.LightGreen,
                    Margin = new Thickness(0, 5, 0, 0)
                };
                LogPanel.Children.Add(tb);
            });
        }

        private void AppendLog(string text)
        {
            Dispatcher.Invoke(() =>
            {
                LogPanel.Children.Add(new TextBlock
                {
                    Text = text,
                    Foreground = System.Windows.Media.Brushes.LightGray,
                    Margin = new Thickness(0, 3, 0, 0)
                });
            });
        }
    }

    public class ClientHandler
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private Action<string> broadcast;
        private Action<string> log;

        public ClientHandler(TcpClient client, Action<string> broadcast, Action<string> log)
        {
            this.client = client;
            this.broadcast = broadcast;
            this.log = log;

            var stream = client.GetStream();
            reader = new StreamReader(stream, Encoding.UTF8);
            writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
        }

        public async Task ProcessAsync()
        {
            try
            {
                while (true)
                {
                    string message = await reader.ReadLineAsync();
                    if (message != null)
                    {
                        log("Сообщение от клиента: " + message);
                        broadcast(message);
                    }
                }
            }
            catch (Exception ex)
            {
                log("Ошибка клиента: " + ex.Message);
            }
        }

        public async void SendAsync(string message)
        {
            try
            {
                await writer.WriteLineAsync(message);
            }
            catch (Exception ex)
            {
                log("Ошибка отправки клиенту: " + ex.Message);
            }
        }
    }
}
