using MediatR;
using Microsoft.AspNetCore.Mvc;
using UzEx.Analytics.Application.Clients.SoliqClients;

namespace UzEx.Analytics.Api.Controllers.Clients;

[Route("api/[controller]")]
[ApiController]
public class SoliqController : ControllerBase
{
    private readonly ISender _sender;

    public SoliqController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetClientFromSoliqByTin([FromQuery] string tin, CancellationToken cancellationToken)
    {
        var query = new GetClientFromSoliqByTinQuery(tin);
        
        var result = await _sender.Send(query, cancellationToken);
        
        return result.IsSuccess ? Ok(result) : NotFound();
    }
}