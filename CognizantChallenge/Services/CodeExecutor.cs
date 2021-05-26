using CognizantChallenge.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CognizantChallenge.Services
{
    public class CodeExecutor : ICodeExecutor
    {
        public CodeExecutor(string connectionString)
        {
            var args = connectionString.Replace(" ", "").Split(";");
            if (args.Length == 2)
            {
                _clientId = args[0];
                _clientSecret = args[1];
            }
        }

        private string _clientId = "";
        private string _clientSecret = "";

        private class ExecRequest
        {
            public string clientId = "";
            public string clientSecret = "";
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
            requestContent.clientId = _clientId;
            requestContent.clientSecret = _clientSecret;
            requestContent.script = sourceCode.Replace("\\", "\\\\");

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.jdoodle.com/v1/execute")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(requestContent),
                    Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
