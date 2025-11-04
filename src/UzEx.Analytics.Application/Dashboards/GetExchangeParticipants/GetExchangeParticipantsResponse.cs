namespace UzEx.Analytics.Application.Dashboards.GetExchangeParticipants;

public class GetExchangeParticipantsResponse
{
    public required string Trade { get; init; }

    public decimal Participants { get; init; }

    public required string ParticipantsUnit{ get; init; }
}