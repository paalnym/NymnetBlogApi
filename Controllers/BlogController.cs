using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NymnetBlogApi.Models;
using NymnetBlogApi.Data;
using Microsoft.EntityFrameworkCore;

namespace NymnetBlogApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]    
    public class BlogController : ControllerBase
    {

        private readonly BlogContext _blogContext;

        public BlogController(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IList<Post>>> GetAll()
        {
            var post = await _blogContext.Posts.AsNoTracking().ToListAsync();
            return post;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetById(int id)
        {
            // Tracking not required
            var post = await _blogContext.Posts.Where(x => x.Id == id)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
            if (post == null)
            {
                throw new Exception($"Post with ID:{id} Not Found");
            }

            return post;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Post>> Post([FromBody] Post newPost)
        {
            // Entity state : Added
            await _blogContext.Posts.AddAsync(newPost);

            // This issues insert statement
            await _blogContext.SaveChangesAsync();
            return newPost;
        }

        // PATCH api/values/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<Post>> Patch(int id, [FromBody] Post modifiedPost)
        {
            var post = await _blogContext.Posts.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new Exception($"Post with ID:{id} Not Found");
            }

            post.Title = modifiedPost.Title;
            post.Summary = modifiedPost.Summary;
            post.PostContents = modifiedPost.PostContents;
            post.Comments = modifiedPost.Comments;
            // Set other properties 

            // Entity state : Modified
            _blogContext.Posts.Update(post);

            // This issues insert statement
            await _blogContext.SaveChangesAsync();
            return modifiedPost;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete(int postId)
        {
            var post = await _blogContext.Posts.Where(x => x.Id == postId).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new Exception($"Post with ID:{postId} Not Found");
            }

            // Entity state : Deleted
            _blogContext.Posts.Remove(post);

            // This issues insert statement
            await _blogContext.SaveChangesAsync();
            return post;
        }
    }
}