using System.ServiceProcess;
using System.Threading;

namespace MEK7300service
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartListening();
        }

        protected override void OnStop()
        {
        }

        private void StartListening()
        {
            Thread listeningThread = new Thread(() =>
            {
                while (true)
                {
                    ProccessFile processFile = new ProccessFile();
                    processFile.CreateInitializationFile(); 

                    string dataReceived = SerialListener.listener(); 

                }
            })
            {
                IsBackground = true
            };

            listeningThread.Start();
        }
    }
}
