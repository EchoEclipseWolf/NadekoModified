using NadekoBot.Core.Common;
using NadekoBot.Core.Common.Configs;
using NadekoBot.Core.Modules.Gambling.Common;
using NadekoBot.Core.Services;

namespace NadekoBot.Core.Modules.EchoAdditions.Services
{
    public sealed class EchoConfigService : ConfigServiceBase<EchoConfig>
    {
        public override string Name { get; } = "echo";
        private const string FilePath = "data/echo.yml";
        private static TypedKey<EchoConfig> changeKey = new TypedKey<EchoConfig>("config.echo.updated");

        
        public EchoConfigService(IConfigSeria serializer, IPubSub pubSub)
            : base(FilePath, serializer, pubSub, changeKey)
        {
            AddParsedProp("minbet", gs => gs.MinHugs, int.TryParse, ConfigPrinters.ToString, val => val >= 0);
        }
    }
}