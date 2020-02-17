// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet.Commands
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    internal class RestoreCommand
    {
        internal static Task<int> Invoke(IHost host, RestoreArgs restoreArgs)
        {
            if (restoreArgs?.Targets == null)
            {
                Console.WriteLine("targets is null");
            }
            else
            {
                Console.WriteLine("targets = " + restoreArgs.Targets.Length);
                foreach (var file in restoreArgs.Targets)
                {
                    Console.WriteLine(file);
                }
            }
            return Task.FromResult(0);
        }

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
    }
}
