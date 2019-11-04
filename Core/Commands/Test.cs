using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using PokemonBot.Core.Calculations;

namespace PokemonBot.Core.Commands
{
    public class Test : ModuleBase<SocketCommandContext>
    {
        [Command("damage")]
        [Summary("Testing the damage calculation")]
        public async Task TestDamage(int level, int power, double atk, double def, double modifiers)
        {

            double result = BattleCalcs.Damage(level, power, atk, def, modifiers);
            await Context.Channel.SendMessageAsync(result.ToString());
        }
    }
}
