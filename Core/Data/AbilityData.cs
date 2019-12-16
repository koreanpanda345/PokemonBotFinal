using System;
using System.Collections.Generic;
using System.Text;
using PokeApiNet;
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
        public List<Ability> abilities = new List<Ability>();
        public async Task CreateAbilityTable()
        {
            //Creates and grabs data from pokeapi.co to be put in the abilities list.
            PokeApiClient client = new PokeApiClient();
            //Letter A
            Ability adaptability = await client.GetResourceAsync<Ability>("adaptability"); //91
            Ability aerilate = await client.GetResourceAsync<Ability>("aerilate");//185
            Ability aftermath = await client.GetResourceAsync<Ability>("aftermath");//106
            Ability airlock = await client.GetResourceAsync<Ability>("air-lock");//76
            Ability analytic = await client.GetResourceAsync<Ability>("analytic");//148
            Ability angerpoint = await client.GetResourceAsync<Ability>("anger-point");//83
            Ability anticipation = await client.GetResourceAsync<Ability>("anticipation");//107
            Ability arenatrap = await client.GetResourceAsync<Ability>("arena-trap");//71
            Ability aromaveil = await client.GetResourceAsync<Ability>("aroma-veil");//165
            Ability aurabreak = await client.GetResourceAsync<Ability>("aura-break");//188
            Console.WriteLine("All Abilities with Letter A has been loaded");
            //Letter B
            Ability baddreams = await client.GetResourceAsync<Ability>("bad-dreams"); //123
            //Ability ballfetch = await client.GetResourceAsync<Ability>("ball-fetch");//237
            Ability battery = await client.GetResourceAsync<Ability>("battery");//217
            Ability battlearmor = await client.GetResourceAsync<Ability>("battle-armor");//4
            Ability battlebond = await client.GetResourceAsync<Ability>("battle-bond");//210
            Ability beastboost = await client.GetResourceAsync<Ability>("beast-boost");//224
            Ability berserk = await client.GetResourceAsync<Ability>("berserk");//201
            Ability bigpecks = await client.GetResourceAsync<Ability>("big-pecks");//145
            Ability blaze = await client.GetResourceAsync<Ability>("blaze");//66
            Ability bulletproof = await client.GetResourceAsync<Ability>("bulletproof");//171
            Console.WriteLine("All Abilities with Letter B has been loaded");
            //Letter C
            Ability cheekpouch = await client.GetResourceAsync<Ability>("cheek-pouch");//167
            Ability chlorophyll = await client.GetResourceAsync<Ability>("chlorophyll");//34
            Ability clearbody = await client.GetResourceAsync<Ability>("clear-body");//29
            Ability cloudnine = await client.GetResourceAsync<Ability>("cloud-nine");//13
            Ability colorchange = await client.GetResourceAsync<Ability>("color-change");//16
            Ability comatose = await client.GetResourceAsync<Ability>("comatose");//213
            Ability competitive = await client.GetResourceAsync<Ability>("competitive");//172
            Ability compoundeyes = await client.GetResourceAsync<Ability>("compound-eyes");//14
            Ability contrary = await client.GetResourceAsync<Ability>("contrary");//126
            Ability corrosion = await client.GetResourceAsync<Ability>("corrosion");//212
            //Ability cottondown = await client.GetResourceAsync<Ability>("cotton-down");//238
            Ability cursedbody = await client.GetResourceAsync<Ability>("cursed-body");//130
            Ability cutecharm = await client.GetResourceAsync<Ability>("cute-charm");//56
            Console.WriteLine("All Abilities with Letter C has been loaded");
            //Letter D
            Ability damp = await client.GetResourceAsync<Ability>("damp");//6
            Ability dancer = await client.GetResourceAsync<Ability>("dancer");//216
            Ability darkaura = await client.GetResourceAsync<Ability>("dark-aura");//186
            //Ability dauntlessshield = await client.GetResourceAsync<Ability>("dauntless-shield");//235
            Ability dazzling = await client.GetResourceAsync<Ability>("dazzling");//219
            Ability defeatist = await client.GetResourceAsync<Ability>("defeatist");//129
            Ability defiant = await client.GetResourceAsync<Ability>("defiant");//128
            Ability deltastream = await client.GetResourceAsync<Ability>("delta-stream");//191
            Ability desolateland = await client.GetResourceAsync<Ability>("desolate-land");//190
            Ability disguise = await client.GetResourceAsync<Ability>("disguise");//209
            Ability download = await client.GetResourceAsync<Ability>("download");//88
            Ability drizzle = await client.GetResourceAsync<Ability>("drizzle");//2
            Ability drought = await client.GetResourceAsync<Ability>("drought");//70
            Ability dryskin = await client.GetResourceAsync<Ability>("dry-skin");//87
            Console.WriteLine("All Abilities with letter C has been Loaded");
            //Insert into the list name abilities
            //moves will be placed in order base on their id.
            //1
            abilities.Add(drizzle);//2
            //3
            abilities.Add(battlearmor);//4
            abilities.Add(damp);//6
            abilities.Add(cloudnine);//13
            abilities.Add(compoundeyes);//14
            abilities.Add(colorchange);//16
            abilities.Add(clearbody);//29
            abilities.Add(chlorophyll);//34
            abilities.Add(cutecharm);//56
            abilities.Add(blaze);//66
            abilities.Add(drought);//70
            abilities.Add(arenatrap);//71
            abilities.Add(airlock);//76
            abilities.Add(angerpoint);//83
            abilities.Add(dryskin);//87
            abilities.Add(download);//88
            abilities.Add(adaptability);//91
            abilities.Add(aftermath);//106
            abilities.Add(anticipation);//107
            abilities.Add(baddreams);//123
            abilities.Add(contrary);//126
            abilities.Add(defiant);//128
            abilities.Add(defeatist);//129
            abilities.Add(cursedbody);//130
            abilities.Add(bigpecks); //145
            abilities.Add(analytic);//148
            abilities.Add(aromaveil); //165
            abilities.Add(cheekpouch);//167
            abilities.Add(bulletproof);//171
            abilities.Add(competitive);//172
            abilities.Add(aerilate);//185
            abilities.Add(darkaura);//186
            abilities.Add(aurabreak); //188
            abilities.Add(desolateland);//190
            abilities.Add(deltastream);//191
            abilities.Add(berserk);//201
            abilities.Add(disguise);//209
            abilities.Add(battlebond);//210
            abilities.Add(corrosion);//212
            abilities.Add(comatose);//213
            abilities.Add(dancer);//216
            abilities.Add(battery);//217
            abilities.Add(dazzling);//219
            abilities.Add(beastboost);//224
            //abilities.Add(dauntlessshield);//235
            //abilities.Add(ballfetch);//237
            //abilities.Add(cottondown);//238
            
            

        }

        public List<Ability> GetAbilitieTable()
        {
            return abilities;
        }
    }

    public class OnModifyMove
    {
        public double Stab(double num)
        {

            return num;
        }
    }

}
