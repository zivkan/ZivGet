// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet
{
    using System.CommandLine;
    using System.CommandLine.Builder;
    using System.CommandLine.Hosting;
    using System.CommandLine.Parsing;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ZivGet.Commands;

    internal class Program
    {
        internal static async Task<int> Main(string[] args)
        {
            Parser parser = new CommandLineBuilder(GetRootCommand())
                .UseDefaults()
                .UseHost(host =>
                {
                    host.ConfigureServices(ConfigureServices);
                })
                .Build();

            return await parser.InvokeAsync(args);
        }

        internal static RootCommand GetRootCommand()
        {
            return new RootCommand()
            {
                RestoreCommand.GetCommand()
            };
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
        }
    }
}
