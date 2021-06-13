using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using ItemAPI;
using Dungeonator;
using System.Reflection;
using Random = System.Random;
using FullSerializer;
using System.Collections;
using Gungeon;
using MonoMod.RuntimeDetour;



namespace Knives
{   
    public class Stopda : IounStoneOrbitalItem
    {
        public static void Register()
        {
            string name = "Stopda Rock";
            string resourcePath = "Knives/Resources/Stopda_Rock";
            GameObject gameObject = new GameObject();
            Stopda rock = gameObject.AddComponent<Stopda>();
            ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
            string shortDesc = "Cant Stopda Rock";
            string longDesc = "Ever since the dawn of the universe this rock has never stopped moving even if it had to bend the laws of physics to get some speed." +
                " Even when is seems to sit still it vibrates unstoppably. This Stone has the unique ability to store all energy projected into it to use at will." +
                " The stone will jump with joy upon collecting new energy." +
                "\n\n\n - Knife_to_a_Gunfight";
            rock.SetupItem(shortDesc, longDesc, "ski");
            rock.quality = PickupObject.ItemQuality.B;
            Stopda.BuildPrefab();

            rock.OrbitalPrefab = Stopda.orbitalPrefab;
            
            rock.Identifier = IounStoneOrbitalItem.IounStoneIdentifier.GENERIC;
           

        }

       
        public static void BuildPrefab()
        {
            bool flag = Stopda.orbitalPrefab != null;
            if (!flag)
            {
                GameObject gameObject = SpriteBuilder.SpriteFromResource("Knives/Resources/Stopda_Rock", null);
                gameObject.name = "Stopda";
                SpeculativeRigidbody speculativeRigidbody = gameObject.GetComponent<tk2dSprite>().SetUpSpeculativeRigidbody(IntVector2.Zero, new IntVector2(10, 10));
                speculativeRigidbody.CollideWithTileMap = false;
                speculativeRigidbody.CollideWithOthers = true;
                
                speculativeRigidbody.PrimaryPixelCollider.CollisionLayer = CollisionLayer.EnemyBulletBlocker;
                Stopda.orbitalPrefab = gameObject.AddComponent<PlayerOrbital>();
                Stopda.orbitalPrefab.motionStyle = PlayerOrbital.OrbitalMotionStyle.ORBIT_PLAYER_ALWAYS;
                Stopda.orbitalPrefab.orbitDegreesPerSecond = 40f;
                Stopda.orbitalPrefab.shouldRotate = true ;
                Stopda.orbitalPrefab.orbitRadius = 2.5f;
                

                Stopda.orbitalPrefab.SetOrbitalTier(0);
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
                FakePrefab.MarkAsFakePrefab(gameObject);
                gameObject.SetActive(false);
                
                
            }
        }


        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);

            Stopda.guonHook = new Hook(typeof(PlayerOrbital).GetMethod("Initialize"), typeof(Stopda).GetMethod("GuonInit"));
            bool flag = player.gameObject.GetComponent<Stopda.BaBoom>() != null;
            bool flag2 = flag;
            bool flag3 = flag2;
            bool flag4 = flag3;
            if (flag4)
            {
                player.gameObject.GetComponent<Stopda.BaBoom>().Destroy();
            }
            player.gameObject.AddComponent<Stopda.BaBoom>();
            GameManager.Instance.OnNewLevelFullyLoaded += this.FixGuon;
            bool flag5 = this.m_extantOrbital != null;
            bool flag6 = flag5;
            bool flag7 = flag6;
            if (flag7)
            {
                SpeculativeRigidbody specRigidbody = this.m_extantOrbital.GetComponent<PlayerOrbital>().specRigidbody;
                specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.OnPreCollison));
            }

            
        }
        public static void GuonInit(Action<PlayerOrbital, PlayerController> orig, PlayerOrbital self, PlayerController player)
        {
            orig(self, player);
        }
        private void FixGuon()
        {
            bool flag = base.Owner && base.Owner.GetComponent<Stopda.BaBoom>() != null;
            bool flag2 = flag;
            bool flag3 = flag2;
            bool flag4 = flag3;
            if (flag4)
            {
                base.Owner.GetComponent<Stopda.BaBoom>().Destroy();
            }
            bool flag5 = this.m_extantOrbital != null;
            bool flag6 = flag5;
            bool flag7 = flag6;
            if (flag7)
            {
                SpeculativeRigidbody specRigidbody = this.m_extantOrbital.GetComponent<PlayerOrbital>().specRigidbody;
                specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.OnPreCollison));
            }
            PlayerController owner = base.Owner;
            owner.gameObject.AddComponent<Stopda.BaBoom>();
        }
        private void OnPreCollison(SpeculativeRigidbody myRigidbody, PixelCollider myCollider, SpeculativeRigidbody other, PixelCollider otherCollider)
        {

           
                rockpoints++;

                RockGoBrrrrrrr(this.Owner, this.m_extantOrbital.GetComponent<PlayerOrbital>());

                other.projectile.ForceDestruction();
           
       
        }
    
        protected override void Update()
        {

           


            base.Update();
        }

        float rockpoints;
       
        public IEnumerator RockGoBrrrrrrr(PlayerController user,PlayerOrbital wee)
        {
            
            wee.orbitDegreesPerSecond = 40 + (rockpoints);
            
            return null;
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Stopda.speedUp = false;
            return base.Drop(player);
        }

        protected override void OnDestroy()
        {
            Stopda.speedUp = false;
            base.OnDestroy();
        }

        public static bool speedUp = false;
        public static PlayerOrbital orbitalPrefab;
        public List<IPlayerOrbital> orbitals = new List<IPlayerOrbital>();
        public static Hook guonHook;
        

        private class BaBoom : BraveBehaviour
        {
            // Token: 0x06000B0A RID: 2826 RVA: 0x0005EB58 File Offset: 0x0005CD58
            private void Start()
            {
                this.owner = base.GetComponent<PlayerController>();
            }

            // Token: 0x06000B0B RID: 2827 RVA: 0x0005EB67 File Offset: 0x0005CD67
            public void Destroy()
            {
                UnityEngine.Object.Destroy(this);
            }

            // Token: 0x040005BC RID: 1468
            private PlayerController owner;
        }
    }

}
