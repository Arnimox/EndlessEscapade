using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EEMod.NPCs.CoralReefs
{
    public class Grebyser : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grebyser");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;

            npc.HitSound = SoundID.NPCHit25;
            npc.DeathSound = SoundID.NPCDeath28;

            npc.alpha = 0;

            npc.lifeMax = 550;
            npc.defense = 10;

            npc.width = 32;
            npc.height = 32;

            npc.noGravity = false;

            npc.lavaImmune = false;
            npc.noTileCollide = false;
            //bannerItem = ModContent.ItemType<Items.Banners.GiantSquidBanner>();
        }

        public override void AI()
        {
            npc.velocity.X = npc.ai[1];
            if (npc.ai[0] == 0)
                npc.ai[1] = 1;
            npc.ai[0]++;
            if (npc.ai[0] % 180 == 0 && Helpers.OnGround(npc))
            {
                npc.velocity.Y -= 5;
                if (Helpers.isCollidingWithWall(npc))
                {
                    if (npc.ai[1] == -1)
                    {
                        npc.ai[1] = 1;
                    }
                    else
                    {
                        npc.ai[1] = -1;
                    }
                }
            }
            npc.ai[2]++;
            if(npc.ai[2] >= 300)
            {
                Main.NewText("ae");
                //Projectile.NewProjectile(npc.Center + new Vector2(0, -16), new Vector2(0, -5), ModContent.ProjectileType<GrebyserGeyser>(), 20, 2f);
                npc.ai[2] = 0;
            }
        }
    }
}