using System;
using System.IO;

namespace MEK7300service
{
    public class ProcessFile
    {
        public void CreateInitializationFile(string dataReceived)
        {
            try
            {
                string[] dataSplited = dataReceived.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Verifica se os dados têm o tamanho esperado
                if (dataSplited.Length < 23)
                {
                    WriteLog("Erro: Dados incompletos recebidos.");
                    return;
                }

                // Mapeia os dados recebidos para a classe
                var formatedFile = new FormatedFile
                {
                    FileName = dataSplited[0],
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

                // Salva os dados no arquivo
                SaveToFile(formatedFile);
            }
            catch (Exception ex)
            {
                WriteLog($"Erro ao processar os dados: {ex.Message}");
            }
        }

        private void SaveToFile(FormatedFile formatedFile)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{formatedFile.FileName}.txt");

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"FileName: {formatedFile.FileName}");
                    writer.WriteLine($"WBC: {formatedFile.WBC}");
                    writer.WriteLine($"LY_Percent: {formatedFile.LY_Percent}");
                    writer.WriteLine($"MO_Percent: {formatedFile.MO_Percent}");
                    writer.WriteLine($"NE_Percent: {formatedFile.NE_Percent}");
                    writer.WriteLine($"EO_Percent: {formatedFile.EO_Percent}");
                    writer.WriteLine($"BA_Percent: {formatedFile.BA_Percent}");
                    writer.WriteLine($"LY: {formatedFile.LY}");
                    writer.WriteLine($"MO: {formatedFile.MO}");
                    writer.WriteLine($"NE: {formatedFile.NE}");
                    writer.WriteLine($"EO: {formatedFile.EO}");
                    writer.WriteLine($"BA: {formatedFile.BA}");
                    writer.WriteLine($"RBC: {formatedFile.RBC}");
                    writer.WriteLine($"HGB: {formatedFile.HGB}");
                    writer.WriteLine($"HCT: {formatedFile.HCT}");
                    writer.WriteLine($"MCV: {formatedFile.MCV}");
                    writer.WriteLine($"PDW: {formatedFile.PDW}");
                    writer.WriteLine($"MCH: {formatedFile.MCH}");
                    writer.WriteLine($"MCHC: {formatedFile.MCHC}");
                    writer.WriteLine($"RDW_CV: {formatedFile.RDW_CV}");
                    writer.WriteLine($"PLT: {formatedFile.PLT}");
                    writer.WriteLine($"PCT: {formatedFile.PCT}");
                    writer.WriteLine($"MPV: {formatedFile.MPV}");
                }
                WriteLog($"Arquivo '{formatedFile.FileName}.txt' criado com sucesso.");
            }
            catch (Exception ex)
            {
                WriteLog($"Erro ao criar o arquivo: {ex.Message}");
            }
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

    public class FormatedFile
    {
        public string FileName { get; set; }
        public string WBC { get; set; }
        public string LY_Percent { get; set; }
        public string MO_Percent { get; set; }
        public string NE_Percent { get; set; }
        public string EO_Percent { get; set; }
        public string BA_Percent { get; set; }
        public string LY { get; set; }
        public string MO { get; set; }
        public string NE { get; set; }
        public string EO { get; set; }
        public string BA { get; set; }
        public string RBC { get; set; }
        public string HGB { get; set; }
        public string HCT { get; set; }
        public string MCV { get; set; }
        public string PDW { get; set; }
        public string MCH { get; set; }
        public string MCHC { get; set; }
        public string RDW_CV { get; set; }
        public string PLT { get; set; }
        public string PCT { get; set; }
        public string MPV { get; set; }
    }
}