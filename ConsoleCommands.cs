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
    public class ConsoleCommands
    {
        private readonly DiscordSocketClient client;
        public ConsoleCommands(DiscordSocketClient client)
        {
            this.client = client;
        }
        public void HelpMessage()
        {
            Console.WriteLine(
                "help\n" +
                "status\n" +
                "message\n" +
                "spawn\n" +
                "shinyspawn\n" +
                "update\n" +
                "sql\n" +
                "close\n" +
                "connection\n" +
                "client");
        }
        public async void StatusChange()
        {
            var msg = string.Empty;
            while (msg.Trim() == string.Empty)
            {
                Console.WriteLine("Status: ");
                msg = Console.ReadLine();
            }
            if (msg == "online") await client.SetStatusAsync(UserStatus.Online);
            if (msg == "idle") await client.SetStatusAsync(UserStatus.Idle);
            if (msg == "dnd") await client.SetStatusAsync(UserStatus.DoNotDisturb);
            if (msg == "inv") await client.SetStatusAsync(UserStatus.Invisible);
        }
        public void ClientStatus()
        {

            Console.WriteLine($"Bot is {client.ConnectionState}\nUsername: {client.CurrentUser}\nBot Status: {client.Status}\nActivity set to: {client.Activity.Name}\nGuilds Count: {client.Guilds.Count()}\nGuild Name");
            var socketGuilds = client.Guilds.ToList();
            var maxIndex = client.Guilds.Count();
            for (var i = 0; i < maxIndex; ++i)
            {
                Console.WriteLine($"{i + 1} - {socketGuilds[i]}");
            }
        }
        public void Connection()
        {
            SqliteDbContext database = new SqliteDbContext();
            Console.WriteLine($"Database Connection is {database.myConnection.State}");
            Console.WriteLine($"Database is {database.myConnection.BusyTimeout}");
        }
        public void CloseDatabase()
        {
            SqliteDbContext database = new SqliteDbContext();
            if (database.myConnection.State == System.Data.ConnectionState.Open)
            {
                database.CloseConnection();
                Console.WriteLine("Closed database");
            }
            else if (database.myConnection.State == System.Data.ConnectionState.Closed)
            {
                Console.WriteLine("database is already closed");
            }
        }
        public void SQLConsole()
        {
            var command = string.Empty;
            while (command.Trim() == string.Empty)
            {
                Console.WriteLine("SQL COMMAND");
                command = Console.ReadLine();
                SqliteDbContext database = new SqliteDbContext();
                string query = $"{command}";
                SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
                database.OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        if (command.Contains("*"))
                        {
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine(result.ToString());
                        }

                    }
                }
                result.Close();
                database.CloseConnection();
            }
        }
        public async void ConsoleUpdate()
        {
            var msg = string.Empty;
            while (msg.Trim() == string.Empty)
            {
                Console.WriteLine("Your Message: ");
                msg = Console.ReadLine();
            }
            await client.SetGameAsync($"{msg}", "", ActivityType.Watching);
            await client.SetStatusAsync(UserStatus.DoNotDisturb);
        }
        public async void ConsoleSendMessage()
        {
            Console.WriteLine("Select the guild:");
            var guild = GetSelectedGuild(client.Guilds);
            var textChannel = GetSelectedTextChannel(guild.TextChannels);
            var msg = string.Empty;
            while (msg.Trim() == string.Empty)
            {
                Console.WriteLine("Your Message: ");
                msg = Console.ReadLine();
            }
            await textChannel.SendMessageAsync(msg);
        }
        public void SpawnPokemon()
        {
            Console.WriteLine("Select the guild: ");
            var guild = GetSelectedGuild(client.Guilds);
            var textChannel = GetSelectedTextChannel(guild.TextChannels);
            var msg = string.Empty;
            while (msg.Trim() == string.Empty)
            {
                Console.WriteLine("Pokemon: ");
                msg = Console.ReadLine();
            }
            Spawn.SpawnPokemon(guild, textChannel, msg);
        }
        public void ShinySpawnPokemon()
        {
            Console.WriteLine("Select the guild: ");
            var guild = GetSelectedGuild(client.Guilds);
            var textChannel = GetSelectedTextChannel(guild.TextChannels);
            var msg = string.Empty;
            while (msg.Trim() == string.Empty)
            {
                Console.WriteLine("Pokemon: ");
                msg = Console.ReadLine();
            }
            Spawn.SpawnPokemon(guild, textChannel, msg);
        }
        private SocketTextChannel GetSelectedTextChannel(IEnumerable<SocketTextChannel> channels)
        {
            var textChannels = channels.ToList();
            var maxIndex = channels.Count() - 1;
            for (var i = 0; i <= maxIndex; ++i)
            {
                Console.WriteLine($"{i} - {textChannels[i].Name}");
            }
            var selectedIndex = -1;
            while (selectedIndex < 0 || selectedIndex > maxIndex)
            {
                var success = int.TryParse(Console.ReadLine().Trim(), out selectedIndex);
                if (!success)
                {
                    Console.WriteLine("That was an Invalid Index, try again.");
                    selectedIndex = -1;
                }
                if (selectedIndex < 0 || selectedIndex > maxIndex) Console.WriteLine("This is a valid index");
            }

            return textChannels[selectedIndex];
        }

        private SocketGuild GetSelectedGuild(IEnumerable<SocketGuild> guilds)
        {
            var socketGuilds = guilds.ToList();
            var maxIndex = guilds.Count() - 1;
            for (var i = 0; i <= maxIndex; ++i)
            {
                Console.WriteLine($"{i} - {socketGuilds[i].Name}");
            }
            var selectedIndex = -1;
            while (selectedIndex < 0 || selectedIndex > maxIndex)
            {
                var success = int.TryParse(Console.ReadLine().Trim(), out selectedIndex);
                if (!success)
                {
                    Console.WriteLine("That was an Invalid Index, try again.");
                    selectedIndex = -1;
                }

                if (selectedIndex < 0 || selectedIndex > maxIndex) Console.WriteLine("This is a valid index");
            }

            return socketGuilds[selectedIndex];
        }
    }
}
