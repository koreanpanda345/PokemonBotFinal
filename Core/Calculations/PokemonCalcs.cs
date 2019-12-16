using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PokemonBot.Core.Data;
using PokeApiNet;
using PokeApiNet.Models;


namespace PokemonBot.Core.Calculations
{
    public static class PokemonCalcs
    {
        public static int getHp(int hp,int _base, int level)
        {
            return ((2 * _base + hp + 100) * level) / 100 + 10;
        }
        public static double GetAtk(string nature, int _atk, int _base, int level)
        {
            double atk = (((2 * _base + _atk) * level) / 100 + 5);
            //Attacks
            if (nature == "Lonely")
            {
                atk *= 1.10;
            }
            else if (nature == "Brave")
            {
                atk *= 1.10;
            }
            else if (nature == "Adamant")
            {
                atk *= 1.10;
            }
            else if (nature == "Naughty")
            {
                atk *= 1.10;
            }
            else if (nature == "Bold")
            {
                atk /= 1.10;
            }
            else if (nature == "Timid")
            {
                atk /= 1.10;
            }
            else if (nature == "Modest")
            {
                atk /= 1.10;
            }
            else if (nature == "Calm")
            {
                atk /= 1.10;
            }

            return Math.Floor(Math.Round(atk));
        }
        public static double GetDef(string nature, int _def, int _base, int level)
        {

            double def = (((2 * _base + _def) * level) / 100 + 5);
            if (nature == "Lonely")
            {
                def /= 1.10;
            }
            else if (nature == "Bold")
            {
                def *= 1.10;
            }
            else if (nature == "Relaxed")
            {
                def *= 1.10;
            }
            else if (nature == "Impish")
            {
                def *= 1.10;
            }
            else if (nature == "Lax")
            {
                def *= 1.10;
            }
            else if (nature == "Hasty")
            {
                def /= 1.10;
            }
            else if (nature == "Mild")
            {
                def /= 1.10;
            }
            else if (nature == "Gentle")
            {
                def /= 1.10;
            }
            return Math.Floor(Math.Round(def));
        }
        public static double GetSpAtk(string nature, int _spatk, int _base, int level)
        {
            double spatk = (((2 * _base + _spatk) * level) / 100 + 5);

            if (nature == "Adamant")
            {
                spatk /= 1.10;
            }
            else if (nature == "Impish")
            {
                spatk /= 1.10;
            }
            else if (nature == "Jolly")
            {
                spatk /= 1.10;
            }
            else if (nature == "Modest")
            {
                spatk *= 1.10;
            }
            else if (nature == "Mild")
            {
                spatk *= 1.10;
            }
            else if (nature == "Quiet")
            {
                spatk *= 1.10;
            }
            else if (nature == "Rash")
            {
                spatk *= 1.10;
            }
            else if (nature == "Careful")
            {
                spatk /= 1.10;
            }


            return Math.Floor(Math.Round(spatk));
        }
        public static double GetSpDef(string nature, int _spdef, int _base, int level)
        {
            double spdef = (((2 * _base + _spdef) * level) / 100 + 5);
            if (nature == "Naughty")
            {
                spdef /= 1.10;
            }
            else if (nature == "Lax")
            {
                spdef /= 1.10;
            }
            else if (nature == "Naive")
            {
                spdef /= 1.10;
            }
            else if (nature == "Rash")
            {
                spdef /= 1.10;
            }
            else if (nature == "Calm")
            {
                spdef *= 1.10;
            }
            else if (nature == "Gentle")
            {
                spdef *= 1.10;
            }
            else if (nature == "Sassy")
            {
                spdef *= 1.10;
            }
            else if (nature == "Careful")
            {
                spdef *= 1.10;
            }
            return Math.Floor(Math.Round(spdef));
        }
        public static double GetSpeed(string nature, int _speed, int _base, int level)
        {
            double speed = (((2 * _base + _speed) * level) / 100 + 5);
            if (nature == "Brave")
            {
                speed /= 1.10;
            }
            else if (nature == "Relaxed")
            {
                speed /= 1.10;
            }
            else if (nature == "Timid")
            {
                speed *= 1.10;
            }
            else if (nature == "Hasty")
            {
                speed *= 1.10;
            }
            else if (nature == "Jolly")
            {
                speed *= 1.10;
            }
            else if (nature == "Naive")
            {
                speed *= 1.10;
            }

            return Math.Floor(Math.Round(speed));
        }
        public static double getTotalIv(int hp, int atk, int def, int spatk, int spdef, int speed)
        {
            int average = (hp + atk + def + spatk + spdef + speed);
            double averageDouble = System.Convert.ToDouble(average);
            double total = Math.Floor(Math.Round((averageDouble / 186) * 100));
            
            return total;
        }
        //Exp
        public static double getExpNeed(string speed, double level)
        {
            double exp = 0;

            if (speed == "fast")
                exp = (Math.Floor( 4* Math.Pow(level, 3)) / 5);
            else if (speed == "medium fast") 
                exp = (Math.Floor(Math.Pow(level, 3)));
            else if(speed == "medium fast")
                exp = Math.Floor((6 / 5) *  Math.Pow( level, 3) - (15 * Math.Pow( level, 2)) + (100 * level) - 140);
            else if(speed == "slow")
                exp = Math.Floor(5 * Math.Pow( level, 3) / 4);
            return exp;
        }
    }
}
