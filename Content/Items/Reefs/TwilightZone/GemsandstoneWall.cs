﻿using Terraria.ModLoader;

namespace EndlessEscapade.Content.Items.Reefs.TwilightZone;

public class GemsandstoneWall : ModItem
{
    public override void SetDefaults() {
        Item.DefaultToPlacableWall((ushort)ModContent.WallType<Walls.Reefs.TwilightZone.GemsandstoneWall>());
    }
}