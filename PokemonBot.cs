using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;


using Discord.Commands;
using System.Reflection;
using PokemonBot.Core.Spawning;
using PokemonBot.Core.Xp;
using System.Data.SQLite;
using PokemonBot.Resources.Database;
using PokemonBot.Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace PokemonBot
{

    public class PokemonBot
    {
        private DiscordSocketClient client;
        private CommandService command;
        public PokemonBot()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });

            command = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Verbose
            });
        }
        public async Task MainAsync()
        {
            var cmdHandler = new CommandHandler(client, command);
            var events = new Events(client, command);

            await cmdHandler.InitializeAsync();
            await events.InitializeAsync();

            if (Config.bot.token == "" || Config.bot.token == null) return;

            await client.LoginAsync(TokenType.Bot, Config.bot.token);
            await client.StartAsync();
            await ConsoleInput();
            await Task.Delay(-1);
        }

        public async Task ConsoleInput()
        {
            ConsoleCommands console = new ConsoleCommands(client);
            string input = Console.ReadLine();
            while (input.Trim().ToLower() != "block")
            {
                switch (input.Trim().ToLower())
                {
                    case "help":
                        console.HelpMessage();
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;
                    case "status":
                        console.StatusChange();
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;
                    case "message":
                        console.ConsoleSendMessage();
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;
                    case "spawn":
                        console.SpawnPokemon();
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;
                    case "shinyspawn":
                        console.ShinySpawnPokemon();
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;
                    case "update":
                        console.ConsoleUpdate();
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;
                    case "sql":
                        console.SQLConsole();
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;
                    case "close":
                        console.CloseDatabase();
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;
                    case "connection":
                        console.Connection();
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;
                    case "client":
                        console.ClientStatus();
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;
                    default:
                        input = String.Empty;
                        input = Console.ReadLine();
                        break;


                }
            }
        }
    }
}
