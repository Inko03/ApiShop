using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserServices:IUserServices{
    private readonly DataBaseContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenServices _tokenServices;
    private readonly IUserContextServices _userContextServices;
    public UserServices(DataBaseContext dataBaseContext,ITokenServices tokenServices, IPasswordHasher<User> passwordHasher,IUserContextServices userContextServices){
        _context = dataBaseContext;
        _passwordHasher = passwordHasher;
        _tokenServices = tokenServices;
        _userContextServices = userContextServices;
    }
    public async Task<bool> UserIsInDb(string email){

        if(email!=null){
            var result = await _context.Users
            .FirstOrDefaultAsync(p=>p.Email==email);
            if(result!=null){
                return false;
            }
        }
        return false;

    }
    public async Task<Result<bool>> AddUserToDatabase(User user){
        if(await UserIsInDb(user.Email)){
            return Result<bool>.Failure("User alredy in databse");
        }

        var HashedPassword = _passwordHasher.HashPassword(user,user.PasswordHash);
        user.PasswordHash =  HashedPassword;
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
    public async Task<Result<string>> CheckedUserInDb(UserDto user){
        var userInDb = await _context.Users.FirstOrDefaultAsync(p=>p.Email==user.Email);
        if(userInDb!=null){
            var result = _passwordHasher.VerifyHashedPassword(userInDb,userInDb.PasswordHash,user.PasswordHash);
            if(result==PasswordVerificationResult.Failed){
                return Result<string>.Failure("Wrong password");
            }
            var token = _tokenServices.GenerateToken(userInDb.Name,userInDb.Id,userInDb.Role);
            return Result<string>.Success($"{token}");
        }
        return Result<string>.Failure("No user in database/or wrong email");
    }
    public async Task<Result<User>> GetUser(){
        var userId = _userContextServices.GetUserId(); 
        var userDb = await _context.Users.FirstOrDefaultAsync(o=>o.Id==userId);
        if(userDb is null){
            return Result<User>.Failure("No user in database");
        }
        return Result<User>.Success(userDb);
    }
}