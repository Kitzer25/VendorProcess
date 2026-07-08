using System.Text;
using Domain.Commons;
using Infraestructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

/*
 * DBContext
 */
builder.Services.AddDbContext<AppDbContext>(o =>
{
    var dbServer = builder.Configuration["DataBaseConnection"];
    var connectionString = builder.Configuration["ConnectionStrings"];
    
    //Verificación de Servidor en caso de cambio
    if (dbServer == "Postgres")
    {
        o.UseNpgsql(connectionString);
    }
    else if (dbServer == "MySql")
    {
        o.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    }
});



/*
 * JWT
 */
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]
                                       ?? throw new InvalidOperationException("SecretKey is null")
                ))
        };
    });

/*
 * JWT Policies
 */
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyNames.AdminPolicy,
        p => p.RequireRole("Moder"));
    options.AddPolicy(PolicyNames.EmployeesPolicy,
        p => p.RequireRole("Internal"));
    options.AddPolicy(PolicyNames.PharmacystPolicy,
            p => p.RequireRole("Pharmacyst"));
});



/* SWAGGER */
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/* JWT Initialization */
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
