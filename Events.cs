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
    public class Events
    {
        private bool BeingTested = false;
        private readonly DiscordSocketClient client;
        private readonly CommandService command;
        public Events(DiscordSocketClient client, CommandService command) 
        {
            this.client = client;
            this.command = command;
        }
        public Task InitializeAsync()
        {
            client.Ready += ReadyEvent;
            client.Log += LogEvent;
            client.ReactionAdded += OnReactionAdded;
            return Task.CompletedTask;
        }

        private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> cache, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (reaction.MessageId == Global.MessageIdToTrack)
            {
                if (reaction.Emote.Name == "◀")
                {
                    await channel.SendMessageAsync($"{reaction.User.Value.Username} says ◀");
                }
            }


        }

        private Task LogEvent(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source} {Message.Message}");
            return Task.CompletedTask;
        }

        private async Task ReadyEvent()
        {
            if (BeingTested)
            {
                await client.SetGameAsync("Pokemon bot is being tested", "", ActivityType.Watching);
                await client.SetStatusAsync(UserStatus.DoNotDisturb);
            }
            else
            {
                await client.SetGameAsync("Prefix `p.`", "", ActivityType.Watching);
            }
        }
    }
}
