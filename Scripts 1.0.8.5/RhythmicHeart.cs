using System;
using System.Collections.Generic;
using Dungeonator;
using ItemAPI;
using UnityEngine;

namespace Knives
{
	// Token: 0x02000024 RID: 36
	public class Rhythmic_heart :PlayerItem
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public static void Register()
		{
		
			string itemName = "Rhythmic heart";

			string resourceName = "Knives/Resources/led maiden";

			GameObject obj = new GameObject(itemName);

			var item = obj.AddComponent<Rhythmic_heart>();

			ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

			//Ammonomicon entry variables
			string shortDesc = "A faint pulse";
			string longDesc =

				"";

			//Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
			//Do this after ItemBuilder.AddSpriteToObject!
			ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

			ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Timed, 1f);



			item.consumable = false;
			item.quality = PickupObject.ItemQuality.A;
			
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000B768 File Offset: 0x00009968
		public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
			player.OnEnteredCombat = (Action)Delegate.Combine(player.OnEnteredCombat, new Action(this.Heartbeat));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000B798 File Offset: 0x00009998
		private void Notify(string header, string text)
		{
			tk2dBaseSprite notificationObjectSprite = GameUIRoot.Instance.notificationController.notificationObjectSprite;
			GameUIRoot.Instance.notificationController.DoCustomNotification(header, text, notificationObjectSprite.Collection, notificationObjectSprite.spriteId, UINotificationController.NotificationColor.PURPLE, true, true);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000B7D8 File Offset: 0x000099D8
		protected override void DoEffect(PlayerController user)
		{
			AkSoundEngine.PostEvent("Play_OBJ_plate_press_01", base.gameObject);
			float num = UnityEngine.Random.Range(0f, 1.3f);
			bool flag = (double)num < 0.1;
			bool flag2 = flag;
			if (flag2)
			{
				string header = "Fist for Guns";
				string text = "Punched-out";
				this.Notify(header, text);
				AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
				AkSoundEngine.PostEvent("Play_MUS_RatPunch_Theme_03", base.gameObject);
			}
			else
			{
				bool flag3 = (double)num < 0.2;
				bool flag4 = flag3;
				if (flag4)
				{
					string header = "Enter the Gungeon";
					string text = "Enter the Gungeon";
					this.Notify(header, text);
					AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
					AkSoundEngine.PostEvent("Play_MUS_Anthem_Winner_Short_01", base.gameObject);
				}
				else
				{
					bool flag5 = (double)num < 0.3;
					bool flag6 = flag5;
					if (flag6)
					{
						string header = "Paradox";
						string text = "Gunslinger's Anthem";
						this.Notify(header, text);
						AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
						AkSoundEngine.PostEvent("Play_MUS_Lich_Double_01", base.gameObject);
					}
					else
					{
						bool flag7 = (double)num < 0.4;
						bool flag8 = flag7;
						if (flag8)
						{
							string header = "Gungeon Lite";
							string text = "Remixed";
							this.Notify(header, text);
							AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
							AkSoundEngine.PostEvent("Play_MUS_Anthem", base.gameObject);
						}
						else
						{
							bool flag9 = (double)num < 0.5;
							bool flag10 = flag9;
							if (flag10)
							{
								string header = "Filthy Oubliette";
								string text = "Toxic Jam";
								this.Notify(header, text);
								AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
								AkSoundEngine.PostEvent("Play_MUS_Sewer_Theme_01", base.gameObject);
							}
							else
							{
								bool flag11 = (double)num < 0.6;
								bool flag12 = flag11;
								if (flag12)
								{
									string header = "Hollow Howl";
									string text = "Chilled";
									this.Notify(header, text);
									AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
									AkSoundEngine.PostEvent("Play_MUS_Catacombs_Theme_01", base.gameObject);
								}
								else
								{
									bool flag13 = (double)num < 0.7;
									bool flag14 = flag13;
									if (flag14)
									{
										string header = "Office Party Massacre";
										string text = "R&G";
										this.Notify(header, text);
										AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
										AkSoundEngine.PostEvent("Play_MUS_Office_Theme_01", base.gameObject);
									}
									else
									{
										bool flag15 = (double)num < 0.8;
										bool flag16 = flag15;
										if (flag16)
										{
											string header = "Abbey or Die";
											string text = "Orgun Melody";
											this.Notify(header, text);
											AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
											AkSoundEngine.PostEvent("Play_MUS_Cathedral_Theme_01", base.gameObject);
										}
										else
										{
											bool flag17 = (double)num < 0.9;
											bool flag18 = flag17;
											if (flag18)
											{
												string header = "Forge Forgives Not";
												string text = "Hot Mix";
												this.Notify(header, text);
												AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
												AkSoundEngine.PostEvent("Play_MUS_Forge_Theme_01", base.gameObject);
											}
											else
											{
												bool flag19 = num < 1f;
												bool flag20 = flag19;
												if (flag20)
												{
													string header = "Space Jams";
													string text = "Robotic Tunes";
													this.Notify(header, text);
													AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
													AkSoundEngine.PostEvent("Play_MUS_Space_Theme_01", base.gameObject);
												}
												else
												{
													bool flag21 = (double)num < 1.1;
													bool flag22 = flag21;
													if (flag22)
													{
														string header = "Trapped in Bullet Hell";
														string text = "Deep Down";
														this.Notify(header, text);
														AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
														AkSoundEngine.PostEvent("Play_MUS_BulletHell_Theme_01", base.gameObject);
													}
													else
													{
														bool flag23 = (double)num < 1.2;
														bool flag24 = flag23;
														if (flag24)
														{
															string header = "Black Powder Stomp";
															string text = "In the Mines";
															this.Notify(header, text);
															AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
															AkSoundEngine.PostEvent("Play_MUS_Mines_Theme_01", base.gameObject);
														}
														else
														{
															string header = "Dragun Tooth";
															string text = "No More Playing Around";
															this.Notify(header, text);
															AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
															AkSoundEngine.PostEvent("Play_MUS_Boss_Theme_Dragun_02", base.gameObject);
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000BC48 File Offset: 0x00009E48
		public void ResetMusic(Dungeon d)
		{
			bool flag = !string.IsNullOrEmpty(d.musicEventName);
			if (flag)
			{
				this.m_cachedMusicEventCore = d.musicEventName;
			}
			else
			{
				this.m_cachedMusicEventCore = "Play_MUS_Dungeon_Theme_01";
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000BC88 File Offset: 0x00009E88

		private void DropMusic()
		{
			AkSoundEngine.PostEvent("Stop_MUS_All", base.gameObject);
			AkSoundEngine.PostEvent("Play_OBJ_plate_press_01", base.gameObject);
			this.ResetMusic(GameManager.Instance.Dungeon);
			AkSoundEngine.PostEvent(this.m_cachedMusicEventCore, GameManager.Instance.gameObject);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		private void Heartbeat()
		{
			//detect BPM and set timescale rhythm.
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000BD3C File Offset: 0x00009F3C
		protected void AffectEnemy(AIActor target)
		{
			bool flag = target && target.behaviorSpeculator;
			if (flag)
			{
				target.behaviorSpeculator.Stun(1.6f, true);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000BD78 File Offset: 0x00009F78
		protected override void OnPreDrop(PlayerController user)
		{
			this.heartbeatactive = false;
			user.OnEnteredCombat = (Action)Delegate.Remove(user.OnEnteredCombat, new Action(this.Heartbeat));
			this.DropMusic();
		}

		public int BPM = 0;
		// Token: 0x04000045 RID: 69
		public bool heartbeatactive;

		// Token: 0x04000046 RID: 70
		private string m_cachedMusicEventCore;
	}
}