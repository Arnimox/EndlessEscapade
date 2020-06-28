using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EEMod.Items.Placeables;

namespace EEMod.Tiles.Walls
{
<<<<<<< HEAD:Tiles/Walls/VolcanoBG.cs
    public class VolcanoBG : ModWall
=======
    public class MagmastoneWallTile : ModWall
>>>>>>> a33e64a6c41d4223e366e34f6700c915ee454c03:Tiles/Walls/MagmastoneWallTile.cs
    {
        public override void SetDefaults()
        {
            AddMapEntry(new Color(67, 47, 155));

            Main.wallHouse[Type] = true;
            dustType = 154;
            drop = ModContent.ItemType<MagmastoneWall>();
            soundStyle = 1;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.4f;
            g = 0.4f;
            b = 0.4f;
        }
    }
}