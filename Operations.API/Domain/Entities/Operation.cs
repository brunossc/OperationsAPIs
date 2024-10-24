using System.ComponentModel.DataAnnotations;

namespace Operations.API.Domain.Entities
{
    public class Operation
    {
        /// <summary>
        /// ULID = https://github.com/RobThree/NUlid
        /// </summary>
        public string Id { get; set; }
        public DateTime Day { get; set; }
        [Required]
        public required int Type { get; set; }
        [Required]
        public required decimal Value { get; set; }
    }
}
