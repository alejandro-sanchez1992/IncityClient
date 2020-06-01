using System.ComponentModel.DataAnnotations;

namespace IncityClient.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
