using EcommerceProject.Models;

namespace EcommerceProject.Repository.Interface
{
    public interface IUserAsyncRepository
    {
        Task<long> UserRegistration(UserRegistrationModel userregistration);
        Task<long> AddEmployee(UserRegistrationModel userregistration);
        Task<long> EditUser(UserRegistrationModel userregistration);
        Task<long> SetPassword(UserPasswordModel userpassoword);
        Task<List<UserRegistrationModel>> GetAllUsers();
        Task<UserRegistrationModel> GetAllUsersAdd();
        Task<UserRegistrationModel> GetUserById(long id);
        Task<User> UserLogIn(UserLogInModel usermodel);

    }
}
