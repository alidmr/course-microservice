using System.ComponentModel.DataAnnotations;

namespace FreeCourse.IdentityServer.Dtos
{
    public class SignUpDto
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string UserName { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string City { get; set; }
    }
}