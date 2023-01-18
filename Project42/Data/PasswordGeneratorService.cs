using Newtonsoft.Json;
using System.Net;

namespace Project42.Data
{
    public class PasswordGeneratorService
    {
        public static string Generate()
        {
            int numWords = 3;
            int wordLength = 5;
            string uri = $"https://random-word-api.vercel.app/api?words={numWords}&length={wordLength}&type=capitalized";

            var req = WebRequest.Create(uri);
            req.ContentType = "application/json";
            req.Method = "GET";

            var rsp = req.GetResponse();

            using (var sr = new StreamReader(rsp.GetResponseStream()))
            {
                string jsonPayload = sr.ReadToEnd();

                string[] words = JsonConvert.DeserializeObject<string[]>(jsonPayload);

                return BuildPassword(words);
            }
        }

        private static string BuildPassword(string[] words)
        {
            string pwd = String.Empty;

            foreach (string word in words)
            {
                if (word.Length < 2)
                {
                    throw new Exception("Word is too short.");
                }

                pwd += word.Substring(0, 1).ToUpper() + word.Substring(1, word.Length - 1);
            }

            return String.Join("", new string[]
            {
                pwd,
                "#",
                DateTime.Now.ToString("HHmm")
            });
        }
    }
}
