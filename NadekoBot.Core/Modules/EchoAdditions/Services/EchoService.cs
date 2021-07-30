using Discord.WebSocket;
using NadekoBot.Core.Modules.Gambling.Common;
using NadekoBot.Core.Services;
using NadekoBot.Modules.Gambling.Common.Connect4;
using NadekoBot.Modules.Gambling.Common.WheelOfFortune;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NadekoBot.Core.Modules.EchoAdditions.Services;
using NadekoBot.Core.Modules.Gambling.Services;
using Serilog;

namespace NadekoBot.Modules.Gambling.Services
{
    public class EchoService : INService
    {
        private readonly DbService _db;
        private readonly ICurrencyService _cs;
        private readonly NadekoBot _bot;
        private readonly DiscordSocketClient _client;
        private readonly IDataCache _cache;
        private readonly EchoConfigService _ecs;

        public ConcurrentDictionary<(ulong, ulong), RollDuelGame> Duels { get; } = new ConcurrentDictionary<(ulong, ulong), RollDuelGame>();
        public ConcurrentDictionary<ulong, Connect4Game> Connect4Games { get; } = new ConcurrentDictionary<ulong, Connect4Game>();

        private readonly Timer _decayTimer;

        public EchoService(DbService db, NadekoBot bot, ICurrencyService cs,
            DiscordSocketClient client, IDataCache cache, EchoConfigService ecs)
        {
            _db = db;
            _cs = cs;
            _bot = bot;
            _client = client;
            _cache = cache;
            _ecs = ecs;
            
            if (_bot.Client.ShardId == 0)
            {
                
            }

        }
    }
}
