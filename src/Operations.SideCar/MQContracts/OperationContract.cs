using Operations.SideCar.Enum;

namespace Operations.SideCar.MQContracts
{
    public class OperationContract
    {
        public string Id { get; set; } // ULID
        public DateTime Day { get; set; }
        public OperationType Type { get; set; }
        public decimal Value { get; set; }
    }
}
