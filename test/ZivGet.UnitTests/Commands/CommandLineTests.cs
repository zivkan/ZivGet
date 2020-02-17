// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet.UnitTests.Commands
{
    using System.CommandLine;
    using System.CommandLine.Parsing;
    using System.Linq;
    using Xunit;

    public class CommandLineTests
    {
        [Fact]
        public void NoArgsShowHelp()
        {
            RootCommand rootCommand = Program.GetRootCommand();
            Parser parser = new Parser(rootCommand);
            ParseResult result = parser.Parse("");

            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Required command was not provided.", result.Errors.First().Message);
        }

        [Fact]
        public void Restore_NoArgsOk()
        {
            RootCommand rootCommand = Program.GetRootCommand();
            Parser parser = new Parser(rootCommand);
            ParseResult result = parser.Parse("restore");

            Assert.Equal(0, result.Errors.Count);
        }

        [Fact]
        public void Restore_WithTarget()
        {
            RootCommand rootCommand = Program.GetRootCommand();
            Parser parser = new Parser(rootCommand);
            ParseResult result = parser.Parse(new[] { "restore", @"c:\path\" });

            Assert.Equal(0, result.Errors.Count);
        }
    }
}
