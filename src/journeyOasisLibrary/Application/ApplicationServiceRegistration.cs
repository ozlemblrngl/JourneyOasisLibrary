using System.Reflection;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Application.Pipelines.Validation;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Abstraction;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Configurations;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Serilog.File;
using NArchitecture.Core.ElasticSearch;
using NArchitecture.Core.ElasticSearch.Models;
using NArchitecture.Core.Localization.Resource.Yaml.DependencyInjection;
using NArchitecture.Core.Mailing;
using NArchitecture.Core.Mailing.MailKit;
using NArchitecture.Core.Security.DependencyInjection;
using Application.Services.AnalogueBooks;
using Application.Services.Books;
using Application.Services.BookFormats;
using Application.Services.EBooks;
using Application.Services.Formats;
using Application.Services.Languages;
using Application.Services.LanguageBooks;
using Application.Services.Libraries;
using Application.Services.Materials;
using Application.Services.Publishers;
using Application.Services.PublisherBooks;
using Application.Services.Subjects;
using Application.Services.SubjectBooks;
using Application.Services.Translators;
using Application.Services.TranslatorBooks;
using Application.Services.Writers;
using Application.Services.WriterBooks;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        MailSettings mailSettings,
        FileLogConfiguration fileLogConfiguration,
        ElasticSearchConfig elasticSearchConfig
    )
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMailService, MailKitMailService>(_ => new MailKitMailService(mailSettings));
        services.AddSingleton<ILogger, SerilogFileLogger>(_ => new SerilogFileLogger(fileLogConfiguration));
        services.AddSingleton<IElasticSearch, ElasticSearchManager>(_ => new ElasticSearchManager(elasticSearchConfig));

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();

        services.AddYamlResourceLocalization();

        services.AddSecurityServices<Guid, int>();

        services.AddScoped<IAnalogueBookService, AnalogueBookManager>();
        services.AddScoped<IBookService, BookManager>();
        services.AddScoped<IBookFormatService, BookFormatManager>();
        services.AddScoped<IEBookService, EBookManager>();
        services.AddScoped<IFormatService, FormatManager>();
        services.AddScoped<ILanguageService, LanguageManager>();
        services.AddScoped<ILanguageBookService, LanguageBookManager>();
        services.AddScoped<ILibraryService, LibraryManager>();
        services.AddScoped<IMaterialService, MaterialManager>();
        services.AddScoped<IPublisherService, PublisherManager>();
        services.AddScoped<IPublisherBookService, PublisherBookManager>();
        services.AddScoped<ISubjectService, SubjectManager>();
        services.AddScoped<ISubjectBookService, SubjectBookManager>();
        services.AddScoped<ITranslatorService, TranslatorManager>();
        services.AddScoped<ITranslatorBookService, TranslatorBookManager>();
        services.AddScoped<IWriterService, WriterManager>();
        services.AddScoped<IWriterBookService, WriterBookManager>();
        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
