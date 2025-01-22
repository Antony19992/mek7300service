using System;
using System.IO.Ports;

namespace MEK7300service
{
    public class SerialListener
    {
        static SerialPort serialPort;
        static string dataReceived = "";  // Variável para armazenar os dados recebidos

        public static string listener()
        {
            // Inicializa a porta serial
            serialPort = new SerialPort
            {
                PortName = "COM4",
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadBufferSize = 4096,
                WriteBufferSize = 2048,
                ReadTimeout = 1000,
                WriteTimeout = 1000
            };

            try
            {
                // Associa o evento DataReceived
                serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

                // Abre a porta serial
                serialPort.Open();
                Console.WriteLine("Porta serial aberta. Aguardando dados...");

                Console.WriteLine("Pressione 'q' para sair.");
                while (Console.ReadKey(true).Key != ConsoleKey.Q) { }

                // Retorna os dados recebidos ao final do listener
                return dataReceived;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return string.Empty; // Caso haja erro, retorna uma string vazia
            }
            finally
            {
                // Fecha a porta serial quando terminar
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    Console.WriteLine("Porta serial fechada.");
                }
            }
        }

        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = "";

                // Lê os dados disponíveis na porta serial
                while (serialPort.BytesToRead > 0)
                {
                    data += serialPort.ReadExisting(); // Lê os dados recebidos
                }

                Console.WriteLine($"Dados recebidos:\n{data}");

                // Processa os dados e armazena na variável compartilhada
                dataReceived = ProcessData(data);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Timeout ao ler os dados.");
            }
        }

        private static string ProcessData(string data)
        {
            string dataExam = "";
            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                dataExam += line.Trim() + ";"; // Adiciona a linha processada
                Console.WriteLine($"Linha processada: {line}");
            }

            return dataExam;
        }
    }
}
