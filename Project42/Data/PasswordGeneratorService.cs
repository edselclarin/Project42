using Newtonsoft.Json;

namespace Project42.Data
{
    public class PasswordGeneratorService
    {
        public static async Task<string> Generate()
        {
            int numWords = 3;
            int wordLength = 5;
            string uri = $"https://random-word-api.vercel.app/api?words={numWords}&length={wordLength}&type=capitalized";

			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(uri))
			using (HttpContent content = response.Content)
			{
				string jsonPayload = await content.ReadAsStringAsync();

                string[]? words = JsonConvert.DeserializeObject<string[]>(jsonPayload);

                return BuildPassword(words);
            }
        }

        private static string BuildPassword(string[]? words)
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
                "-",
                (new Random()).Next(10000).ToString("D4")
            });
        }
    }
}
