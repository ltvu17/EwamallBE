namespace Ewamall.WebAPI.DTOs
{
    public class Authentication
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public RoleDTO Role { get; set; }
        public UserDTO User { get; set; }
    }
    public class RoleDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
