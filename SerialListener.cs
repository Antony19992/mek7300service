using System;
using System.IO;
using System.IO.Ports;

namespace MEK7300service
{
    public class SerialListener : IDisposable
    {
        private readonly SerialPort _serialPort;
        private readonly object _lock = new object();

        public bool IsPortOpen => _serialPort.IsOpen;

        public SerialListener(string portName, int baudRate)
        {
            _serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
            {
                ReadTimeout = 1000,
                WriteTimeout = 1000,
                ReadBufferSize = 4096,
                WriteBufferSize = 2048
            };
            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        public void OpenPort()
        {
            lock (_lock)
            {
                if (!_serialPort.IsOpen)
                {
                    try
                    {
                        _serialPort.Open();
                        WriteLog("Porta serial aberta com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        WriteLog($"Erro ao abrir porta: {ex.Message}");
                    }
                }
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = string.Empty;

                while (_serialPort.BytesToRead > 0)
                {
                    data += _serialPort.ReadExisting();
                }

                WriteLog($"Dados recebidos: {data}");
                ProcessFile processFile = new ProcessFile();
                processFile.CreateInitializationFile(data);
            }
            catch (Exception ex)
            {
                WriteLog($"Erro ao processar dados recebidos: {ex.Message}");
            }
        }

        public void Dispose()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            _serialPort.Dispose();
        }

        public static void WriteLog(string message)
        {
            string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service.log");
            using (StreamWriter writer = new StreamWriter(logFile, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}