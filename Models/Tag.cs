namespace NymnetBlogApi.Models
{
    public class Tag : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public List<PostTags> PostTags { get; set; }
    }
}