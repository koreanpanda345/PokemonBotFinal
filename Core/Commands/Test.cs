using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using PokemonBot.Core.Calculations;
using PokemonBot.Core.Data;

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
        [Command("exp")]
        [Summary("Testing the exp calculations")]
        public async Task TestExp(int level, [Remainder]string speed)
        {
           await Context.Channel.SendMessageAsync(Calculations.PokemonCalcs.getExpNeed(speed, level).ToString());
        }

        [Command("ability")]
        [Summary("Test getting the abilities")]
        public async Task GetAbility([Remainder]string name)
        {
            EmbedBuilder embed = new EmbedBuilder();
            AbilityData data = new AbilityData();
            Ability ability =data.GetAbility(name);
            embed.WithTitle(ability.Name);
            embed.WithDescription(ability.Description);
            embed.AddField(x =>
            {
                x.Name = "Effects:";
                x.Value = ability.Effects;
                x.IsInline = true;
            });
            embed.AddField(x =>
            {
                x.Name = "Conditions";
                x.Value = ability.Conditions;
                x.IsInline = true;

            });
            embed.AddField(x =>
            {
                x.Name = "Modifier";
                x.Value = ability.Modifier;
                x.IsInline = true;
            });
            embed.AddField(x =>
            {
                x.Name = "Modifys The Pokemon";
                x.Value = ability.ModifyPokemon;
                x.IsInline = false;
            });
            if (ability.ModifyPokemon)
            {
                embed.AddField(x =>
                {
                    x.Name = "Modifys it by";
                    x.Value = ability.ModifyBy;
                    x.IsInline = false;
                });
            }

            await Context.Channel.SendMessageAsync(embed: embed.Build());

        }
    }
}
