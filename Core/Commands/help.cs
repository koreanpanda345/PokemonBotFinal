﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.Commands;
using System.Linq;

namespace PokemonBot.Core.Commands
{

    public class Help : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;
        public Help(CommandService service)
        {
            _service = service;
        }
        [Command("help")]
        [Alias("command", "commands")]
        [Summary("Displays all of the bots commands")]
        public async Task HelpCommand([Remainder]string command = "")
        {
            if(command == "")
            {
                string prefix = "p.";
                var builder = new EmbedBuilder()
                {
                    Color = new Color(114, 137, 218),
                    Description = "These are the commands you can use"
                };

                foreach (var module in _service.Modules)
                {
                    string description = null;
                    foreach (var cmd in module.Commands)
                    {
                        var result = await cmd.CheckPreconditionsAsync(Context);
                        if (result.IsSuccess)
                            description += $"{prefix}{cmd.Aliases.First()}\n";
                    }

                    if (!string.IsNullOrWhiteSpace(description))
                    {
                        string name;
                        if(module.Name == "Help")
                        {
                            name = $" ▶ Need some help: ";
                        }
                        else if(module.Name == "Information")
                        {
                            name = $"▶ What pokemon is that?:";
                        }
                        else if(module.Name == "Start")
                        {
                            name = $"▶ Get Started with your adventure :";
                        }
                        else if(module.Name == "Battling")
                        {
                            name = $"▶ Get Ready to DUUUUEEELLL: ";
                        }
                        else if(module.Name == "Catch")
                        {
                            name = $"▶ Catching Pokemon?:";
                        }
                        else
                        {
                            name = module.Name;
                        }
                        builder.AddField(x =>
                        {
                            x.Name = name;
                            x.Value = description;
                            x.IsInline = false;
                        });
                    }
                }

                await ReplyAsync("", false, builder.Build());
            }
            else
            {
                var result = _service.Search(Context, command);

                if (!result.IsSuccess)
                {
                    await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
                    return;
                }

                string prefix = "p.";
                var builder = new EmbedBuilder()
                {
                    Color = new Color(114, 137, 218),
                    Description = $"Here are some commands like **{command}**"
                };

                foreach (var match in result.Commands)
                {
                    var cmd = match.Command;

                    builder.AddField(x =>
                    {
                        x.Name = string.Join(", ", cmd.Aliases);
                        x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}\n" +
                                  $"Summary: {cmd.Summary}";
                        x.IsInline = false;
                    });
                }

                await ReplyAsync("", false, builder.Build());
            }
        }
    }
}
