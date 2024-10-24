namespace Operations.Report.API.Domain.Entities
{
    public class OperationDay
    {
        public string Id { get; set; }
        public DateTime Day { get; set; }
        public decimal Total { get; set; }
    }
}
