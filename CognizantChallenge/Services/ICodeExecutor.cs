using CognizantChallenge.Models;
using System.Threading.Tasks;

namespace CognizantChallenge.Services
{
    public interface ICodeExecutor
    {
        Task<ExecutionResult> Execute(string sourceCode);
    }
}