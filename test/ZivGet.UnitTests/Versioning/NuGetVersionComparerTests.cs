// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet.UnitTests.Versioning
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;
    using ZivGet.Versioning;

    public class NuGetVersionComparerTests
    {
        internal static IEnumerable<object[]> Compare_VariousVersions_ReturnsCorrectValue_Data()
        {
            var v1_0_0 = new NuGetVersion(1, 0, 0, 0, null, null);
            var v2_0_0 = new NuGetVersion(2, 0, 0, 0, null, null);
            var v2_1_0 = new NuGetVersion(2, 1, 0, 0, null, null);
            var v2_1_1 = new NuGetVersion(2, 1, 1, 0, null, null);
            var v1_0_0_alpha = new NuGetVersion(1, 0, 0, 0, new[] { "alpha" }, null);
            var v1_0_0_alpha_1 = new NuGetVersion(1, 0, 0, 0, new[] { "alpha", "1" }, null);
            var v1_0_0_alpha_beta = new NuGetVersion(1, 0, 0, 0, new[] { "alpha", "beta" }, null);
            var v1_0_0_beta = new NuGetVersion(1, 0, 0, 0, new[] { "beta" }, null);
            var v1_0_0_beta_2 = new NuGetVersion(1, 0, 0, 0, new[] { "beta", "2" }, null);
            var v1_0_0_beta_11 = new NuGetVersion(1, 0, 0, 0, new[] { "beta", "11" }, null);
            var v1_0_0_rc_1 = new NuGetVersion(1, 0, 0, 0, new[] { "rc", "1" }, null);

            return new List<object[]>()
            {
                new object[] { v1_0_0, v2_0_0 },
                new object[] { v2_0_0, v2_1_0 },
                new object[] { v2_1_0, v2_1_1 },
                new object[] { v1_0_0_alpha, v1_0_0 },
                new object[] { v1_0_0_alpha, v1_0_0_alpha_1 },
                new object[] { v1_0_0_alpha_1, v1_0_0_alpha_beta },
                new object[] { v1_0_0_alpha_beta, v1_0_0_beta },
                new object[] { v1_0_0_beta, v1_0_0_beta_2 },
                new object[] { v1_0_0_beta_2, v1_0_0_beta_11 },
                new object[] { v1_0_0_beta_11, v1_0_0_rc_1 },
                new object[] { v1_0_0_rc_1, v1_0_0 }
            };
        }

        [Theory]
        [MemberData(nameof(Compare_VariousVersions_ReturnsCorrectValue_Data))]
        internal void Compare_UnequalVersions_ReturnsCorrectValue(NuGetVersion smaller, NuGetVersion larger)
        {
            NuGetVersionComparer target = NuGetVersionComparer.Default;

            int result = target.Compare(smaller, larger);
            result.Should().BeLessThan(0);

            result = target.Compare(larger, smaller);
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        internal void Compare_DifferentMetadata_ReturnsZero()
        {
            var v1_a = new NuGetVersion(1, 0, 0, 0, null, "a");
            var v1_b = new NuGetVersion(1, 0, 0, 0, null, "b");

            var target = NuGetVersionComparer.Default;

            var result = target.Compare(v1_a, v1_b);
            result.Should().Be(0);
        }
    }
}
