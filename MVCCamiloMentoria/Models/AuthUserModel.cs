using System.ComponentModel.DataAnnotations;

namespace MVCCamiloMentoria.Models
{
    public class AuthUserModel
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }


    }
}
