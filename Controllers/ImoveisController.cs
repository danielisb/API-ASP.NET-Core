using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using challenge_OLX.Data;
using challenge_OLX.Models;

namespace challenge_OLX.Controllers
{
    [ApiController]
    [Route("v1/imoveis")]
    public class ImoveisController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Imoveis>>> Get([FromServices] DataContext context)
        {
            var imoveis = await context.Imoveis.ToListAsync();
            return imoveis;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Imoveis>> Post(
            [FromServices] DataContext context,
            [FromBody]Imoveis model)
        {
            if (ModelState.IsValid)
            {
                context.Imoveis.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
