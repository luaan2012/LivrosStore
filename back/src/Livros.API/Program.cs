using System.Text;
using System.Text.Json.Serialization;
using Livros.Data.Interface;
using Livros.Data.Repository;
using Livros.Domain.Models;
using Livros.Service.Interface;
using Livros.Service.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IEbookService, EbookService>();

builder.Services.AddScoped<IEbookRepository, EbookRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<User>();

builder.Services.AddCors();

// Entity core
// builder.Services.AddDbContext<DbLivros>(
//     context => context.UseSqlite(builder.Configuration.GetConnectionString("ConectionString")) 
// );

builder.Services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
        .AddNewtonsoftJson(option => 
            option.SerializerSettings.ReferenceLoopHandling = 
            Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Consulta.API", Version = "V1"});
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = $@"JWT Authorization header usando Bearer.
                    Entre com 'Bearer' [espaço] então coloque seu token.
                    Exemplo: 'Bearer 12345asdac'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
