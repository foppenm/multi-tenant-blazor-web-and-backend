using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddMsalAuthentication(options =>
            {
                var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

                // Todo: Make authority and clientid configurable
                var authentication = options.ProviderOptions.Authentication;

                // Use the /common endpoint so it can be multi tenant
                authentication.Authority = "https://login.microsoftonline.com/common";

                // Azure AD app registration client id
                authentication.ClientId = configuration["clientId"];
                authentication.PostLogoutRedirectUri = configuration["postLogoutUrl"];

                // When retrieving an access token this will be a default used scope
                // Therefore you dont need to specify it everytime you get a token.
                options.ProviderOptions.DefaultAccessTokenScopes.Add($"{configuration["clientId"]}/user_impersonation");
            });

            await builder.Build().RunAsync();
        }
    }
}
