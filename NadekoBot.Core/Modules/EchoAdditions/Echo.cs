using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NadekoBot.Common;
using NadekoBot.Common.Attributes;
using NadekoBot.Core.Common;
using NadekoBot.Core.Modules.Gambling.Common;
using NadekoBot.Core.Services;
using NadekoBot.Core.Services.Database.Models;
using NadekoBot.Extensions;
using NadekoBot.Modules.Gambling.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using NadekoBot.Core.Modules.EchoAdditions.Services;
using NadekoBot.Core.Modules.Gambling.Services;
using NadekoBot.Core.Services.Database;

namespace NadekoBot.Modules.Gambling
{
    public partial class Echo : EchoModule<EchoService>
    {
        private readonly DbService _db;
        private readonly ICurrencyService _cs;
        private readonly IDataCache _cache;
        private readonly DiscordSocketClient _client;
        private readonly NumberFormatInfo _enUsCulture;
        private readonly DownloadTracker _tracker;
        private readonly EchoConfigService _configService;

        private List<string> _suggestions = new List<string>();

        public Echo(DbService db, ICurrencyService currency,
            IDataCache cache, DiscordSocketClient client,
            DownloadTracker tracker, EchoConfigService configService) : base(configService)
        {
            _db = db;
            _cs = currency;
            _cache = cache;
            _client = client;
            _enUsCulture = new CultureInfo("en-US", false).NumberFormat;
            _enUsCulture.NumberDecimalDigits = 0;
            _enUsCulture.NumberGroupSeparator = " ";
            _tracker = tracker;
            _configService = configService;

            if (File.Exists("Suggestions.txt")) {
                _suggestions = File.ReadAllLines("Suggestions.txt").ToList();
            }
        }

        [NadekoCommand, Usage, Description, Aliases]
        [RequireContext(ContextType.Guild)]
        public async Task Hug([Leftover] IUser user = null)
        {
            if (user == null) {
                return;
            }

            if (user == ctx.User) {
                return;
            }

            user = user ?? ctx.User;

            var sendUsersName = ctx.User.Username;
            if (ctx.User is IGuildUser guildUser) {
                if (guildUser.Nickname != null) {
                    sendUsersName = guildUser.Nickname;
                }
                
            }

            await ctx.Channel.SendConfirmAsync($"{sendUsersName} hugs {user.Mention}").ConfigureAwait(false);
        }

        [NadekoCommand, Usage, Description, Aliases]
        [RequireContext(ContextType.Guild)]
        public async Task Slap([Leftover] IUser user = null)
        {
            if (user == null) {
                return;
            }

            if (user == ctx.User) {
                return;
            }

            if (user.Username.Equals("EchoEclipse")) {
                await ctx.Channel.SendConfirmAsync($"Thou cannot slap echo").ConfigureAwait(false);
                return;
            }

            if (user is IGuildUser localGuildUser) {
                var roles = localGuildUser.GetRoles().ToList();
                foreach (var role in roles) {
                    if (role.Name.Equals("Pet", StringComparison.OrdinalIgnoreCase)) {
                        await ctx.Channel.SendConfirmAsync($"Thou cannot slap pets").ConfigureAwait(false);
                        return;
                    }
                }
            }

            user = user ?? ctx.User;

            var sendUsersName = ctx.User.Username;
            if (ctx.User is IGuildUser guildUser) {
                if (guildUser.Nickname != null) {
                    sendUsersName = guildUser.Nickname;
                }
                
            }

            await ctx.Channel.SendConfirmAsync($"{sendUsersName} slaps {user.Mention}").ConfigureAwait(false);
        }

        [NadekoCommand, Usage, Description, Aliases]
        [RequireContext(ContextType.Guild)]
        public async Task Owo([Leftover] IUser user = null)
        {
            await ctx.Channel.SendConfirmAsync($"owo").ConfigureAwait(false);
        }

        [NadekoCommand, Usage, Description, Aliases]
        [RequireContext(ContextType.Guild)]
        public async Task BotSuggestion([Leftover] string suggestion = null)
        {
            if (suggestion == null || string.IsNullOrEmpty(suggestion)) {
                return;
            }

            if (_suggestions.Contains(suggestion)) {
                await ctx.Channel.SendConfirmAsync($"Already Contains : {suggestion}").ConfigureAwait(false);
                return;
            }

            await ctx.Channel.SendConfirmAsync($"Added : {suggestion}").ConfigureAwait(false);

            _suggestions.Add(suggestion);
            File.WriteAllLines("Suggestions.txt", _suggestions);

        }

        [NadekoCommand, Usage, Description, Aliases]
        [RequireContext(ContextType.Guild)]
        public async Task ListBotSuggestions() {
            var text = String.Join($"{Environment.NewLine}", _suggestions);
            await ctx.Channel.SendConfirmAsync($"Bot Suggestions {Environment.NewLine} ---------- {Environment.NewLine} {text}").ConfigureAwait(false);

        }
    }
}
