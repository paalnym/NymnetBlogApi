using Microsoft.EntityFrameworkCore;
using NymnetBlogApi.Models;

namespace NymnetBlogApi.Data
{
    public class PostsDataAccess
    {
        private readonly BlogContext _blogContext;

        public PostsDataAccess(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<Post> GetById(int id)
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

        public async Task<IList<Post>> GetAll()
        {
            var post = await _blogContext.Posts.AsNoTracking().ToListAsync();
            return post;
        }

        public async Task Insert(Post newPost)
        {
            // Entity state : Added
            await _blogContext.Posts.AddAsync(newPost);

            // This issues insert statement
            await _blogContext.SaveChangesAsync();
        }

        public async Task Update(int id, Post modifiedPost)
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
        }

        public async Task Delete(int postId)
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
        }
    }
}
