using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonBot.Core.Calculations
{
    //damage = ((((2 * level)/ 5) + 2) * Power * (A/D))/50) + 2) * Modifier
    public static class BattleCalcs
    {
        public static double Damage(int level, int Power, double atk, double def, double Modifier)
        {
            double _level = System.Convert.ToDouble(level);
            double _power = System.Convert.ToDouble(Power);
            double damage = ((((((2 * _level) / 5) + 2) * _power * (atk / def)) / 50) + 2) * Modifier;
            return Math.Floor(Math.Round(damage));
        }
    }
}
