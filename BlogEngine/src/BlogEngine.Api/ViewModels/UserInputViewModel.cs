using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Api.ViewModels
{
    public class UserInputViewModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}