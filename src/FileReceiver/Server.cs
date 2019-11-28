using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FileReceiver
{
    public class Server
    {
        private HttpListener listener;
        private CancellationTokenSource cancellationTokenSource;

        public Server()
        {
            cancellationTokenSource = new CancellationTokenSource();
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8000/");
        }

        public bool IsStopped { get => !listener.IsListening; }

        public void Start()
        {
            if (!listener.IsListening)
            {
                listener.Start();
            }
        }

        public void Stop()
        {
            if (!listener.IsListening)
            {
                cancellationTokenSource.Cancel();
                listener.Stop();
            }
        }

        public Task<HttpListenerContext> Listening()
        {
            return Task.Run(() =>
            {
                var taskContext = listener.GetContextAsync();

                while (!taskContext.IsCompleted && !taskContext.IsFaulted && !taskContext.IsCanceled)
                {
                    if (cancellationTokenSource.IsCancellationRequested)
                        cancellationTokenSource.Cancel();
                }

                return taskContext;

            }, cancellationTokenSource.Token);
        }
    }
}
