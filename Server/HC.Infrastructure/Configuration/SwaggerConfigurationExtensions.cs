using System.Reflection;
using HC.Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Pluralize.NET;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HC.Infrastructure.Configuration;

public static class SwaggerConfigurationExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        Assert.NotNull(services, nameof(services));

        //More info : https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters

        #region AddSwaggerExamples
        //Add services to use Example Filters in swagger
        //If you want to use the Request and Response example filters (and have called options.ExampleFilters() above), then you MUST also call
        //This method to register all ExamplesProvider classes form the assembly
        //services.AddSwaggerExamplesFromAssemblyOf<PersonRequestExample>();

        //We call this method for by reflection with the Startup type of entry assmebly (MyApi assembly)
        var mainAssembly = Assembly.GetEntryAssembly(); // => MyApi project assembly
        var mainType = mainAssembly.GetExportedTypes()[0];

        const string methodName = nameof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions.AddSwaggerExamplesFromAssemblyOf);
        //MethodInfo method = typeof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions).GetMethod(methodName);
        MethodInfo method = typeof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions).GetRuntimeMethods().FirstOrDefault(x => x.Name == methodName && x.IsGenericMethod);
        MethodInfo generic = method.MakeGenericMethod(mainType);
        generic.Invoke(null, new[] { services });
        #endregion

        //Add services and configuration to use swagger
        services.AddSwaggerGen(options =>
        {
            var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "HC.Api.xml");
            //show controller XML comments like summary
            options.IncludeXmlComments(xmlDocPath, true);

            options.EnableAnnotations();

            #region DescribeAllEnumsAsStrings
            //This method was Deprecated. 
            //options.DescribeAllEnumsAsStrings();

            //You can specify an enum to convert to/from string, uisng :
            //[JsonConverter(typeof(StringEnumConverter))]
            //public virtual MyEnums MyEnum { get; set; }

            //Or can apply the StringEnumConverter to all enums globaly, using :
            //Used in ServiceCollectionExtensions.AddMinimalMvc
            //.AddNewtonsoftJson(option => option.SerializerSettings.Converters.Add(new StringEnumConverter()));
            #endregion

            //options.DescribeAllParametersInCamelCase();
            //options.DescribeStringEnumsInCamelCase()
            //options.UseReferencedDefinitionsForEnums()
            //options.IgnoreObsoleteActions();
            //options.IgnoreObsoleteProperties();

            options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "API V1" });
            options.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "API V2" });

            #region Filters
            //Enable to use [SwaggerRequestExample] & [SwaggerResponseExample]
            options.ExampleFilters();

            //It doesn't work anymore in recent versions because of replacing Swashbuckle.AspNetCore.Examples with Swashbuckle.AspNetCore.Filters
            //Adds an Upload button to endpoints which have [AddSwaggerFileUploadButton]
            //options.OperationFilter<AddFileParamTypesOperationFilter>();

            //Set summary of action if not already set
            options.OperationFilter<ApplySummariesOperationFilter>();

            #region Add UnAuthorized to Response
            //Add 401 response and security requirements (Lock icon) to actions that need authorization
            options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "OAuth2");
            #endregion

            #region Add Jwt Authentication
            //Add Lockout icon on top of swagger ui page to authenticate
            #region Old way
            //options.AddSecurityDefinition("Bearer", new ApiKeyScheme
            //{
            //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            //    Name = "Authorization",
            //    In = "header"
            //});
            //options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
            //{
            //    {"Bearer", new string[] { }}
            //});
            #endregion

            //options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "OAuth2" }
            //        },
            //        Array.Empty<string>() //new[] { "readAccess", "writeAccess" }
            //    }
            //});

            //OAuth2Scheme
            options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                //Scheme = "Bearer",
                //In = ParameterLocation.Header,
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri("/api/v1/users/Token", UriKind.Relative),
                        //AuthorizationUrl = new Uri("/api/v1/users/Token", UriKind.Relative)
                        //Scopes = new Dictionary<string, string>
                        //{
                        //    { "readAccess", "Access read operations" },
                        //    { "writeAccess", "Access write operations" }
                        //}
                    }
                }
            });
            #endregion

            #region Versioning
            // Remove version parameter from all Operations
            options.OperationFilter<RemoveVersionParameters>();

            //set version "api/v{version}/[controller]" from current swagger doc verion
            options.DocumentFilter<SetVersionInPaths>();

            //Seperate and categorize end-points by doc version
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                var versions = methodInfo.DeclaringType
                    .GetCustomAttributes<ApiVersionAttribute>(true)
                    .SelectMany(attr => attr.Versions);

                return versions.Any(v => $"v{v}" == docName);
            });
            #endregion

            //If use FluentValidation then must be use this package to show validation in swagger (MicroElements.Swashbuckle.FluentValidation)
            //options.AddFluentValidationRules();
            #endregion
        });
    }

    public static IApplicationBuilder UseSwaggerAndUI(this IApplicationBuilder app)
    {
        Assert.NotNull(app, nameof(app));

        //More info : https://github.com/domaindrivendev/Swashbuckle.AspNetCore

        //Swagger middleware for generate "Open API Documentation" in swagger.json
        app.UseSwagger(/*options =>
        {
            options.RouteTemplate = "api-docs/{documentName}/swagger.json";
        }*/);

        //Swagger middleware for generate UI from swagger.json
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");

            #region Customizing
            //// Display
            //options.DefaultModelExpandDepth(2);
            //options.DefaultModelRendering(ModelRendering.Model);
            //options.DefaultModelsExpandDepth(-1);
            //options.DisplayOperationId();
            //options.DisplayRequestDuration();
            options.DocExpansion(DocExpansion.None);
            //options.EnableDeepLinking();
            //options.EnableFilter();
            //options.MaxDisplayedTags(5);
            //options.ShowExtensions();

            //// Network
            //options.EnableValidator();
            //options.SupportedSubmitMethods(SubmitMethod.Get);

            //// Other
            //options.DocumentTitle = "CustomUIConfig";
            //options.InjectStylesheet("/ext/custom-stylesheet.css");
            //options.InjectJavascript("/ext/custom-javascript.js");
            //options.RoutePrefix = "api-docs";
            #endregion
        });

        //ReDoc UI middleware. ReDoc UI is an alternative to swagger-ui
        app.UseReDoc(options =>
        {
            options.SpecUrl("/swagger/v1/swagger.json");
            //options.SpecUrl("/swagger/v2/swagger.json");

            #region Customizing
            //By default, the ReDoc UI will be exposed at "/api-docs"
            //options.RoutePrefix = "docs";
            //options.DocumentTitle = "My API Docs";

            options.EnableUntrustedSpec();
            options.ScrollYOffset(10);
            options.HideHostname();
            options.HideDownloadButton();
            options.ExpandResponses("200,201");
            options.RequiredPropsFirst();
            options.NoAutoAuth();
            options.PathInMiddlePanel();
            options.HideLoading();
            options.NativeScrollbars();
            options.DisableSearch();
            options.OnlyRequiredInSamples();
            options.SortPropsAlphabetically();
            #endregion
        });

        return app;
    }
}

public class ApplySummariesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor == null) return;

        var pluralizer = new Pluralizer();

        var actionName = controllerActionDescriptor.ActionName;
        var singularizeName = pluralizer.Singularize(controllerActionDescriptor.ControllerName);
        var pluralizeName = pluralizer.Pluralize(singularizeName);

        var parameterCount = operation.Parameters.Where(p => p.Name != "version" && p.Name != "api-version").Count();

        if (IsGetAllAction())
        {
            if (!operation.Summary.HasValue())
                operation.Summary = $"Returns all {pluralizeName}";
        }
        else if (IsActionName("Post", "Create"))
        {
            if (!operation.Summary.HasValue())
                operation.Summary = $"Creates a {singularizeName}";

            if (!operation.Parameters[0].Description.HasValue())
                operation.Parameters[0].Description = $"A {singularizeName} representation";
        }
        else if (IsActionName("Read", "Get"))
        {
            if (!operation.Summary.HasValue())
                operation.Summary = $"Retrieves a {singularizeName} by unique id";

            if (!operation.Parameters[0].Description.HasValue())
                operation.Parameters[0].Description = $"a unique id for the {singularizeName}";
        }
        else if (IsActionName("Put", "Edit", "Update"))
        {
            if (!operation.Summary.HasValue())
                operation.Summary = $"Updates a {singularizeName} by unique id";

            //if (!operation.Parameters[0].Description.HasValue())
            //    operation.Parameters[0].Description = $"A unique id for the {singularizeName}";

            if (!operation.Parameters[0].Description.HasValue())
                operation.Parameters[0].Description = $"A {singularizeName} representation";
        }
        else if (IsActionName("Delete", "Remove"))
        {
            if (!operation.Summary.HasValue())
                operation.Summary = $"Deletes a {singularizeName} by unique id";

            if (!operation.Parameters[0].Description.HasValue())
                operation.Parameters[0].Description = $"A unique id for the {singularizeName}";
        }

        #region Local Functions
        bool IsGetAllAction()
        {
            foreach (var name in new[] { "Get", "Read", "Select" })
            {
                if (actionName.Equals(name, StringComparison.OrdinalIgnoreCase) && parameterCount == 0 ||
                    actionName.Equals($"{name}All", StringComparison.OrdinalIgnoreCase) ||
                    actionName.Equals($"{name}{pluralizeName}", StringComparison.OrdinalIgnoreCase) ||
                    actionName.Equals($"{name}All{singularizeName}", StringComparison.OrdinalIgnoreCase) ||
                    actionName.Equals($"{name}All{pluralizeName}", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        bool IsActionName(params string[] names)
        {
            foreach (var name in names)
            {
                if (actionName.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                    actionName.Equals($"{name}ById", StringComparison.OrdinalIgnoreCase) ||
                    actionName.Equals($"{name}{singularizeName}", StringComparison.OrdinalIgnoreCase) ||
                    actionName.Equals($"{name}{singularizeName}ById", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}

public class RemoveVersionParameters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Remove version parameter from all Operations
        var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");
        if (versionParameter != null)
            operation.Parameters.Remove(versionParameter);
    }
}

public class SetVersionInPaths : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var updatedPaths = new OpenApiPaths();

        foreach (var entry in swaggerDoc.Paths)
        {
            updatedPaths.Add(
                entry.Key.Replace("v{version}", swaggerDoc.Info.Version),
                entry.Value);
        }

        swaggerDoc.Paths = updatedPaths;
    }
}

public class UnauthorizedResponsesOperationFilter : IOperationFilter
{
    private readonly bool includeUnauthorizedAndForbiddenResponses;
    private readonly string schemeName;

    public UnauthorizedResponsesOperationFilter(bool includeUnauthorizedAndForbiddenResponses, string schemeName = "Bearer")
    {
        this.includeUnauthorizedAndForbiddenResponses = includeUnauthorizedAndForbiddenResponses;
        this.schemeName = schemeName;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var filters = context.ApiDescription.ActionDescriptor.FilterDescriptors;
        var metadta = context.ApiDescription.ActionDescriptor.EndpointMetadata;

        var hasAnonymous = filters.Any(p => p.Filter is AllowAnonymousFilter) || metadta.Any(p => p is AllowAnonymousAttribute);
        if (hasAnonymous) return;

        var hasAuthorize = filters.Any(p => p.Filter is AuthorizeFilter) || metadta.Any(p => p is AuthorizeAttribute);
        if (!hasAuthorize) return;

        if (includeUnauthorizedAndForbiddenResponses)
        {
            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
        }

        operation.Security.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Scheme = schemeName,
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "OAuth2" }
                },
                Array.Empty<string>() //new[] { "readAccess", "writeAccess" }
            }
        });
    }
}