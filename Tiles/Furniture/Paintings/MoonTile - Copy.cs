using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using EEMod.Items.Placeables.Paintings;
using Microsoft.Xna.Framework;

namespace EEMod.Tiles.Furniture.Paintings
{
    public class CrownedKingTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Crowned King");
            AddMapEntry(new Color(255, 168, 28), name);
            dustType = 11;
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<CrownedKing>());
        }
    }
}