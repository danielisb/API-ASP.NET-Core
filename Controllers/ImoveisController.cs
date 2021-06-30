using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using challenge_OLX.Data;
using challenge_OLX.Models;
using System.Linq;
using System;
using System.Net;
using static System.Net.WebRequestMethods;

namespace challenge_OLX.Controllers
{
    [ApiController]
    [Route("v1/imoveis")]
    public class ImoveisController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Imoveis>>> Get([FromServices] DataContext context, int pageNumber=1, int totalCount=10)
        {
            int pageSize = (int)System.Math.Ceiling(context.Imoveis.Count() / Convert.ToDecimal(totalCount));

            if (pageNumber < pageSize) Response.Headers.Add("X-Pages-NextPages", Url.Link("DefaultApi", new { pageNumber = pageNumber + 1, tamanhoPagina = totalCount }));
                Response.Headers.Add("X-Pages-TotalPages", pageSize.ToString());
                

            var imoveis = await context.Imoveis.OrderBy(i => i.id)
                                               .Skip(pageSize * (pageNumber - 1))
                                               .Take(pageSize)
                                               .ToListAsync();
            return imoveis;
        }
    }
}
