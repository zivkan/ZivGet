// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet.Commands
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    internal class RestoreCommand
    {
        internal static Command GetCommand()
        {
            var command = new Command("restore")
            {
                Handler = CommandHandler.Create<IHost, RestoreArgs>(Invoke)
            };

            command.Add(
                new Argument<string>("targets")
                {
                    Arity = ArgumentArity.ZeroOrMore
                });

            return command;
        }

        internal static Task<int> Invoke(IHost host, RestoreArgs restoreArgs)
        {
            // Grand idea for simple restore entry point:

            // RestoreContext context = new RestoreContext(); // has singletons like logger, download manager, extraction manager, etc
            // ValidateArguments(restoreArgs) // check input projects exist, etc.
            // IReadOnlyList<Project> projects = GetProjectsToRestore(restoreArgs, context); // expand sln file. Find project references not included on command line or sln
            // IReadOnlyList<IDictionary<Project, RestoreGraph>> restoreGraphs = CalculateRestoreGraphs(projects);
            // DownloadAndExtractNupkgs(restoreGraphs);
            // WriteAssetsFiles(restoreGraphs);

            return Task.FromResult(0);
        }
    }
}
