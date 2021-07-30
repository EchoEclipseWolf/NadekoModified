using System;
using Discord;
using NadekoBot.Core.Services;
using NadekoBot.Modules;
using System.Threading.Tasks;
using NadekoBot.Core.Modules.EchoAdditions.Services;
using NadekoBot.Core.Modules.Gambling.Services;

namespace NadekoBot.Core.Modules.Gambling.Common
{
    public abstract class EchoModule<TService> : NadekoModule<TService>
    {
        private readonly Lazy<EchoConfig> _lazyConfig;
        protected EchoConfig _config => _lazyConfig.Value;
        protected EchoModule(EchoConfigService echoService)
        {
            _lazyConfig = new Lazy<EchoConfig>(() => echoService.Data);
        }

        private async Task<bool> InternalCheckBet(long amount)
        {
            if (amount < 1)
            {
                return false;
            }
            if (amount < _config.MinHugs)
            {
                await ReplyErrorLocalizedAsync("min_hug_limit", 
                    Format.Bold(_config.MinHugs.ToString())).ConfigureAwait(false);
                return false;
            }
            return true;
        }

        protected Task<bool> CheckBetMandatory(long amount)
        {
            if (amount < 1)
            {
                return Task.FromResult(false);
            }
            return InternalCheckBet(amount);
        }

        protected Task<bool> CheckBetOptional(long amount)
        {
            if (amount == 0)
            {
                return Task.FromResult(true);
            }
            return InternalCheckBet(amount);
        }
    }

    public abstract class GamblingSubmodule<TService> : GamblingModule<TService>
    {
        protected GamblingSubmodule(GamblingConfigService gamblingConfService) : base(gamblingConfService)
        {
        }
    }
}
