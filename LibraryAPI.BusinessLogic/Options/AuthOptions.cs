using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.BusinessLogic.Options
{
    public class AuthOptions
    {
        [Required] public string Secret { get; set; }
        [Required] public int JwtTokenExpiresMinutes { get; set; }
    }
}
