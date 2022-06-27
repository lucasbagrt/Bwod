using Bwod.OrderAPI.Repository;
using Bwod.OrderAPI.MessageConsumer;
using Bwod.OrderAPI.Model.Context;
using Bwod.OrderAPI.RabbitMQSender;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Bwod.OrderAPI.Config;
using Bwod.OrderAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connection = builder.Configuration["MySQLConnection:MySQlConnectionString"];
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connection,
    new MySqlServerVersion(new Version(8, 0, 29))));

var bld = new DbContextOptionsBuilder<MySQLContext>();
bld.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 29)));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
services.AddSingleton(mapper);
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddHostedService<RabbitMQCheckoutConsumer>(); 
services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();
services.AddSingleton(new OrderRepository(bld.Options));
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddControllers();
services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "bwod");
    });
});

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bwod.OrderAPI v1", Version = "v1" });    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token!",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();