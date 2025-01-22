using System;
using System.IO;

namespace MEK7300service
{
    public class ProccessFile
    {
        public void CreateInitializationFile()
        {
            string dataReceived = SerialListener.listener();

            string[] dataSplited = dataReceived.Split(';');

            var formatedFile = new
            {
                fileName = dataSplited[0],
                WBC = dataSplited[1],
                LY_Percent = dataSplited[2],
                MO_Percent = dataSplited[3],
                NE_Percent = dataSplited[4],
                EO_Percent = dataSplited[5],
                BA_Percent = dataSplited[6],
                LY = dataSplited[7],
                MO = dataSplited[8],
                NE = dataSplited[9],
                EO = dataSplited[10],
                BA = dataSplited[11],
                RBC = dataSplited[12],
                HGB = dataSplited[13],
                HCT = dataSplited[14],
                MCV = dataSplited[15],
                PDW = dataSplited[16],
                MCH = dataSplited[17],
                MCHC = dataSplited[18],
                RDW_CV = dataSplited[19],
                PLT = dataSplited[20],
                PCT = dataSplited[21],
                MPV = dataSplited[22]
            };

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, formatedFile.fileName + ".txt");

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var property in formatedFile.GetType().GetProperties())
                    {
                        writer.WriteLine($"{property.Name}: {property.GetValue(formatedFile)}");
                    }
                }

                Console.WriteLine($"Arquivo '{formatedFile.fileName}.txt' criado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar o arquivo: {ex.Message}");
            }
        }
    }
}
