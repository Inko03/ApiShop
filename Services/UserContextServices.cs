using System.Security.Claims;

public class UserContextServices:IUserContextServices{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextServices(IHttpContextAccessor httpContextAccessor){
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetUserId(){
        var userid = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(!Int32.TryParse(userid, out int id)){
            throw new UnauthorizedAccessException("No user id");
        }
        return id;
        
    }
}