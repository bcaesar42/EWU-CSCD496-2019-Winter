using System;

namespace BlogEngine.Api.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public DateTime PostedOn { get; set; }
        public string Text { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int PostId { get; set; }
    }
}