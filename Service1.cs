using System;
using System.ServiceProcess;
using System.Threading;

namespace MEK7300service
{
    public partial class Service1 : ServiceBase
    {
        private Timer _timer;
        private SerialListener _serialListener;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _serialListener = new SerialListener("COM4", 9600);
            _timer = new Timer(CheckPortStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        protected override void OnStop()
        {
            _serialListener.Dispose();
            _timer.Dispose();
        }

        private void CheckPortStatus(object state)
        {
            if (!_serialListener.IsPortOpen)
            {
                _serialListener.OpenPort();
            }
        }
    }
}