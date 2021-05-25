using CognizantChallenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace CognizantChallenge.Services
{
    public class CodeExecutor
    {
        private class ExecRequest
        {
            public string clientId = "fa2eaa085e2c62c87c0e4270a15219d8";
            public string clientSecret = "25150b587c12126ffa30418f7211b3c1faa8e8796a3420e073d7b33fad7ffb82";
            public string script = "using System; class Program { static void Main(string[] args) { Console.WriteLine(\"Hello world!\"); } }";
            public string stdin = "";
            public string language = "csharp";
            public string versionIndex = "1";
        }

        public async Task<ExecutionResult> Execute(string sourceCode)
        {
            var result = await RunExternaly(sourceCode);

            return JsonConvert.DeserializeObject<ExecutionResult>(result);
        }

        private async Task<string> RunExternaly(string sourceCode)
        {
            var requestContent = new ExecRequest();
            requestContent.script = sourceCode;
            var serialized1 = JsonConvert.SerializeObject(requestContent);


            requestContent.script = sourceCode.Replace("\\", "\\\\");
            var serialized2 = JsonConvert.SerializeObject(requestContent);
            var content2 = new StringContent(serialized2, System.Text.Encoding.UTF8, "application/json");

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.jdoodle.com/v1/execute")
            {
                Content = new StringContent(serialized2, System.Text.Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);

            // var obj = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
