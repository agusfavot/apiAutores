using apiVS.DTOs;
using apiVS.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiVS.Controllers
{
    [ApiController]
    [Route("api/books/{bookId:int}/comments")]
    public class CommentsControllers : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CommentsControllers(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<CommentDTO>>> Get([FromRoute] int bookId)
        {
            var comments = await context.Comments.Where(x => x.BookId == bookId).ToListAsync();
            return mapper.Map<List<CommentDTO>>(comments);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromRoute] int bookId, [FromBody] CommentCreation commentCreation )
        {
            var exist = await context.Books.AnyAsync(x => x.Id == bookId);
            if (!exist)
            {
                return NotFound();
            }
            var comment = mapper.Map<Comments>(commentCreation);
            comment.BookId = bookId;
            context.Add(comment);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
