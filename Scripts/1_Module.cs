using ItemAPI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Knives
{
    public class Module : ETGModule
    {
        public static readonly string MOD_NAME = "Knife_to_a_Gunfight";
        
        public static readonly string VERSION = "1.0.3";
        public static readonly string TEXT_COLOR = "#5deba4";

        public override void Start()
        {
            ItemBuilder.Init();

            //register all items and synergies. if text at the bottom doesnt fire something along the way crashed/produced and error
            //Not all scripts are loaded some are saved for a later date.
          

            // general passive
            Dizzyring.Register();
            Spring_roll.Register();
            Salmon_roll.Register();
            dragun_roll.Register();
            Long_roll_boots.Register();
            Rocket_boots.Register();
            Fly_Friend.Register();
            Space_hammer.Register();
            Sus_rounds.Register();
            nightmare_mode.Register();
            Fates_blessing.Register();
            daft_helm.Register();
            punk_helm.Register();
            book.Register();
            clean_soul.Register();
            stardust.Register();
            loan.Register();
            tabletech_dizzy.Register();
            pocketwatch.Register();
            SCP_323.Register();
            Cant_touch_this.Register();
            Super_fly.Register();
            Im_blue.Register();
            bad_attitude.Register();
            rubber_man.Register();
            Survivor.Register();
            speedster.Register();
            Danger_dance.Register();
            disco_inferno.Register();
            persuasive_bullets.Register();

            // general active
            Led_Maiden.Register();
            jojo_arrow.Register();
            nano_boost.Register();
            rad_board.Register();
            koolbucks.Register();
            cased_daruma.Register();
            sandvich.Register();
            dog.Register();
            power_bracer.Register();
            roundabout.Register();
            Eye_of_the_tiger.Register();
            Luft_balloons.Register();
            punisher.Register();
            vodoo_kit.Register();
            BloodyNapkin.Register();
            Pig_Whistle.Register();

            //Guns
            hail_2_u.Add();
            fourth_wall_breaker.Add();
            Za_hando.Add();
            violin.Add();
            Queen.Add();
            Lance.Add();
            MagicHat.Add();
            CopperChariot.Add();
            //KnightSpear.Add();
            //SpinHammer.Add();

            //unfinished or unfunctional

            //smooth_criminal.Register();
            //Old_Computer.Register();
            //bandaids.Register();
            //Jim.Register();
            //shaw.Register();
            //Empty_Collection.Register();
            //Rocker_Collection.Register();
            //menacing_aura.Register();
            //Knives.Register();
            //Dio.Register();
            //testing_gun.Add();
            //hot_coffee.Add();
            
            //trinket.Register();

            GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(new AdvancedSynergyEntry[] { new Customsynergiesknives.tomislav() }).ToArray();
            GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(new AdvancedSynergyEntry[] { new Customsynergiesknives.Daft_Punk() }).ToArray();
            GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(new AdvancedSynergyEntry[] { new Customsynergiesknives.Super_Duper_Fly() }).ToArray();
            GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(new AdvancedSynergyEntry[] { new Customsynergiesknives.split() }).ToArray();
            GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(new AdvancedSynergyEntry[] { new Customsynergiesknives.flurry_of_blows() }).ToArray();
            GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(new AdvancedSynergyEntry[] { new Customsynergiesknives.BEEES() }).ToArray();
            GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(new AdvancedSynergyEntry[] { new Customsynergiesknives.nano() }).ToArray();
            GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(new AdvancedSynergyEntry[] { new Customsynergiesknives.Big_problem() }).ToArray();
            GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(new AdvancedSynergyEntry[] { new Customsynergiesknives.lich() }).ToArray();
            

            Log($"Don't bring a {MOD_NAME} v{VERSION}. You'll lose!", TEXT_COLOR);
           
        }

        public static void Log(string text, string color= "#5deba4")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }

        public override void Exit() { }
        public override void Init() { }
    }
}
