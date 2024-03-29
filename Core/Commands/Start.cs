﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using PokeApiNet;
using PokeApiNet.Models;
using PokemonBot.Core.Data;
using System.IO;
using Newtonsoft.Json;
using PokemonBot.Core.Calculations;

namespace PokemonBot.Core.Commands
{
    public class Start : ModuleBase<SocketCommandContext>
    {
        PokeApiClient pokeClient = new PokeApiClient();

        [Command("start"), Summary("displays a list of starters you can choose from")]
        public async Task _Start()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Pokemon Bot");
            embed.WithDescription("Welcome type in \"p.start < starter name\"");
            embed.AddField("Gen 1", "charmander, bulbasaur, squirtle");
            embed.AddField("Gen 2", "chikorita, totodile, cyndaquil");
            embed.AddField("Gen 3", "mudkip, treecko, torchic");
            embed.AddField("Gen 4", "turtwig, chimchar, piplup");
            embed.AddField("Gen 5", "oshawott, snivy, tepig");
            embed.AddField("Gen 6", "froakie, fennekin, chespin");
            embed.AddField("Gen 7", "rowlet, litten, popplio");

            await Context.Channel.SendMessageAsync("", embed: embed.Build());
        }
        [Command("pick"), Summary("Lets you pick your starter")]
        public async Task _Pick(string PokemonName)
        {
            string poke = PokemonName;
            if (Data.PokemonData.HasStarter(Context.Message.Author.Id))
            {
                await Context.Channel.SendMessageAsync("You already have a starter");
                return;
            }
            if (!(poke == "charmander" || poke == "bulbasaur" || poke == "squirtle"
            || poke == "chikorita" || poke == "totodile" || poke == "cyndaquil"
            || poke == "mudkip" || poke == "treecko" || poke == "torchic"
            || poke == "turtwig" || poke == "chimchar" || poke == "piplup"
            || poke == "oshawott" || poke == "snivy" || poke == "tepig"
            || poke == "froakie" || poke == "fennekin" || poke == "chespin"
            || poke == "rowlet" || poke == "litten" || poke == "poplio"))
            {
                await Context.Channel.SendMessageAsync("That is not a valid starter pokemon");
            }
            string[] NatureChance = new Natures().NatureChance;
            int spawnHp = RandomNumber(1, 31);
            int spawnAtk = RandomNumber(1, 31);
            int spawnDef = RandomNumber(1, 31);
            int spawnSpAtk = RandomNumber(1, 31);
            int spawnSpDef = RandomNumber(1, 31);
            int spawnSpe = RandomNumber(1, 31);
            double ivTotal = (((spawnHp + spawnAtk + spawnDef + spawnSpAtk + spawnSpDef + spawnSpe) / 186) * 100);
            double totalIv = Math.Round(ivTotal);
            int spawnNature = RandomNumber(1, 24);
            string _nature = NatureChance[spawnNature];
            Pokemon _poke = await pokeClient.GetResourceAsync<Pokemon>(poke);
            Console.WriteLine(_poke.Stats);
            int hp = Calculations.PokemonCalcs.getHp(spawnHp, _poke.Stats[5].BaseStat, 5);
            int atk = (((2 * _poke.Stats[4].BaseStat + spawnAtk) * 5) / 100 + 10);
            int def = (((2 * _poke.Stats[3].BaseStat + spawnDef) * 5) / 100 + 10);
            int spAtk = (((2 * _poke.Stats[2].BaseStat + spawnSpAtk) * 5) / 100 + 10);
            int spDef = (((2 * _poke.Stats[1].BaseStat + spawnSpDef) * 5) / 100 + 10);
            int spe = (((2 * _poke.Stats[0].BaseStat + spawnSpe) * 5) / 100 + 10);
            var embed = new EmbedBuilder();
            embed.WithTitle($"Congratalution {Context.User.Username}, you recieved a **Level 5 **");
            embed.WithDescription($"Level 5");
            embed.WithImageUrl($"{_poke.Sprites.FrontDefault}");
            embed.AddField("Nature", $"{_nature}");
            embed.AddField("Stats", $"HP: {hp} | {spawnHp}/31\nAtk: {atk} | {spawnAtk}/31\nDef: {def} | {spawnDef}/31\nSp.Atk: {spAtk} | {spawnSpAtk}/31\nSp.Def: {spDef} | {spawnSpDef}/31\nSpeed: {spe} | {spawnSpe}/31\nTotal IV: {totalIv}");
            int[] Iv = { hp, atk, def, spAtk, spDef, spe };
            await Context.Channel.SendMessageAsync("", embed: embed.Build());

            await Data.PokemonData.CreateAccount(Context.Message.Author.Id, poke, 5, Iv, _nature);

        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
