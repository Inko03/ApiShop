public interface IUserServices{
    Task<bool> UserIsInDb(string email);
    Task<Result<bool>> AddUserToDatabase(User user);
    Task<Result<string>> CheckedUserInDb(UserDto user);
    Task<Result<User>> GetUser();
}