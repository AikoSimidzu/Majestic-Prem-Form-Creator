namespace MajesticPremForm
{
    using System.Collections.Generic;
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Services;
    using Google.Apis.Sheets.v4;
    using System.Windows.Forms;
    using MajesticPremForm.Properties;

    internal class GTabs
    {
        private string token { get; set; }
        private string AppName = "your app name";
        readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        readonly string SheetID = "sheet id";

        private string sheet = string.Empty;
        private SheetsService service;

        public GTabs(string Token, string Sheet)
        {
            token= Token;
            sheet= Sheet;
        }

        public List<string>? Load()
        {
            string path = @"path to your json file";
            if (!File.Exists(path))
            {
                File.WriteAllBytes(path, Resources.client_secret);
            }

            GoogleCredential credential;
            using (FileStream stream = new(path, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            service = new(new BaseClientService.Initializer() 
            {
                HttpClientInitializer= credential,
                ApplicationName= AppName,
            });

            var Req = service.Spreadsheets.Values.Get(SheetID, sheet);
            var Resp = Req.Execute();
            var Values = Resp.Values;

            List<string> dict = new List<string>();
            if (Values != null && Values.Count > 0)
            {
                foreach (var row in Values)
                {
                    if (row[7].ToString() == "Статик")
                    {
                        continue;
                    }
                    dict.Add($"{(string)row[7]};{(string)row[11]}");
                }
            }
            return dict;
        }
    }
}
