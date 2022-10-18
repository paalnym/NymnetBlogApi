namespace NymnetBlogApi.Models
{
    public class BlogPost
    {
        public int Id {get; set;}
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public Author Author { get; set; }
        public string[] Tags { get; set; }
    }
}