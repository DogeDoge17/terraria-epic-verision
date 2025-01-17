﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.Social.WeGame;
using Steamworks;

//no I didn't resuse code... thanks for asking
namespace TerrariaEpicVerision.NPCs.Enemy.Persona
{
    public class PallasAthena : HighResNPC
    {
        public Aigis aigis;

        public static Aigis tempAigis;

        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Enemy/Persona/PallasAthena High Res");

        public static byte orgiaStack;

        private bool orgiaMode = false;
        
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Athena"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        }

        public override void SetDefaults()
        {
            if (orgiaStack == 2)
            {
                orgiaMode = true;
            }
            orgiaStack = 0;

            if (!orgiaMode)
                NPC.damage = 50;
            else NPC.damage = 0x96;
            NPC.width = 152;
            NPC.height = 101;
            NPC.value = 0;
            NPC.lifeMax = 1;
            NPC.scale = 1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.aiStyle = 22;
            NPC.knockBackResist = 0;
            NPC.defense = 0;
            NPC.noGravity = true;
            NPC.noTileCollide = true;

            source = new Rectangle(0, 0, 750, 600);

            try
            {
                if (tempAigis != null)
                {
                    aigis = tempAigis;
                    tempAigis = null;
                }
            }
            catch
            {
                aigis = null;
            }

        }
        public override void OnKill()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Aigis/PersonaBreak1",
                "TerrariaEpicVerision/Sounds/Aigis/PersonaBreak2"
            };

            if (!killedSelf)
                if (aigis == null)
                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                else
                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), aigis.NPC.position);


            for (int d = 0; d < 5; d++)
            {

                //static int 	NewGore (IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale=1f) 	
                //  Gore.NewGore(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 1.5f);
                Gore.NewGore(null, new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2)), new Vector2(), GoreID.Smoke1, 1.5f);
            }

            base.OnKill();
        }

        bool killedSelf;
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            killedSelf = true;

            NPC.life = 0;


            for (int d = 0; d < 5; d++)
            {

                //static int 	NewGore (IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale=1f) 	
                //  Gore.NewGore(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 1.5f);
                Gore.NewGore(null, new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2)), new Vector2(), GoreID.Smoke1, 1.5f);
            }

            base.ModifyHitPlayer(target, ref modifiers);
        }

  
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Aigis Persona 3's Persona.")
            });
        }

        public override void AI()
        {

            if (aigis != null)
            {
                if(aigis.NPC.life <= 0)
                {
                    for (int d = 0; d < 5; d++)
                    {
                        Gore.NewGore(null, new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2)), new Vector2(), GoreID.Smoke1, 1.5f);
                    }
                    NPC.life = 0;         
                }
            }

            if (Main.LocalPlayer.position.X > NPC.position.X)
            {
                NPC.spriteDirection = 1;
            }
            else
            {
                NPC.spriteDirection = 0;
            }

            base.AI();
        }



        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }
    }

}
