using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sol_Demo.Entity;
using Sol_Demo.Infrastructure;
using Sol_Demo.Utility;

namespace Sol_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SalesController : ControllerBase
    {
        private readonly AdventureWorks2012Context context;

        public SalesController(AdventureWorks2012Context context)
        {
            this.context = context;
        }

        [HttpGet("get-sales-order")]
        public async Task<IActionResult> GetSalesOrderData([FromQuery] PageQueryParamter pageQuery)
        {
            var owners = await PageList<SalesOrderDetail>.ToPagedListAsync(context.SalesOrderDetails, pageQuery.PageNumber, pageQuery.PageSize);

            return base.Ok(await Task.FromResult(owners.ToList()));
        }
    }
}