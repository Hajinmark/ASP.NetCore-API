using System.ComponentModel.DataAnnotations;

namespace WebAPIMastery.Models.Domain
{
    public class Register
    {
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Roles { get; set; } 

    }
}
