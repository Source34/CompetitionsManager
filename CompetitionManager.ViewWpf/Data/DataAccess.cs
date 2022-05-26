using System;
using System.IO;
using System.Windows;
using System.Net.Http;
using System.Text.Json;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.Data
{
    public static class DataAccess
    {
        private const string Path = "Data/apiconfig.json";
        public static CompetitionsStorage Storage { get; set; }

        public static void Init()
        {
            try
            {
                var config = JsonSerializer.Deserialize<ApiConfig>(File.ReadAllText(Path));

               if (config == null)
                   throw new Exception("Ошибка конфигурации!");

               Storage = new CompetitionsStorage(config.Host, new HttpClient());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
