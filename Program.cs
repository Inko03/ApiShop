using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

//adding services
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<ProductServices>();
builder.Services.AddScoped<OrderServices>();
builder.Services.AddScoped<CartServices>();
builder.Services.AddScoped<TokenServices>();
builder.Services.AddHttpContextAccessor();
//adding authentication
builder.Services.AddAuthentication(options=>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme="Bearer";
}).AddJwtBearer(cfg=>{
cfg.RequireHttpsMetadata = false;
cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "your-issuer",
        ValidAudience = "your-issuer",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey123supersecretkey123"))
};
    cfg.Events = new JwtBearerEvents{
        OnMessageReceived = context =>{
            var accesToken = context.Request.Cookies["AuthToken"];
            if(!accesToken.IsNullOrEmpty()){
                context.Token = accesToken;
            }
            return Task.CompletedTask;
        }
    };
});

//controller
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //json loop
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
//Add database context to program
builder.Services.AddDbContext<DataBaseContext>(options =>
//connection string
options.UseSqlServer("Server=localhost;Database=MyDataBase;Trusted_Connection=True;TrustServerCertificate=True;")
);

var app = builder.Build();

app.UseMiddleware<ErrorMiddleware>();
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

