using System.ComponentModel.DataAnnotations;

namespace OneNight.Pages
{
    public class UsernameEnterModel
    {
        [Required]
        [StringLength(16, ErrorMessage = "Name is too long.")]
        public string username;
    }
}
