using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Data.SQLite;
using PokeApiNet.Data;
using PokeApiNet.Models;
using PokemonBot.Resources.Database;
using PokemonBot.Core.Calculations;
using PokemonBot.Core.Data;
using Discord.Rest;
namespace PokemonBot.Core.Commands
{
    public class Information : ModuleBase<SocketCommandContext>
    {
        private int pages = 1;

        [Command("select"), Summary("Lets you select which pokemon you want to have set as your partner.")]
        public async Task SelectPokemon(int num)
        {
            SqliteDbContext database = new SqliteDbContext();
            string query = $"UPDATE player SET selected = @selected WHERE Id = '{Context.Message.Author.Id}'";
            SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
            database.OpenConnection();
            myCommand.Parameters.AddWithValue("@selected", num);
            myCommand.Prepare();
            myCommand.ExecuteNonQuery();
            database.CloseConnection();
            Console.WriteLine(database.myConnection.State);
            await Context.Channel.SendMessageAsync($"You have selected your **Level {Data.PokemonData.GetLevel(Context.Message.Author.Id, num)} {Data.PokemonData.GetPokemon(Context.Message.Author.Id, num)}**.");
        }
        [Command("pokemon"), Summary("All the pokemon you caught")]
        public async Task AllPokemon()
        {
            int num = 1;
            int page = 1;
            int pokeNum = Data.PokemonData.GetId(Context.Message.Author.Id);
            string pokemon = "";
            ulong id = Context.Message.Author.Id;
            EmbedBuilder embed = new EmbedBuilder();
            if (Data.PokemonData.GetId(Context.Message.Author.Id) > 15)
            {
                num = 16;
            }
            else
            {
                num = Data.PokemonData.GetId(Context.Message.Author.Id);
            }
            Console.WriteLine(num);
            int i = 1;
            while (i < num)
            {
                Console.WriteLine(i);
                pokemon += $"**{Data.PokemonData.GetPokemon(id, i)}** |Id: {i}| Level: {Data.PokemonData.GetLevel(id, i)}\n";
                ++i;  
            }
            
            embed.WithDescription(pokemon);
            RestUserMessage msg = await Context.Channel.SendMessageAsync("", embed: embed.Build());
            var backwards = new Emoji("◀");
            var forward = new Emoji("▶");
            await msg.AddReactionAsync(backwards);
            await msg.AddReactionAsync(forward);
            Global.MessageIdToTrack = msg.Id;
        }

        [Command("info"), Summary("Lets you info your pokemon.")]
        public async Task InfoPokemon(string id = "0")
        {
            int num;
            if (id == "latest" || id == "latests")
            {
                num = Data.PokemonData.GetId(Context.Message.Author.Id);
            }
            else
            {
                num = System.Convert.ToInt32(id);
                if (num == 0)
                {
                    num = Data.PokemonData.GetSelected(Context.Message.Author.Id);
                }
            }
            PokeApiClient pokeClient = new PokeApiClient();
            Pokemon poke = await pokeClient.GetResourceAsync<Pokemon>(Data.PokemonData.GetPokemon(Context.Message.Author.Id, num));
            EmbedBuilder embed = new EmbedBuilder();
            if (Data.PokemonData.IsShiny(Context.Message.Author.Id, num) == true)
            {
                embed.WithTitle($"Level {Data.PokemonData.GetLevel(Context.Message.Author.Id, num)} {Data.PokemonData.GetPokemon(Context.Message.Author.Id, num)}🌟");
            }
            else
            {
                embed.WithTitle($"Level {Data.PokemonData.GetLevel(Context.Message.Author.Id, num)} {Data.PokemonData.GetPokemon(Context.Message.Author.Id, num)}");
            }
            int hpIv = Data.PokemonData.GetIvs(Context.Message.Author.Id, num, 0);
            int atkIv = Data.PokemonData.GetIvs(Context.Message.Author.Id, num, 1); ;
            int defIv = Data.PokemonData.GetIvs(Context.Message.Author.Id, num, 2); ;
            int spatkIv = Data.PokemonData.GetIvs(Context.Message.Author.Id, num, 3);
            int spdefIv = Data.PokemonData.GetIvs(Context.Message.Author.Id, num, 4);
            int speedIv = Data.PokemonData.GetIvs(Context.Message.Author.Id, num, 5);
            string _nature = Data.PokemonData.GetNature(Context.Message.Author.Id, num);
            int level = Data.PokemonData.GetLevel(Context.Message.Author.Id, num);
            int baseHp = poke.Stats[5].BaseStat;
            int baseAtk = poke.Stats[4].BaseStat;
            int baseDef = poke.Stats[3].BaseStat;
            int baseSpAtk = poke.Stats[2].BaseStat;
            int baseSpDef = poke.Stats[1].BaseStat;
            int baseSpeed = poke.Stats[0].BaseStat;

            
            int hp = Calculations.PokemonCalcs.getHp(hpIv, baseHp, Data.PokemonData.GetLevel(Context.Message.Author.Id, num));
            double TotalIv = Calculations.PokemonCalcs.getTotalIv(hpIv, atkIv, defIv, spatkIv, spdefIv, speedIv);
            if (poke.Types.Count == 2)
            {
                embed.WithDescription($"Type: {poke.Types[0].Type.Name} | {poke.Types[1].Type.Name}\n" +
                    $"**Nature:** {_nature}\n" +
                    $"**HP:** {hp} - IV: {hpIv}/31\n" +
                    $"**Attack:** {PokemonCalcs.GetAtk(_nature, atkIv, baseAtk, level)} - IV: {atkIv}/31\n" +
                    $"**Defense:** {PokemonCalcs.GetDef(_nature, defIv, baseDef, level)} - IV: {defIv}/31\n" +
                    $"**Sp.Atk:** {PokemonCalcs.GetSpAtk(_nature, spatkIv, baseSpAtk, level)} - IV: {spatkIv}/31\n" +
                    $"**Sp.Def:** {PokemonCalcs.GetSpDef(_nature, spdefIv, baseSpDef, level)} - IV: {spdefIv}/31\n" +
                    $"**Speed:** {PokemonCalcs.GetSpeed(_nature, speedIv, baseSpeed, level)} - IV: {speedIv}/31\n" +
                    $"**Total IV %:** {TotalIv}");
            }
            else
            {
                embed.WithDescription($"Type: {poke.Types[0].Type.Name}\n" +
                    $"**Nature:** {Data.PokemonData.GetNature(Context.Message.Author.Id, num)}\n" +
                    $"**HP:** {hp} - IV: {hpIv}/31\n" +
                    $"**Attack:** {PokemonCalcs.GetAtk(_nature, atkIv, baseAtk, level)} - IV: {atkIv}/31\n" +
                    $"**Defense:** {PokemonCalcs.GetDef(_nature, defIv, baseDef, level)} - IV: {defIv}/31\n" +
                    $"**Sp.Atk:** {PokemonCalcs.GetSpAtk(_nature, spatkIv, baseSpAtk, level)} - IV: {spatkIv}/31\n" +
                    $"**Sp.Def:** {PokemonCalcs.GetSpDef(_nature, spdefIv, baseSpDef, level)} - IV: {spdefIv}/31\n" +
                    $"**Speed:** {PokemonCalcs.GetSpeed(_nature, speedIv, baseSpeed, level)} - IV: {speedIv}/31\n" +
                    $"**Total IV %:** {TotalIv}");

            }

            if (Data.PokemonData.IsShiny(Context.Message.Author.Id, num) == true)
            {
                embed.WithImageUrl("https://play.pokemonshowdown.com/sprites/xyani-shiny/" + Data.PokemonData.GetPokemon(Context.Message.Author.Id, num) + ".gif");
            }
            else
            {
                embed.WithImageUrl("http://play.pokemonshowdown.com/sprites/xyani/" + Data.PokemonData.GetPokemon(Context.Message.Author.Id, num) + ".gif");
            }
            embed.WithFooter($"{num} of {Data.PokemonData.GetId(Context.Message.Author.Id)} Pokemon");

            await Context.Channel.SendMessageAsync("", embed: embed.Build());

        }
        [Command("dex"), Summary("Lets you look at pokemon you don't own or do own.")]
        public async Task PokeDex([Remainder] string name)
        {

            PokeApiClient pokeClient = new PokeApiClient();
                Pokemon poke = await pokeClient.GetResourceAsync<Pokemon>(name);
            

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"Data On {name}");
            if (poke.Types.Count == 2)
            {
                embed.WithDescription($"Type: {poke.Types[0].Type.Name} | {poke.Types[1].Type.Name}\n" +
                    $"**HP:** {poke.Stats[0].BaseStat}\n" +
                    $"**Attack:** {poke.Stats[1].BaseStat}\n" +
                    $"**Defense:** {poke.Stats[2].BaseStat}\n" +
                    $"**Sp.Atk:** {poke.Stats[3].BaseStat}\n" +
                    $"**Sp.Def:** {poke.Stats[4].BaseStat}\n" +
                    $"**Speed:** {poke.Stats[5].BaseStat}\n");
            }
            else
            {
                embed.WithDescription($"Type: {poke.Types[0].Type.Name}\n" +
                    $"**HP:** {poke.Stats[0].BaseStat}\n" +
                    $"**Attack:** {poke.Stats[1].BaseStat}\n" +
                    $"**Defense:** {poke.Stats[2].BaseStat}\n" +
                    $"**Sp.Atk:** {poke.Stats[3].BaseStat}\n" +
                    $"**Sp.Def:** {poke.Stats[4].BaseStat}\n" +
                    $"**Speed:** {poke.Stats[5].BaseStat}\n");

            }
            embed.WithImageUrl(poke.Sprites.FrontDefault);
            await Context.Channel.SendMessageAsync("", embed: embed.Build());
        }

       
    }
}

