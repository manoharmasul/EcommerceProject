using Dapper;
using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;

namespace EcommerceProject.Repository
{
    public class UserAsyncRepository : IUserAsyncRepository
    {
        private readonly DapperContext context;
        public UserAsyncRepository(DapperContext context)
        {
            this.context = context;
        }

        public Task<long> EditUser(UserRegistrationModel userregistration)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserRegistrationModel>> GetAllUsers()
        {
            var query = @"select * from tblUser where IsDeleted=0";
            using(var connection=context.CreateConnection())
            {
                var result=await connection.QueryAsync<UserRegistrationModel>(query);
                return result.ToList();
            }
        }

        public async Task<User> UserLogIn(UserLogInModel usermodel)
        {
            var query = @"select * from tblUser where (UserName=@UserName or EmailId=@EmailId) and Password=@Password and IsDeleted=0";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QueryAsync<User>(query, new {UserName=usermodel.UserName,Password=usermodel.Password,EmailId=usermodel.UserName});
                return result.FirstOrDefault();
            }
        }

        public async Task<UserRegistrationModel> GetUserById(long id)
        {
            var query = @"select * from tblUser Where Id=@Id";
            using(var connection=context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<UserRegistrationModel>(query, new { Id = id });
                
                return result;
            }
        }

        public async Task<long> SetPassword(UserPasswordModel userpassoword)
        {
            var query = @"update tblUser set Password=@Password,CreatedBy=@CreatedBy where Id=@Id";
            using(var connection=context.CreateConnection())
            {
                var result=await connection.ExecuteAsync(query, new {Password=userpassoword.Password,CreatedBy=userpassoword.Id,Id=userpassoword.Id});
                return result;
            }
        }

        public async Task<long> UserRegistration(UserRegistrationModel userregistration)
        {
            var query = "insert into tblUser(UserName,FirstName,LastName,EmailId,MobileNo,DateOfBirth,Gender,CreatedDate,IsDeleted,[Role],RId) values(@UserName,@FirstName,@LastName,@EmailId,@MobileNo,@DateOfBirth,@Gender,GetDate(),0,'Customer',2);SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = context.CreateConnection())
            {
                userregistration.Role = "Customer";

                var checkmobile = await connection.QueryFirstOrDefaultAsync(@"select * from tblUser Where IsDeleted=0 and MobileNo=@MobileNo", new { MobileNo = userregistration.MobileNo });
                if (checkmobile != null)
                {
                    return -1;//Mobile No Already Present
                }
                var checkemail = await connection.QueryFirstOrDefaultAsync(@"select * from tblUser where IsDeleted=0 and EmailId=@EmailId", new { EmailId = userregistration.EmailId });
                if (checkemail != null)
                {
                    return -2; //Email Id Already Present
                }
                string ss = userregistration.FirstName + userregistration.LastName;
                var checkuname = await connection.QueryFirstOrDefaultAsync
                    (
                    @"select * from tblUser where IsDeleted=0 and UserName=@UserName",
                   new {UserName=ss});
                if (checkuname == null)
                {
                    userregistration.UserName = userregistration.FirstName + userregistration.LastName;
                }
                if (checkuname != null)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Random rnd = new Random();
                        int xx = rnd.Next(999);
                        ss += xx;
                        var checkagain = await connection.QueryFirstOrDefaultAsync
                           (@"select * from tblUser where UserName=@UserName and IsDeleted=0", new { UserName = ss });
                        if (checkagain == null)
                        {
                            userregistration.UserName = ss;
                            break;
                        }

                    }

                }
                var result = await connection.QuerySingleAsync<long>(query, userregistration);
                return result;
            }

        }

        public async Task<long> AddEmployee(UserRegistrationModel userregistration)
        {
            var query = "insert into tblUser(UserName,FirstName,LastName,EmailId,MobileNo,DateOfBirth,Gender,CreatedDate,IsDeleted,[Role],RoleId) values(@UserName,@FirstName,@LastName,@EmailId,@MobileNo,@DateOfBirth,@Gender,GetDate(),0,'Customer',@RoleId);SELECT CAST(SCOPE_IDENTITY() as int)";
           

            using (var connection = context.CreateConnection())
            {
                userregistration.Role = "Customer";

                var checkmobile = await connection.QueryFirstOrDefaultAsync(@"select * from tblUser Where IsDeleted=0 and MobileNo=@MobileNo", new { MobileNo = userregistration.MobileNo });
                if (checkmobile != null)
                {
                    return -1;//Mobile No Already Present
                }
                var checkemail = await connection.QueryFirstOrDefaultAsync(@"select * from tblUser where IsDeleted=0 and EmailId=@EmailId", new { EmailId = userregistration.EmailId });
                if (checkemail != null)
                {
                    return -2; //Email Id Already Present
                }
                string ss = userregistration.FirstName + userregistration.LastName;
                var checkuname = await connection.QueryFirstOrDefaultAsync
                    (
                    @"select * from tblUser where IsDeleted=0 and UserName=@UserName",
                   new { UserName = ss });
                if (checkuname == null)
                {
                    userregistration.UserName = userregistration.FirstName + userregistration.LastName;
                }
                if (checkuname != null)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Random rnd = new Random();
                        int xx = rnd.Next(999);
                        ss += xx;
                        var checkagain = await connection.QueryFirstOrDefaultAsync
                           (@"select * from tblUser where UserName=@UserName and IsDeleted=0", new { UserName = ss });
                        if (checkagain == null)
                        {
                            userregistration.UserName = ss;
                            break;
                        }

                    }

                }
               
                var result = await connection.QuerySingleAsync<long>(query, userregistration);
               
                return result;

            }
        }

        public async Task<UserRegistrationModel> GetAllUsersAdd()
        {
            UserRegistrationModel urm = new UserRegistrationModel();
            var query = @"select Id as RoleId,Type as RoleType from tblUserType";
            using(var connection=context.CreateConnection())
            {
                var result = await connection.QueryAsync<UserTypes>(query);
                urm.userTypes = result.ToList();
                return urm;
            }
        }

        public async Task<long> AddWalletBalance(UserRegistrationModel addwalletbalace)
        {
            var query = @"Update tblUser set WalletBalance=@WalletBalance where Id=@Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, addwalletbalace);
                return result;
            }
        }

      
    }
}
