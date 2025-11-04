using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Clients.Events;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Domain.Orders;

namespace UzEx.Analytics.Domain.Clients
{
    public sealed class Client : Entity
    {
        public DateTime CreatedOnUtc { get; private set; }

        public ClientType Type { get; private set; }

        public ClientRegNumber RegNumber { get; private set; }

        public ClientName Name { get; private set; }

        public ClientCountry Country { get; private set; }

        public ClientRegion Region { get; private set; }

        public ClientDistrict District { get; private set; }

        public ClientAddress Address { get; private set; }
        
        public ClientNewSpotKey NewSpotKey { get; private set; }
        
        public ClientOldSpotKey OldSpotKey { get; private set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Deal>? SellerDeals { get; set; }
        
        public ICollection<Deal>? BuyerDeals { get; set; }

        private Client()
        {
        }

        public Client(
            Guid id,
            DateTime createdOnUtc,
            ClientType type,
            ClientRegNumber regNumber,
            ClientName name,
            ClientCountry country,
            ClientRegion region,
            ClientDistrict district,
            ClientAddress address
            ) : base(id)
        {
            CreatedOnUtc = createdOnUtc;
            Type = type;
            RegNumber = regNumber;
            Name = name;
            Country = country;
            Region = region;
            District = district;
            Address = address;
        }

        public static Client Create(
            Guid id,
            DateTime createdOnUtc,
            int type,
            string regNumber,
            string name,
            string country,
            string region,
            string district,
            string address)
        {
            var client = new Client(
                id,
                createdOnUtc,
                (ClientType)type,
                new ClientRegNumber(regNumber),
                new ClientName(name),
                new ClientCountry(country),
                new ClientRegion(region),
                new ClientDistrict(district),
                new ClientAddress(address));

            client.RaiseDomainEvent(new ClientCreatedDomainEvent(id));

            return client;
        }
        
        public Result SetNewSpotKey(string key)
        {
            NewSpotKey = new ClientNewSpotKey(key);

            return Result.Success();
        }
        
        public Result SetOldSpotKey(string key)
        {
            OldSpotKey = new ClientOldSpotKey(key);

            return Result.Success();
        }
    }
}
