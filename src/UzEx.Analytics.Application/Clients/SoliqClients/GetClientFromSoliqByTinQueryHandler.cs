using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Abstractions.Soliq;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Clients.Errors;

namespace UzEx.Analytics.Application.Clients.SoliqClients;

public class GetClientFromSoliqByTinQueryHandler: IQueryHandler<GetClientFromSoliqByTinQuery, GetClientFromSoliqByTinResponse>
{
    private readonly ISoliqService _soliq;

    public GetClientFromSoliqByTinQueryHandler(ISoliqService soliq)
    {
        _soliq = soliq;
    }

    public async Task<Result<GetClientFromSoliqByTinResponse>> Handle(GetClientFromSoliqByTinQuery request, CancellationToken cancellationToken)
    {
        var clientFromSoliq = await _soliq.GetUserAsync(request.Tin, cancellationToken);
        
        if (clientFromSoliq is null)
        {
            return Result.Failure<GetClientFromSoliqByTinResponse>(ClientErrors.NotFound);
        }
        
        var response = new GetClientFromSoliqByTinResponse
        {
            Name = clientFromSoliq.Name,
            Tin = clientFromSoliq.Tin,
            Account = clientFromSoliq.Account,
            Address = clientFromSoliq.Address,
            Mfo = clientFromSoliq.Mfo,
            Ns10Code = clientFromSoliq.Ns10Code,
            Ns11Code = clientFromSoliq.Ns11Code,
            Oked = clientFromSoliq.Oked,
            ShortName = clientFromSoliq.ShortName,
            StatusCode = clientFromSoliq.StatusCode,
            StatusName = clientFromSoliq.StatusName,
            Director = clientFromSoliq.Director,
            DirectorTin = clientFromSoliq.DirectorTin
        };
        
        return response;
    }
}