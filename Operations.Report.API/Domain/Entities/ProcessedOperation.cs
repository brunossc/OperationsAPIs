using Operations.SideCar.Enum;

namespace Operations.Report.API.Domain.Entities
{
    public class ProcessedOperation
    {
        public string Id { get; set; }
        public DateTime Day { get; set; }
        public OperationType Type { get; set; }
        public decimal Value { get; set; }
    }
}
