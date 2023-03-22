using BusinessLogic.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class CredentialRequestDto
    {
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Character length between 4 and 20 for user name! ")]
        [Required]
        [CheckForWhiteSpaces]
        public string? UserName { get; set; }
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Character length between 8 and 20 for password! ")]
        [Required]
        [CheckForWhiteSpaces]
        public string? Password { get; set; }
    }
}
