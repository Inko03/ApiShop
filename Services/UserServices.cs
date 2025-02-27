using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserServices{
    private readonly DataBaseContext databaseContext;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly TokenServices tokenServices;
    public UserServices(DataBaseContext _dataBaseContext,TokenServices _tokenServices, IPasswordHasher<User> _passwordHasher){
        databaseContext = _dataBaseContext;
        passwordHasher = _passwordHasher;
        tokenServices = _tokenServices;
    }
    public async Task<bool> UserIsInDb(string email){

        if(email!=null){
            var result = await databaseContext.Users
            .FirstOrDefaultAsync(p=>p.Email==email);
            if(result!=null){
                throw new UnauthorizedAccessException("User alredy in databse");
            }
        }
        return false;

    }
    public async Task<bool> AddUserToDatabase(User user){
        await UserIsInDb(user.Email);
        var HashedPassword = passwordHasher.HashPassword(user,user.PasswordHash);
        user.PasswordHash =  HashedPassword;
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();
        return true;
    }
    public async Task<string> CheckedUserInDb(UserDto user){
        var userInDb = await databaseContext.Users.FirstOrDefaultAsync(p=>p.Email==user.Email);
        if(userInDb!=null){
            var result = passwordHasher.VerifyHashedPassword(userInDb,userInDb.PasswordHash,user.PasswordHash);
            if(result==PasswordVerificationResult.Failed){
                throw new UnauthorizedAccessException("Wrong password");
            }
            var token = tokenServices.GenerateToken(userInDb.Name,userInDb.Id,userInDb.Role);
            return token;
        }
        throw new UnauthorizedAccessException("No user in database or wrong email");
    }
}