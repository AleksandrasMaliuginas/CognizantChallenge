namespace CognizantChallenge.Models
{
    public class ExecutionResult
    {
        public string Output { get; set; }
        public int StatusCode { get; set; }
        public double Memory { get; set; }
        public double CpuTime { get; set; }
    }
}
