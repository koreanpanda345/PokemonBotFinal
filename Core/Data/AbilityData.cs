using System;
using System.Collections.Generic;
using System.Text;
using PokeApiNet.Data;
using PokeApiNet.Models;
using System.Threading.Tasks;
using Discord;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
namespace PokemonBot.Core.Data
{
    public struct Ability
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Effects { get; set; }
        public string Conditions { get; set; }
        public double Modifier { get; set; }
        public bool ModifyPokemon { get; set; }
        public string ModifyBy { get; set; }

    }
    public class AbilityData
    {
        Ability placeholder;
        public Ability GetAbility(string name)
        {
            placeholder.Name = "empty";
            Adaptability adapt = new Adaptability();
            Aerilate aerilate = new Aerilate();
            Aftermath aftermath = new Aftermath();
            Air_Lock air_Lock = new Air_Lock();
            Analytic analytic = new Analytic();
            if (name == "adaptability")
            {
                adapt.Set();
                return adapt.Get();
            }
            else if(name == "aerilate")
            {
                aerilate.Set();
                return aerilate.Get();
            }
            else if(name == "aftermath")
            {
                aftermath.Set();
                return aftermath.Get();
            }
            else if(name == "air lock")
            {
                air_Lock.Set();
                return air_Lock.Get();
            }
            else if(name == "analytic")
            {
                analytic.Set();
                return analytic.Get();
            }
            else
                return placeholder; 
        }
    }

    public  class Adaptability
    {
        public Ability ability;
        public void Set()
        {
            ability.Name = "Adaptability";
            ability.Description = "Adaptability increases STAB from 1.5× to 2×.";
            ability.Effects = "Increases Stab to 2x.";
            ability.Conditions = "STAB";
            ability.Modifier = 2;
            ability.ModifyPokemon = false;
            ability.ModifyBy = "";
        }
        public Ability Get() {return ability;} 
    }
    public class Aerilate
    {
        public Ability ability;
        public void Set()
        {
            ability.Name = "Aerilate";
            ability.Description = "Normal-type moves become Flying-type moves. The power of those moves is boosted a little.";
            ability.Effects = "Changes Normal-type moves to Flying-type moves, and gains a 1.2x attack bonus.";
            ability.Conditions = "Normal-type move";
            ability.Modifier = 1.2;
            ability.ModifyPokemon = true;
            ability.ModifyBy = "Move Type";
        }
        public Ability Get() { return ability; }
    }
    public class Aftermath
    {
        public Ability ability;
        public void Set()
        {
            ability.Name = "Aftermath";
            ability.Description = "When a pokemon with this ability faints due to damage from a move that makes contact, the attacking pokemon takes damage equal to 1/4 of its own maximum hp.";
            ability.Effects = "If faints by direct damaging move, the attacking pokemon loses hp equal to 1/4 of their maximum hp.";
            ability.Conditions = "Faints by a direct damaging move.";
            ability.Modifier = 0;
            ability.ModifyPokemon = false;
            ability.ModifyBy = "";
        }
        public Ability Get() { return ability; }
    }
    public class Air_Lock
    {
        public Ability ability;
        public void Set()
        {
            ability.Name = "Air Lock";
            ability.Description = "All effects of weather are negated(through the weather itself does not disappear).";
            ability.Effects = "If Weather is active, this ability will negated all of its effects";
            ability.Conditions = "Weather must be active, on switch in";
            ability.Modifier = 0;
            ability.ModifyPokemon = false;
            ability.ModifyBy = "";
        }
        public Ability Get() { return ability; }
    }
    public class Analytic
    {
        public Ability ability;
        public void Set()
        {
            ability.Name = "Analytic";
            ability.Description = "increase the power of it's moves, if the opposing pokemon hasn't made a move of the current turn.";
            ability.Effects = "If the pokemon moves first in the turn, then the power of its move will be increased by 1.3x";
            ability.Conditions = "Weather must be active, on switch in";
            ability.Modifier = 1.3;
            ability.ModifyPokemon = false;
            ability.ModifyBy = "";
        }
        public Ability Get() { return ability; }
    }

}
