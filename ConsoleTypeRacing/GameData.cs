using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ConsoleTypeRacing
{
    internal class GameData
    {
        // This class will be responsible for fetching game data such as words, phrases, sentences, or paragraphs for typing practice.
        // It should handle the logic that goes and gathers example texts from locations based on difficulty selected by user
        // this will handle a json response, parse it, and send on to let player play game
        HttpClient _HTTPClient = new HttpClient();
        private int _randNum;
        public string _username = "";
        public string _password = "";
        

        private int RandNum {get => _randNum; }
        // return json object of text found at random location between 1-700 (this is worth investigating better ways of working with shitty apis)
        public GameData()
        {
            Random _rand = new Random();
            _randNum = _rand.Next(1, 701);
        }

        public string GenerateGameData()
        {
            GetUsernameAndPassword();
            return GetHTTPResponse();
            // ParseJSONforText();
            // ReturnGamePrompt();
            // throw new NotImplementedException;
        }

        private string GetHTTPResponse()
        {
            var byteArray = Encoding.ASCII.GetBytes($"{_username}:{_password}");
            _HTTPClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response = _HTTPClient.GetAsync($"https://data.typeracer.com/api/v1/texts/{RandNum}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;

            string text = root.GetProperty("data").GetProperty("text").GetString();

            return text;
        }

        private void ParseJSONforText()
        {
            throw new NotImplementedException();
        }

        private void ReturnGamePrompt()
        {
            throw new NotImplementedException();
        }
        private void GetUsernameAndPassword()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\secrets.txt");
            string[] lines = File.ReadAllLines(path);
            var credentials = lines
                .Select(line => line.Split('='))
                .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());
            _username = credentials["username"];
            _password = credentials["password"];

        }
    }
}
