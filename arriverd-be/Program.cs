using arriverd_be;
using arriverd_be.Data;
using arriverd_be.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// EF Core
builder.Services.AddDbContext<ArriveDbContext>(options
    //=> options.UseSqlServer("Data Source=arriverd.database.windows.net;Initial Catalog=arriverd;Authentication=Active Directory Default;Encrypt=True;"));
    => options.UseSqlServer("Server=LTP-PCN-05\\PRIMEDEV03;Database=arriverd;User Id=sa;Password=fx=25tb;TrustServerCertificate=True"));

builder.Services.AddScoped<IBlobService, BlobService>();

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ArriveDbContext>()
    .AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AuthResponsesOperationFilter>();

    options.CustomSchemaIds(type => type.ToString().Replace('+', '.'));
    options.AddSecurityDefinition(Constants.JWT_SECURITY_SCHEMA.Reference.Id, Constants.JWT_SECURITY_SCHEMA);
});

// Authenticaton
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Middlewares
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ArriveDbContext>();

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
