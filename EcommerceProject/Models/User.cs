namespace EcommerceProject.Models
{
    public class User:BaseModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class UserRegistrationModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public long RoleId { get; set; }
        public List<UserTypes> userTypes { get; set; }
    }
    public class UserTypes
    {
        public long RoleId { get; set; }
        public string RoleType { get; set; }
    }
    public class UserLogInModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UserPasswordModel
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}
