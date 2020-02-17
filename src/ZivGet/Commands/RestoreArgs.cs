// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet.Commands
{
    internal class RestoreArgs
    {
        internal string[] Targets { get; }

        public RestoreArgs(string[] targets)
        {
            Targets = targets;
        }
    }
}
