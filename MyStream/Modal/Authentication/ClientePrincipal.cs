using System.ComponentModel.DataAnnotations;

namespace MyStream.Modal.Authentication
{
    public class ClientePrincipal
    {
        public string UserId { get; set; }
        public string IdentityProvider { get; set; }
        public string UserDetails { get; set; }
        public string[] UserRoles { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        public bool Blocked { get; set; }
    }
}