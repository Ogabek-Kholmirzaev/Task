using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Taskk.Data;
using Taskk.Dtos.AppUser;
using Taskk.Dtos.Product;
using Taskk.Dtos.ProductAudit;
using Taskk.Entities;
using Taskk.Statics;

namespace Taskk.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class FilterController : ControllerBase
{
    private readonly AppDbContext appDbContext;

    public FilterController(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsAudit([FromQuery] Filter filter)
    {
        var productAudits =
            this.appDbContext.ProductAudits.Any() ? await this.appDbContext.ProductAudits.ToListAsync() : new List<ProductAudit>() ;

        var result = new List<ProductAuditDto>();

        filter.StartDate ??= productAudits.Min(productAudit => productAudit.ChangedDate);
        filter.EndDate ??= productAudits.Max(productAudit => productAudit.ChangedDate);

        var difference = ((TimeSpan)(filter.StartDate - filter.EndDate)).TotalSeconds;
        if (difference > 0)
            return BadRequest("StartDate cannot be larger than EndDate");

        foreach (var productAudit in productAudits)
        {
            var duration1 = ((TimeSpan)(productAudit.ChangedDate - filter.StartDate)).TotalSeconds;
            var duration2 = ((TimeSpan)(productAudit.ChangedDate - filter.EndDate)).TotalSeconds;

            if (duration1 >= 0 && duration2 <= 0)
            {
                var productAuditDto = new ProductAuditDto()
                {
                    Id = productAudit.Id,
                    AppUserDto = productAudit.User!.Adapt<AppUserDto>(),
                    ProductDto = productAudit.Product!.Adapt<ProductDto>(),
                    Name = productAudit.Name,
                    UserId = productAudit.UserId,
                    ProductId = productAudit.ProductId,
                    ChangedDate = productAudit.ChangedDate
                };

                result.Add(productAuditDto);
            }
        }

        var resultJson = JsonConvert.SerializeObject(result);
        
        return Ok(resultJson);
    }
}