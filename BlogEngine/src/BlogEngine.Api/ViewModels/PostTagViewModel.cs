namespace BlogEngine.Api.ViewModels
{
    public class PostTagViewModel
    {
        public int PostId { get; set; }
        public PostViewModel Post { get; set; }
        public int TagId { get; set; }
        public TagViewModel Tag { get; set; }
    }
}