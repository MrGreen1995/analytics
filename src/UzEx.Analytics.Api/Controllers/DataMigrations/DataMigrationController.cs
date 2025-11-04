using MediatR;
using Microsoft.AspNetCore.Mvc;
using UzEx.Analytics.Application.DataMigrations;
using UzEx.Analytics.Application.DataMigrations.CopyDataMigrations;
using UzEx.Analytics.Domain.DataMigrations;

namespace UzEx.Analytics.Api.Controllers.DataMigrations;

[ApiController]
[Route("api/[controller]")]
public class DataMigrationController : ControllerBase
{
    private readonly ISender _sender;

    public DataMigrationController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost("migrate")]
    public async Task<IActionResult> Sync([FromBody] DataMigrationRequest request, CancellationToken cancellationToken)
    {
        var query = new DataMigrationQuery(
            request.Date, 
            (DataMigrationPlatformType)request.Platform, 
            (DataMigrationDataType)request.DataType);
        
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
}