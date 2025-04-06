using FluentValidation;
using LibraryService.Application.Mappings;
using LibraryService.Application.Validators;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Contracts.Services.Additional;
using LibraryService.Core.Contracts.Services.Main;
using LibraryService.Infrastructure.Context;
using LibraryService.Infrastructure.Converter;
using LibraryService.Infrastructure.Repositories;
using LibraryService.Infrastructure.Services.Additional;
using LibraryService.Infrastructure.Services.Main;
using LibraryService.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LibraryService.API.Extensions;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LibraryContext>(opt =>
            opt.UseNpgsql(builder.Configuration.GetConnectionString("LibraryDbString")));

        return builder;
    }

    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();

        return builder;
    }

    public static WebApplicationBuilder AddAdditionalServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
        builder.Services.AddTransient<IJwtTokenProvider, JwtTokenProvider>();

        return builder;
    }

    public static WebApplicationBuilder AddMainServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRegisterService, RegisterService>();
        builder.Services.AddTransient<IAuthService, AuthService>();

        return builder;
    }

    public static WebApplicationBuilder AddMapping(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(UserProfile));
        builder.Services.AddAutoMapper(typeof(AuthProfile));
        builder.Services.AddAutoMapper(typeof(AuthorProfile));
        builder.Services.AddAutoMapper(typeof(LoanProfile));
        builder.Services.AddAutoMapper(typeof(BookProfile));

        return builder;
    }

    public static WebApplicationBuilder AddValidation(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssembly(typeof(LoginRequestDtoValidator).Assembly);
        builder.Services.AddValidatorsFromAssembly(typeof(RegisterRequestDtoValidator).Assembly);
        builder.Services.AddValidatorsFromAssembly(typeof(UserEntityValidator).Assembly);
        builder.Services.AddValidatorsFromAssembly(typeof(AuthorEntityValidator).Assembly);
        builder.Services.AddValidatorsFromAssembly(typeof(BookEntityValidator).Assembly);
        builder.Services.AddValidatorsFromAssembly(typeof(LoanEntityValidator).Assembly);

        return builder;
    }

    public static WebApplicationBuilder AddConverters(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
        });

        return builder;
    }

    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<JwtSettings>>().Value);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = false,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)),
            };
        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library Api", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
            });

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

        return builder;
    }

    public static WebApplicationBuilder AddCommandsAndQueries(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(options =>
        {
            // Authoriztion
            options
            .RegisterServicesFromAssemblies(
                typeof(Application.Commands.Auth.Login.LoginCommandHandler).Assembly);
            options
            .RegisterServicesFromAssemblies(
                typeof(Application.Commands.Auth.Refresh.RefreshCommandHandler).Assembly);

            // User
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.User.Create.UserCreateCommandHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.User.Update.UserUpdateCommandHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.User.Delete.UserDeleteCommandHandler).Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.User.GetAll.UsersGetQueryHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.User.GetById.UserGetByIdQueryHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.User.GetByEmail.UserGetByEmailQueryHandler).Assembly);

            //Author
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Author.Create.AuthorCreateCommandHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Author.Update.AuthorUpdateCommandHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Author.Delete.AuthorDeleteCommandHandler).Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Author.GetAll.AuthorsGetQueryHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Author.GetById.AuthorGetByIdQueryHandler).Assembly);

            //Book
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Book.Create.BookCreateCommandHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Book.Update.BookUpdateCommandHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Book.Delete.BookDeleteCommandHandler).Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Book.GetAll.BooksGetQueryHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Book.GetAllByAuthorId.BooksGetByAuthorIdQueryHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Book.GetById.BookGetByIdQueryHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Book.GetByISBN.BookGetByISBNQueryHandler).Assembly);

            // Loan
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Loan.Create.LoanCreateCommandHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Loan.Update.LoanUpdateCommandHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Loan.Delete.LoanDeleteCommandHandler).Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Loan.GetAllByUserId.LoansGetByUserIdQueryHandler).Assembly);
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Loan.GetById.LoanGetByIdQueryHandler).Assembly);
        });

        return builder;
    }

}