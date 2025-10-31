using Application;
using Core;
using Core.Utilities.Encryption;
using Microsoft.Extensions.Configuration;
using Core.Utilities.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
Core.Utilities.JWT.TokenOptions? tokenOptions = builder.Configuration.GetSection("JwtSettings").Get<Core.Utilities.JWT.TokenOptions>();

if (tokenOptions == null || string.IsNullOrEmpty(tokenOptions.SecurityKey))
{
    // G�venlik anahtar� al�nam�yorsa hata f�rlat
    throw new InvalidOperationException("JWT ayarlar� (JwtSettings) bulunamad� veya SecurityKey eksik.");
}


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
        });
});
builder.Services.AddSingleton<EncryptionHelper>();
builder.Services.AddCoreServices(tokenOptions);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = tokenOptions.Issuer, // Token'� da��tan taraf� do�rula

                ValidateAudience = true,
                ValidAudience = tokenOptions.Audience, // Token'� kullanan taraf� do�rula

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)), // Anahtar� do�rula

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // S�re bitiminde tolerans olmas�n
            };
        }
    );


builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Time Usage Reporting System API", Version = "v1" });

    // Bearer token i�in g�venlik �emas� tan�m�
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // k���k harfle
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT ile kimlik do�rulamas�"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
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
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
