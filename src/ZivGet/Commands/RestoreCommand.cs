// Licensed under the MIT license. See the LICENSE file in the project root for more information.

namespace ZivGet.Commands
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    internal class RestoreCommand
    {
        internal static Task<int> Invoke(IHost host)
        {
            return Task.FromResult(0);
        }
    }
}
