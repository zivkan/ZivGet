// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet.Commands
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Binding;
    using System.Threading.Tasks;

    internal class RestoreCommand
    {
        internal static Command GetCommand()
        {
            return GetCommand(Invoke);
        }

        internal static Command GetCommand(Func<RestoreArgs, Task<int>> handler)
        {
            var command = new Command("restore");

            var targets = new Argument<string[]>("targets")
            {
                Arity = ArgumentArity.ZeroOrMore
            };
            command.Add(targets);

            var restoreArgsBinder = new RestoreArgsBinder(targets);

            command.SetHandler(handler, restoreArgsBinder);

            return command;
        }

        internal static Task<int> Invoke(RestoreArgs restoreArgs)
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

        private class RestoreArgsBinder : BinderBase<RestoreArgs>
        {
            private Argument<string[]> _targets;

            public RestoreArgsBinder(Argument<string[]> targets)
            {
                _targets = targets;
            }

            protected override RestoreArgs GetBoundValue(BindingContext bindingContext)
            {
                string[] targets = bindingContext.ParseResult.GetValueForArgument(_targets);

                var restoreArgs = new RestoreArgs(targets);

                return restoreArgs;
            }
        }
    }
}
