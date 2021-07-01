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
        public async Task<ActionResult<List<Imoveis>>> GetByPortal([FromServices] DataContext context, int pageNumber = 1, int totalCount = 10, string idPortal = "zap")
        {
            int pageSize = (int)System.Math.Ceiling(context.Imoveis.Count() / Convert.ToDecimal(totalCount));

            if (pageNumber < pageSize) Response.Headers.Add("X-Pages-NextPages", Url.Link("DefaultApi", new { pageNumber = pageNumber + 1, tamanhoPagina = totalCount }));
            Response.Headers.Add("X-Pages-TotalPages", pageSize.ToString());

            var imoveis = new List<Imoveis>();

            if(idPortal.ToLower() == "zap")
            {
                imoveis = await context.Imoveis.OrderBy(i => i.id)
                                               .Skip(pageSize * (pageNumber - 1))
                                               .Take(pageSize)
                                               .AsNoTracking()
                                               .Where(x => x.pricingInfos.rentalTotalPrice >= 3500 || x.pricingInfos.price >= 600000)
                                               .ToListAsync();
            }
            else if(idPortal.ToLower() == "viva")
            {
                imoveis = await context.Imoveis.OrderBy(i => i.id)
                                               .Skip(pageSize * (pageNumber - 1))
                                               .Take(pageSize)
                                               .AsNoTracking()
                                               .Where(x => x.pricingInfos.rentalTotalPrice <= 4000 || x.pricingInfos.price <= 700000)
                                               .ToListAsync();
            }
            return imoveis;
        } 
    }
}

