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
    public class AbilityData
    {
        Ability placeholder;
        public Ability GetAbility(string name)
        {
            placeholder.Name = "empty";
            Adaptability adapt = new Adaptability();
            if (name == "adaptability")
                return adapt.Get();
            else
            return placeholder; 
        }
    }
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
}
