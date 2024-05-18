namespace Ewamall.WebAPI.DTOs
{
    public class Authentication
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public RoleDTO Role { get; set; }
    }
    public class RoleDTO
    {
        public string RoleName { get; set; }
    }
}
