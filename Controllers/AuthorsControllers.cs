using apiVS.DTOs;
using AutoMapper;
using System.Linq;
using apiVS.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace apiVS.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsControllers : ControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;

        public AuthorsControllers(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> Get()
        {
            var authors =  await context.Authors.ToListAsync();
            return mapper.Map<List<AuthorDTO>>(authors);
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<List<AuthorDTO>>> GetByName([FromRoute] string name)
        {
            var author = await context.Authors.Where(x => x.Name.Contains(name)).ToListAsync();
            if (author == null)
            {
                return NotFound();
            } 
            return mapper.Map<List<AuthorDTO>>(author);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDTO>> GetById([FromRoute] int id)
        {
            var author = await context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null)
            {
                return BadRequest("El Autor no existe");
            }
            return mapper.Map<AuthorDTO>(author);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AuthorCreation authorCreation)
        {
            var exist = await context.Authors.AnyAsync(x => x.Name == authorCreation.Name);
            if (exist)
            {
                return BadRequest("Ya existe el Autor");
            }
            var author = mapper.Map<Authors>(authorCreation);
            context.Add(author);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Post([FromRoute] int id, [FromBody] Authors author)
        {
            var valid = await context.Authors.AnyAsync(x => x.Id == id);
            if (!valid)
            {
                return BadRequest("El Id del autor no existe");
            }
            context.Update(author);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete([FromRoute] int id)
        {
            var author = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null)
            {
                return BadRequest("El Id del autor no existe");
            }
            context.Remove(author);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
