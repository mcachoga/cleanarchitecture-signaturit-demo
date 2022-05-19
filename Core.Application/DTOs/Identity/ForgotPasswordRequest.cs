using System.ComponentModel.DataAnnotations;

namespace Signaturit.Application.DTOs.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}