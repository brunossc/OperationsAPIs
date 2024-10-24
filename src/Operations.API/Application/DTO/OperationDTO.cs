using Operations.SideCar.Enum;

namespace Operations.API.Application.DTO
{
    public record OperationDTO(OperationType Type, decimal Value);
}
