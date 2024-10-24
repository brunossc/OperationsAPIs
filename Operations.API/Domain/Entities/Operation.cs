namespace Operations.API.Domain.Entities
{
    public class Operation
    {
        /// <summary>
        /// ULID = https://github.com/RobThree/NUlid
        /// </summary>
        public string Id { get; set; }
        public DateTime Day { get; set; }
        public int Type { get; set; }
        public decimal Value { get; set; }
    }
}
