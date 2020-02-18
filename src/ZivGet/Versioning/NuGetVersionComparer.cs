// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet.Versioning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    internal class NuGetVersionComparer : IComparer<NuGetVersion>
    {
        public static NuGetVersionComparer Default { get; } = new NuGetVersionComparer(ignoreMetadata: true);
        public static NuGetVersionComparer Sorting { get; } = new NuGetVersionComparer(ignoreMetadata: false);

        public NuGetVersionComparer(bool ignoreMetadata)
        {
            _ignoreMetadata = ignoreMetadata;
        }

        private readonly bool _ignoreMetadata;

        public int Compare([AllowNull] NuGetVersion x, [AllowNull] NuGetVersion y)
        {
            if (x == null) { throw new ArgumentNullException(nameof(x)); }
            if (y == null) { throw new ArgumentNullException(nameof(y)); }

            var intComparer = Comparer<int>.Default;
            var stringComparer = StringComparer.Ordinal;

            var compareResult = intComparer.Compare(x.Major, y.Major);
            if (compareResult != 0)
            {
                return compareResult;
            }

            compareResult = intComparer.Compare(x.Minor, y.Minor);
            if (compareResult != 0)
            {
                return compareResult;
            }

            compareResult = intComparer.Compare(x.Patch, y.Patch);
            if (compareResult != 0)
            {
                return compareResult;
            }

            compareResult = intComparer.Compare(x.Revision, y.Revision);
            if (compareResult != 0)
            {
                return compareResult;
            }

            if (x.Prerelease != null || y.Prerelease != null)
            {
                if (x.Prerelease == null)
                {
                    return 1;
                }

                if (y.Prerelease == null)
                {
                    return -1;
                }

                int segmentsToCompare = Math.Min(x.Prerelease.Count, y.Prerelease.Count);
                for (int segment = 0; segment < segmentsToCompare; segment++)
                {
                    var xSegment = x.Prerelease[segment];
                    var ySegment = y.Prerelease[segment];
                    if (int.TryParse(xSegment, out int xSegmentValue) && int.TryParse(ySegment, out int ySegmentValue))
                    {
                        compareResult = intComparer.Compare(xSegmentValue, ySegmentValue);
                        if (compareResult != 0)
                        {
                            return compareResult;
                        }
                    }
                    else
                    {
                        compareResult = stringComparer.Compare(xSegment, ySegment);
                        if (compareResult != 0)
                        {
                            return compareResult;
                        }
                    }
                }

                if (x.Prerelease.Count > y.Prerelease.Count)
                {
                    return 1;
                }

                if (x.Prerelease.Count < y.Prerelease.Count)
                {
                    return -1;
                }
            }

            if (!_ignoreMetadata)
            {
                compareResult = stringComparer.Compare(x.Metadata, y.Metadata);
                if (compareResult != 0)
                {
                    return compareResult;
                }
            }

            return 0;
        }
    }
}
