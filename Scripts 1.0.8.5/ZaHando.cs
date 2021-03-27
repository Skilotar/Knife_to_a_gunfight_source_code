using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;
using Dungeonator;
using MultiplayerBasicExample;
using System.Collections.Generic;

namespace Knives
{

    public class Za_hando : GunBehaviour
    {
        
        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Lich's Trigger Hand", "hand");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:lich's_trigger_hand", "ski:lich's_trigger_hand");
            gun.gameObject.AddComponent<Za_hando>();

            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("I'll scrape you away!");
            gun.SetLongDescription("The lich was infamous for scooping up the gundead to use as ammunitions. Though the trigger finger has been torn off, much of its original power still remains.\n\n" +
                "_____________________________________________________\n\n" +
                "This weapon consumes all entities in a ~3 meter range when reloading, EXCEPT for Major Bosses, Mimics, NPcs and Companions." +
                "\n\n\n - Knife_to_a_Gunfight");
            // This is required, unless you want to use the sprites of the base gun.
            // That, by default, is the pea shooter.
            // SetupSprite sets up the default gun sprite for the ammonomicon and the "gun get" popup.
            // WARNING: Add a copy of your default sprite to Ammonomicon Encounter Icon Collection!
            // That means, "sprites/Ammonomicon Encounter Icon Collection/defaultsprite.png" in your mod .zip. You can see an example of this with inside the mod folder.
            gun.SetupSprite(null, "hand_idle_001", 8);
            // ETGMod automatically checks which animations are available.
            // The numbers next to "shootAnimation" determine the animation fps. You can also tweak the animation fps of the reload animation and idle animation using this method.
            gun.SetAnimationFPS(gun.shootAnimation, 8);
            gun.SetAnimationFPS(gun.reloadAnimation, 8);
            // Every modded gun has base projectile it works with that is borrowed from other guns in the game. 
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.
            gun.AddProjectileModuleFrom("balloon_gun", true, true);
            // Here we just take the default projectile module and change its settings how we want it to be.
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.DefaultModule.angleVariance = 0f;
            gun.gunClass = GunClass.SILLY;
            gun.gunHandedness = GunHandedness.HiddenOneHanded;
            gun.reloadTime = 1.5f;
            gun.DefaultModule.cooldownTime = .01f;
            gun.DefaultModule.numberOfShotsInClip = 1;
            gun.SetBaseMaxAmmo(300);
            // Here we just set the quality of the gun and the "EncounterGuid", which is used by Gungeon to identify the gun.
            gun.quality = PickupObject.ItemQuality.A;
            gun.encounterTrackable.EncounterGuid = "Oi Josuke!";
            //This block of code helps clone our projectile. Basically it makes it so things like Shadow Clone and Hip Holster keep the stats/sprite of your custom gun's projectiles.
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            //projectile.baseData allows you to modify the base properties of your projectile module.
            //In our case, our gun uses modified projectiles from the ak-47.
            //Setting static values for a custom gun's projectile stats prevents them from scaling with player stats and bullet modifiers (damage, shotspeed, knockback)
            //You have to multiply the value of the original projectile you're using instead so they scale accordingly. For example if the projectile you're using as a base has 10 damage and you want it to be 6 you use this
            //In our case, our projectile has a base damage of 5.5, so we multiply it by 1.1 so it does 10% more damage from the ak-47.
            projectile.baseData.damage *= 0f;
            projectile.baseData.speed *= 1f;
            projectile.baseData.range = 1.5f;
            projectile.baseData.force = -15;
            projectile.AppliedStunDuration = 2.5f;
            projectile.StunApplyChance = 100f;
            projectile.AppliesStun = true;
            projectile.HasDefaultTint = true;
            projectile.DefaultTintColor = UnityEngine.Color.blue;
            projectile.collidesWithProjectiles = true;
            projectile.projectileHitHealth = 100;
            
            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
         
        public override void OnPostFired(PlayerController player, Gun gun)
        {
            //This determines what sound you want to play when you fire a gun.
            //Sounds names are based on the Gungeon sound dump, which can be found at EnterTheGungeon/Etg_Data/StreamingAssets/Audio/GeneratedSoundBanks/Windows/sfx.txt
            gun.PreventNormalFireAudio = true;
            AkSoundEngine.PostEvent("Play_OBJ_blackhole_close_01", base.gameObject);
            AkSoundEngine.PostEvent("Play_WPN_blackhole_impact_01", base.gameObject);
            AkSoundEngine.PostEvent("Play_WPN_blackhole_shot_01", base.gameObject);
            

        }
       
        private bool HasReloaded;
        private bool HasSynergy;
        protected void Update()
        {
            if (gun.CurrentOwner)
            {

                if (!gun.PreventNormalFireAudio)
                {
                    this.gun.PreventNormalFireAudio = true;
                }
                if (!gun.IsReloading && !HasReloaded)
                {
                    this.HasReloaded = true;
                    
                }
                bool flag = this.gun.CurrentOwner;
                if (flag)
                {
                    GameUIBossHealthController boss = new GameUIBossHealthController();
                    PlayerController owner = (PlayerController)this.gun.CurrentOwner;
                    bool flag2 = owner.HasPickupID(213);
                    if (flag2)
                    {
                        HasSynergy = true;
                    }
                    else
                    {
                        HasSynergy = false;
                    }
                }
            }
            safebullets();
            safebullets();

        }

        // OI JOSUKE 
        // I DELETED EVERYTHING IN THE GUNGEON
        // WITH MY [ZA HANDO]
        // AINT THAT WACKY

        public void safebullets()
        {
            foreach (var projectile in GetbuddyBullets())
            {
                projectile.collidesWithPlayer = false;
                projectile.UpdateCollisionMask();
            }
        }

        private List<Projectile> GetbuddyBullets()
        {
            List<Projectile> list = new List<Projectile>();
            var allProjectiles = StaticReferenceManager.AllProjectiles;
            for (int i = 0; i < allProjectiles.Count; i++)
            {
                Projectile projectile = allProjectiles[i];
                if (projectile && projectile.sprite && !projectile.ImmuneToBlanks)
                {
                    if (projectile.Owner != null)
                    {
                        if (projectile.Owner == bud0 || bud1 || bud2 || bud3 || bud4 || bud5 || bud6 || bud7 || bud8 || bud9 || bud10 || bud11 || bud12 || bud13 || bud14 )
                        {
                            list.Add(projectile);
                        }
                    }
                }
            }
            return list;
        }
        public int limiter = 0;
        
        private void Spawnhando()
        {
            //boss checker
            PlayerController owner = (PlayerController)this.gun.CurrentOwner;
            bool inBossFight = false;
            if (owner.CurrentRoom != null && owner.CurrentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.All))
            {
                foreach (AIActor aiactor in owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
                {
                    if (aiactor != null && aiactor.healthHaver != null && aiactor.healthHaver.IsBoss) 
                    {
                        inBossFight = true;
                    }
                    
                }
            }
           
            //lichspawner
            if (inBossFight && HasSynergy && limiter <= 14)
            { 
                string guid;
                if (this.gun.CurrentOwner)
                {
                    guid = "cf27dd464a504a428d87a8b2560ad40a";
                }
                else
                {
                    guid = "cf27dd464a504a428d87a8b2560ad40a";
                }

                AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                IntVector2? intVector = new IntVector2?(owner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                aiactor.CanTargetEnemies = true;
                aiactor.CanTargetPlayers = false;
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                aiactor.gameObject.AddComponent<KillOnRoomClear>();
                aiactor.IsHarmlessEnemy = true;
                aiactor.CompanionOwner = owner;
                aiactor.CanDropCurrency = false;
                aiactor.IsNormalEnemy = true;
                aiactor.projectile.collidesWithPlayer = false;
                aiactor.isPassable = true;
                aiactor.IgnoreForRoomClear = true;
                aiactor.reinforceType = AIActor.ReinforceType.Instant;
                aiactor.HandleReinforcementFallIntoRoom(0f);
                if(limiter == 0)
                {
                    bud0 = aiactor;
                }
                if(limiter == 1)
                {
                    bud1 = aiactor;
                }
                if (limiter == 2)
                {
                    bud2 = aiactor;
                }
                if (limiter == 3)
                {
                    bud3 = aiactor;
                }
                if (limiter == 4)
                {
                    bud4 = aiactor;
                }
                if (limiter == 5)
                {
                    bud5 = aiactor;
                }
                if (limiter == 6)
                {
                    bud6 = aiactor;
                }
                if (limiter == 7)
                {
                    bud7 = aiactor;
                }
                if (limiter == 8)
                {
                    bud8 = aiactor;
                }
                if (limiter == 9)
                {
                    bud9 = aiactor;
                }
                if (limiter == 10)
                {
                    bud10 = aiactor;
                }
                if (limiter == 11)
                {
                    bud11 = aiactor;
                }
                if (limiter == 12)
                {
                    bud12 = aiactor;
                }
                if (limiter == 13)
                {
                    bud13 = aiactor;
                }
                if (limiter == 14)
                {
                    bud14 = aiactor;
                }
                
                

                limiter++;
            }
            if(!inBossFight && HasSynergy)
            {
                limiter = 0;
            }
           
           
        }

        
        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
           
            

            if (gun.IsReloading && this.HasReloaded)
            {

                HasReloaded = false;
                AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);

                AkSoundEngine.PostEvent("Play_WPN_Life_Orb_Blast_01", gameObject);
                bool flag2 = player.CurrentRoom != null;
                if (flag2)
                {
                    player.CurrentRoom.ApplyActionToNearbyEnemies(player.CenterPosition, 3.5f, new Action<AIActor, float>(this.ProcessEnemy));
                    Spawnhando();
                    
                }
            }
            
        }
        private static System.Random rng = new System.Random();
        private void ProcessEnemy(AIActor target, float distance)
        {
            for (int i = 0; i < this.TargetEnemies.Count; i++)
            {
                bool flag = target.EnemyGuid == this.TargetEnemies[i];
                if (flag)
                {
                    GameManager.Instance.Dungeon.StartCoroutine(this.HandleEnemySuck(target));
                    target.EraseFromExistence(true);
                    int amt = rng.Next(0, 1);
                    money((PlayerController)this.gun.CurrentOwner, amt);
                    break;
                }
            }
        }
      
        public static void money(PlayerController player,int amt)
        {
            
           if(amt >= 0)
            {
                player.GiveItem("casing");
                amt--;
                money(player, amt);
            }
           
        }
        private IEnumerator HandleEnemySuck(AIActor target)
        {
            Transform copySprite = this.CreateEmptySprite(target);
            Vector3 startPosition = copySprite.transform.position;
            float elapsed = 0f;
            float duration = 0.5f;
            while (elapsed < duration)
            {
                elapsed += BraveTime.DeltaTime;
                bool flag = this.gun && copySprite;
                if (flag)
                {
                    Vector3 position = this.gun.PrimaryHandAttachPoint.position;
                    float t = elapsed / duration * (elapsed / duration);
                    copySprite.position = Vector3.Lerp(startPosition, position, t);
                    copySprite.rotation = Quaternion.Euler(0f, 0f, 360f * BraveTime.DeltaTime) * copySprite.rotation;
                    copySprite.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.1f, 0.1f, 0.1f), t);
                    position = default(Vector3);
                }
                yield return null;
            }
            bool flag2 = copySprite;
            if (flag2)
            {
                UnityEngine.Object.Destroy(copySprite.gameObject);
            }
            bool flag3 = this.gun;
            if (flag3)
            {
                this.gun.GainAmmo(1);
            }
            yield break;
        }

        
        private Transform CreateEmptySprite(AIActor target)
        {
            GameObject gameObject = new GameObject("suck image");
            gameObject.layer = target.gameObject.layer;
            tk2dSprite tk2dSprite = gameObject.AddComponent<tk2dSprite>();
            gameObject.transform.parent = SpawnManager.Instance.VFX;
            tk2dSprite.SetSprite(target.sprite.Collection, target.sprite.spriteId);
            tk2dSprite.transform.position = target.sprite.transform.position;
            GameObject gameObject2 = new GameObject("image parent");
            gameObject2.transform.position = tk2dSprite.WorldCenter;
            tk2dSprite.transform.parent = gameObject2.transform;
            bool flag = target.optionalPalette != null;
            if (flag)
            {
                tk2dSprite.renderer.material.SetTexture("_PaletteTex", target.optionalPalette);
            }
            return gameObject2.transform;
        }

        AIActor bud0;
        AIActor bud1;
        AIActor bud2;
        AIActor bud3;
        AIActor bud4;
        AIActor bud5;
        AIActor bud6;
        AIActor bud7;
        AIActor bud8;
        AIActor bud9;
        AIActor bud10;
        AIActor bud11;
        AIActor bud12;
        AIActor bud13;
        AIActor bud14;
       


        //deletes all except FULL Bosses, companions, NPCs and mimics
        public List<string> TargetEnemies = new List<string>
        {
            "db35531e66ce41cbb81d507a34366dfe",
            "6b7ef9e5d05b4f96b04f05ef4a0d1b18",
            "128db2f0781141bcb505d8f00f9e4d47",
            "70216cae6c1346309d86d4a0b4603045",
            "4d37ce3d666b4ddda8039929225b7ede",
            "42be66373a3d4d89b91a35c9ff8adfec",
            "01972dee89fc4404a5c408d50007dad5",
            "88b6b6a93d4b4234a67844ef4728382c",
            "05891b158cd542b1a5f3df30fb67a7ff",
            "0239c0680f9f467dbe5c4aab7dd1eca6",
            "4db03291a12144d69fe940d5a01de376",
            "af84951206324e349e1f13f9b7b60c1a",
            "042edb1dfb614dc385d5ad1b010f2ee3",
            "b5503988e3684e8fa78274dd0dda0bf5",
            "06a82532447247f9ada1940d079a31a7",
            "df7fb62405dc4697b7721862c7b6b3cd",
            "7b0b1b6d9ce7405b86b75ce648025dd6",
            "ffdc8680bdaa487f8f31995539f74265",
            "b54d89f9e802455cbb2b8a96a31e8259",
            "864ea5a6a9324efc95a0dd2407f42810",
            "c1757107b9a44f0c823a707adeb4ae7e",
            "c4fba8def15e47b297865b18e36cbef8",
            "b08ec82bef6940328c7ecd9ffc6bd16c",
            "b5e699a0abb94666bda567ab23bd91c4",
            "2752019b770f473193b08b4005dc781f",
            "9b2cf2949a894599917d4d391a0b7394",
            "249db525a9464e5282d02162c88e0357",
            "022d7c822bc146b58fe3b0287568aaa2",
            "f155fd2759764f4a9217db29dd21b7eb",
            "336190e29e8a4f75ab7486595b700d4a",
            "5288e86d20184fa69c91ceb642d31474",
            "95ec774b5a75467a9ab05fa230c0c143",
            "2feb50a6a40f4f50982e89fd276f6f15",
            "2d4f8b5404614e7d8b235006acde427a",
            "21dd14e5ca2a4a388adab5b11b69a1e1",
            "c0ff3744760c4a2eb0bb52ac162056e6",
            "b4666cb6ef4f4b038ba8924fd8adf38f",
            "6f22935656c54ccfb89fca30ad663a64",
            "a400523e535f41ac80a43ff6b06dc0bf",
            "7ec3e8146f634c559a7d58b19191cd43",
            "c2f902b7cbe745efb3db4399927eab34",
            "ac986dabc5a24adab11d48a4bccf4cb1",
            "48d74b9c65f44b888a94f9e093554977",
            "3cadf10c489b461f9fb8814abc1a09c1",
            "e61cab252cfb435db9172adc96ded75f",
            "fe3fe59d867347839824d5d9ae87f244",
            "b8103805af174924b578c98e95313074",
            "ec8ea75b557d4e7b8ceeaacdf6f8238c",
            "ed37fa13e0fa4fcf8239643957c51293",
            "6e972cd3b11e4b429b888b488e308551",
            "ccf6d241dad64d989cbcaca2a8477f01",
            "0b547ac6b6fc4d68876a241a88f5ca6a",
            "37340393f97f41b2822bc02d14654172",
            "31a3ea0c54a745e182e22ea54844a82d",
            "c5b11bfc065d417b9c4d03a5e385fe2c",
            "43426a2e39584871b287ac31df04b544",
            "9d50684ce2c044e880878e86dbada919",
            "9b4fb8a2a60a457f90dcf285d34143ac",
            "f905765488874846b7ff257ff81d6d0c",
            "eed5addcc15148179f300cc0d9ee7f94",
            "5f3abc2d561b4b9c9e72b879c6f10c7e",
            "044a9f39712f456597b9762893fbc19c",
            "33b212b856b74ff09252bf4f2e8b8c57",
            "3f2026dc3712490289c4658a2ba4a24b",
            "e5cffcfabfae489da61062ea20539887",
            "b1540990a4f1480bbcb3bea70d67f60d",
            "56f5a0f2c1fc4bc78875aea617ee31ac",
            "56fb939a434140308b8f257f0f447829",
            "d8a445ea4d944cc1b55a40f22821ae69",
            "a9cc6a4e9b3d46ea871e70a03c9f77d4",
            "556e9f2a10f9411cb9dbfd61e0e0f1e1",
            "c5a0fd2774b64287bf11127ca59dd8b4",
            "b67ffe82c66742d1985e5888fd8e6a03",
            "d9632631a18849539333a92332895ebd",
            "1898f6fe1ee0408e886aaf05c23cc216",
            "abd816b0bcbf4035b95837ca931169df",
            "07d06d2b23cc48fe9f95454c839cb361",
            "78eca975263d4482a4bfa4c07b32e252",
            "cf27dd464a504a428d87a8b2560ad40a",
            "d1c9781fdac54d9e8498ed89210a0238",
            "1a78cfb776f54641b832e92c44021cf2",
            "1bd8e49f93614e76b140077ff2e33f2b",
            "d4a9836f8ab14f3fadd0f597438b1f1f",
            "116d09c26e624bca8cca09fc69c714b3",
            "0ff278534abb4fbaaa65d3f638003648",
            "1a4872dafdb34fd29fe8ac90bd2cea67",
            "383175a55879441d90933b5c4e60cf6f",
            "72d2f44431da43b8a3bae7d8a114a46d",
            "3f6d6b0c4a7c4690807435c7b37c35a5",
            "cd4a4b7f612a4ba9a720b9f97c52f38c",
            "8bb5578fba374e8aae8e10b754e61d62",
            "98ea2fe181ab4323ab6e9981955a9bca",
            "062b9b64371e46e195de17b6f10e47c8",
            "d5a7b95774cd41f080e517bea07bf495",
            "c182a5cb704d460d9d099a47af49c913",
            "206405acad4d4c33aac6717d184dc8d4",
            "7f665bd7151347e298e4d366f8818284",
            "e21ac9492110493baef6df02a2682a0d",
            "02a14dec58ab45fb8aacde7aacd25b01",
            "47bdfec22e8e4568a619130a267eab5b",
            "78a8ee40dff2477e9c2134f6990ef297",
            "566ecca5f3b04945ac6ce1f26dedbf4f",
            "1398aaccb26d42f3b998c367b7800b85",
            "9044d8e4431f490196ba697927a4e3d4",
            "40bf8b0d97794a26b8c440425ec8cac1",
            "3590db6c4eac474a93299a908cb77ab2",
            "8b4a938cdbc64e64822e841e482ba3d2",
            "b1770e0f1c744d9d887cc16122882b4f",
            "c0260c286c8d4538a697c5bf24976ccf",
            "3e98ccecf7334ff2800188c417e67c15",
            "ba657723b2904aa79f9e51bce7d23872",
            "19b420dec96d4e9ea4aebc3398c0ba7a",
            "463d16121f884984abe759de38418e48",
            "844657ad68894a4facb1b8e1aef1abf9",
            "f38686671d524feda75261e469f30e0b",
            "a446c626b56d4166915a4e29869737fd",
            "22fc2c2c45fb47cf9fb5f7b043a70122",
            "98ca70157c364750a60f5e0084f9d3e2",
            "1bc2a07ef87741be90c37096910843ab",
            "475c20c1fd474dfbad54954e7cba29c1",
            "45192ff6d6cb43ed8f1a874ab6bef316",
            "be0683affb0e41bbb699cb7125fdded6",
            "eeb33c3a5a8e4eaaaaf39a743e8767bc",
            "ba928393c8ed47819c2c5f593100a5bc",
            "4538456236f64ea79f483784370bc62f",
            "d4dd2b2bbda64cc9bcec534b4e920518",
            "8a9e9bedac014a829a48735da6daf3da",
            "cf2b7021eac44e3f95af07db9a7c442c",
            "c50a862d19fc4d30baeba54795e8cb93",
            "bb73eeeb9e584fbeaf35877ec176de28",
            "db97e486ef02425280129e1e27c33118",
            "edc61b105ddd4ce18302b82efdc47178",
            "39de9bd6a863451a97906d949c103538",
            "98fdf153a4dd4d51bf0bafe43f3c77ff",
            "70058857b0a641a888ac4389bd10f455",
            "",
            "",
            "",
            "",
            "",
            "",
            "",


        };
    }

}
