namespace CognizantChallenge.Models
{
    public class TaskRecord
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long TaskId { get; set; }
        public string Description { get; set; }
        public string SourceCode { get; set; }
        public string Output { get; set; }
        public double? Memory { get; set; }
        public double? CpuTime { get; set; }
    }
}
