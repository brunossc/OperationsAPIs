using Operations.SideCar.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
