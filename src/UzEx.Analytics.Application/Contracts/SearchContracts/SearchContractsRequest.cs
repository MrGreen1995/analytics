using System.ComponentModel.DataAnnotations;
using UzEx.Analytics.Application.Models.Shared;

namespace UzEx.Analytics.Application.Contracts.SearchContracts
{
    public sealed record SearchContractsRequest : PaginationAndSorting
    {
        // Filter Parameters
        public string? ContractNumber { get; init; }

        public string? ContractName { get; init; }

        [MaxLength(4)]
        public List<int> ContractType { get; init; } = new List<int>();

        [MaxLength(4)]
        public List<int> ContractForm { get; init; } = new List<int>();

        public string? ContractUnit { get; init; }

        public string? ContractCurrency { get; init; }

        public string? ContractDeliveryBase { get; init; }

        [MaxLength(5)]
        public List<int> ContractTradeType { get; init; } = new List<int>();
    }
}
