using System.ComponentModel.DataAnnotations;

namespace OneNight.Pages
{
    public class GameCodeEnterModel
    {
        [Required]
        [StringLength(maximumLength: 4, MinimumLength = 4, ErrorMessage = "Game code must 4 characters long.")]
        public string gameCode;
    }
}
