// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet.UnitTests.Commands
{
    using System.CommandLine;
    using System.CommandLine.Parsing;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;
    using ZivGet.Commands;

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
            Command command = RestoreCommand.GetCommand(AssertTest);
            Parser parser = new Parser(command);
            ParseResult result = parser.Parse(new[] { "restore", @"c:\path\" });

            Assert.Equal(0, result.Errors.Count);
            result.Invoke();

            static Task<int> AssertTest(RestoreArgs restoreArgs)
            {
                restoreArgs.Targets.Length.Should().Be(1);
                restoreArgs.Targets[0].Should().Be(@"c:\path\");
                return Task.FromResult(0);
            }
        }

        [Fact]
        public void Restore_WithMultipleTargets()
        {
            Command command = RestoreCommand.GetCommand(AssertTest);
            Parser parser = new Parser(command);
            ParseResult result = parser.Parse(new[] { "restore", @"c:\path\", @"c:\path2\" });

            Assert.Equal(0, result.Errors.Count);
            result.Invoke();

            static Task<int> AssertTest(RestoreArgs restoreArgs)
            {
                restoreArgs.Targets.Length.Should().Be(2);
                restoreArgs.Targets.Should().Contain(new[] { @"c:\path\", @"c:\path2\" });
                return Task.FromResult(0);
            }
        }
    }
}
