// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet.Versioning
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class NuGetVersion
    {
        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }
        public int Revision { get; } // For NuGet's System.Version compatibility
        public IReadOnlyList<string>? Prerelease { get; }
        public string? Metadata { get; }

        public string NormalizedString => _normalizedString.Value;

        private Lazy<string> _normalizedString;

        public NuGetVersion(int major, int minor, int patch, int revision, IReadOnlyList<string>? prerelease, string? metadata)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Revision = revision;
            Prerelease = prerelease;

            if (metadata != null && string.IsNullOrEmpty(metadata))
            {
                throw new ArgumentException("Metadata cannot be empty", nameof(metadata));
            }
            Metadata = metadata;

            _normalizedString = new Lazy<string>(ToNormalizedString);
        }

        public override string ToString() => _normalizedString.Value;


        private string ToNormalizedString()
        {
            var sb = new StringBuilder();
            sb.Append(Major);
            sb.Append('.');
            sb.Append(Minor);
            sb.Append('.');
            sb.Append(Patch);

            if (Prerelease != null)
            {
                sb.Append("-");
                for (int segment = 0; segment < Prerelease.Count; segment++)
                {
                    if (segment > 0)
                    {
                        sb.Append(".");
                    }
                    sb.Append(Prerelease[segment]);
                }
            }

            if (Metadata != null)
            {
                sb.Append("+");
                sb.Append(Metadata);
            }

            return sb.ToString();
        }
    }
}
