using System;
using System.Collections.Generic;
using NadekoBot.Common.Yml;

namespace NadekoBot.Core.Modules.EchoAdditions.Services
{
    public sealed class EchoConfig
    {
        public EchoConfig()
        {
        }
        

        [Comment(@"Minimum amount hugs")]
        public int MinHugs { get; set; } = 0;
    }
}