public interface IUserServices{
    Task<bool> UserIsInDb(string email);
    Task<bool> AddUserToDatabase(User user);
    Task<string> CheckedUserInDb(UserDto user);
}