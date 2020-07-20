using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using EEMod.Tiles;
using EEMod.Tiles.Walls;
using System.Collections.Generic;
using System;
using EEMod.Tiles.Furniture;
using EEMod.Tiles.Ores;

namespace EEMod
{
    public class EESubWorlds
    {

        public static Vector2 CoralBoatPos;
        public static void Pyramids(int seed, GenerationProgress customProgressObject = null)
        {
            Main.maxTilesX = 400;
            Main.maxTilesY = 400;
            Main.spawnTileX = 234;
            Main.spawnTileY = 92;
            SubworldManager.Reset(seed);
            SubworldManager.PostReset(customProgressObject);
            EEWorld.EEWorld.FillRegion(400, 400, new Vector2(0, 0), TileID.SandstoneBrick);
            EEWorld.EEWorld.Pyramid(63, 42);
            EEMod.isSaving = false;
        }

        public static void Sea(int seed, GenerationProgress customProgressObject = null)
        {
            Main.maxTilesX = 400;
            Main.maxTilesY = 405;
            Main.spawnTileX = 234;
            Main.spawnTileY = 92;
            SubworldManager.Reset(seed);
            SubworldManager.PostReset(customProgressObject);
            EEWorld.EEWorld.FillWall(400, 400, new Vector2(0, 0), WallID.Waterfall);
            EEMod.isSaving = false;
        }

        public static void CoralReefs(int seed, GenerationProgress customProgressObject = null)
        {
            //Variables and Initialization stuff
            int depth = 70;
            int boatPos = Main.maxTilesX / 2;
            Main.maxTilesX = 1000;
            Main.maxTilesY = 2000;
            SubworldManager.Reset(seed);
            SubworldManager.PostReset(customProgressObject);


            //Rough shape for the reefs
            EEWorld.EEWorld.FillRegion(Main.maxTilesX, Main.maxTilesY, new Vector2(0, 0), ModContent.TileType<GemsandTile>());
            EEWorld.EEWorld.ClearRegion(Main.maxTilesX, Main.maxTilesY / 30, Vector2.Zero);
            for (int i = 0; i < 10; i++)
            {
                for (int j = -5; j < 5; j++)
                    WorldGen.TileRunner(300 + (i * 170) + (j * 10), Main.maxTilesY / 20, 10, 10, ModContent.TileType<GemsandTile>(), true, 0, 0, true, true);
            }
            for (int i = 0; i < 100; i++)
            {
                for (int j = -5; j < 5; j++)
                    WorldGen.TileRunner(300 + (i * 17) + (j * 10), Main.maxTilesY / 20, 4, 10, ModContent.TileType<GemsandTile>(), true, 0, 0, true, true);
            }
            EEWorld.EEWorld.FillRegionNoEdit(Main.maxTilesX, Main.maxTilesY / 20, new Vector2(0, Main.maxTilesY / 40), ModContent.TileType<CoralSand>());
            int maxTiles = (int)(Main.maxTilesX * Main.maxTilesY * 9E-04);


            /*int chasmX = 100;
            int chasmY = 100;
            EEWorld.EEWorld.MakeWavyChasm(chasmX, chasmY, 1000, TileID.StoneSlab, 0.3f, WorldGen.genRand.Next(50, 60));
            EEWorld.EEWorld.MakeWavyChasm2(chasmX - 50, chasmY, 1000, ModContent.TileType<GemsandTile>(), 0.3f, WorldGen.genRand.Next(10, 20), true);
            EEWorld.EEWorld.MakeWavyChasm2(chasmX + 50, chasmY, 1000, ModContent.TileType<GemsandTile>(), 0.3f, WorldGen.genRand.Next(10, 20), true);
            for (int i = 0; i < 5; i++)
            {
                EEWorld.EEWorld.MakeChasm(chasmX + Main.rand.Next(-50, 50) + i * 20, chasmY + (i * 200) + Main.rand.Next(-50, 50), Main.rand.Next(5, 30), TileID.StoneSlab, Main.rand.Next(5, 10), WorldGen.genRand.Next(20, 60), Main.rand.Next(10, 20));

            }
            for (int i = 0; i < 20; i++)
            {
                EEWorld.EEWorld.MakeOvalFlatTop(Main.rand.Next(10, 20), Main.rand.Next(5, 10), new Vector2(chasmX + Main.rand.Next(-10, 10) + i * 15, chasmY + (i * 50) + Main.rand.Next(-20, 20)), ModContent.TileType<GemsandTile>());
                if (i % 5 == 0)
                {
                    EEWorld.EEWorld.MakeLayer(chasmX + Main.rand.Next(-10, 10) + i * 15, chasmY + Main.rand.Next(-20, 20) + (i * 50), 25, 2, ModContent.TileType<GemsandTile>());
                    EEWorld.EEWorld.MakeLayer(chasmX + Main.rand.Next(-10, 10) + i * 5, chasmY + Main.rand.Next(-20, 20) + (i * 50), 20, 1, ModContent.TileType<GemsandTile>());
                    EEWorld.EEWorld.MakeCoral(new Vector2(chasmX + Main.rand.Next(-10, 10) + i * 5, chasmY + Main.rand.Next(-20, 20) + (i * 50)), TileID.Coralstone, Main.rand.Next(4, 8));
                    for (int j = 0; j < 7; j++)
                        EEWorld.EEWorld.MakeOvalFlatTop(WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(5, 10), new Vector2(chasmX + Main.rand.Next(-10, 10) + i * 15 + (j * 35) - 50, chasmY + Main.rand.Next(-20, 20) + (i * 50)), ModContent.TileType<DarkGemsandTile>());
                }
            }
            for (int k = 0; k < maxTiles * 9; k++)
            {
                int xPos = 500;
                int yPos = 1200;
                int size = 80;
                int x = WorldGen.genRand.Next(xPos - (size * 3), xPos + (size * 3));
                int y = WorldGen.genRand.Next(yPos - (size * 3), yPos + (size * 3));
                if (EEWorld.EEWorld.OvalCheck(xPos, yPos, x, y, size * 3, size))
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(10, 20), WorldGen.genRand.Next(5, 10), TileID.StoneSlab, true, 0f, 0f, true, true);
            }

            for (int i = 0; i < 800; i++)
            {
                for (int j = 0; j < 2000; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.type == TileID.StoneSlab)
                        WorldGen.KillTile(i, j);
                }
            }
            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 1500; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, chasmY + j);
                    int yes = WorldGen.genRand.Next(5, 10);
                    if (EEWorld.EEWorld.TileCheck2(i, chasmY + j) == 1 && j % yes == 0)
                    {
                        int selection = WorldGen.genRand.Next(2);
                        switch (selection)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, chasmY + j + 1, ModContent.TileType<CoralLanternTile>());
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, chasmY + j + 1, ModContent.TileType<HangingCoralTile>());
                                break;
                        }
                    }
                    if (EEWorld.EEWorld.TileCheck2(i, chasmY + j) == 2 && j % yes <= 4)
                    {
                        int selection = WorldGen.genRand.Next(10);
                        switch (selection)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, chasmY + j - 1, ModContent.TileType<Coral1Tile>());
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, chasmY + j - 1, ModContent.TileType<Coral2Tile>());
                                break;
                            case 2:
                                WorldGen.PlaceTile(i, chasmY + j - 1, ModContent.TileType<Coral3Tile>());
                                break;
                            case 3:
                                WorldGen.PlaceTile(i, chasmY + j - 1, ModContent.TileType<EyeTile>());
                                break;
                            case 4:
                                WorldGen.PlaceTile(i, chasmY + j - 1, ModContent.TileType<CoralLanternLamp>());
                                break;
                            case 5:
                                WorldGen.PlaceTile(i, chasmY + j - 1, ModContent.TileType<BrainTile>());
                                break;
                            case 6:
                                WorldGen.PlaceTile(i, chasmY + j - 7, ModContent.TileType<BigCoral>());
                                break;
                            case 7:
                                WorldGen.PlaceTile(i, chasmY + j - 7, ModContent.TileType<WavyBigCoral>());
                                break;
                            case 8:
                                WorldGen.PlaceTile(i, chasmY + j - 3, ModContent.TileType<Brain1BigCoral>());
                                break;
                            case 9:
                                WorldGen.PlaceTile(i, chasmY + j - 3, ModContent.TileType<Brain2BigCoral>());
                                break;
                        }
                        if (selection == 5 && j < 300 && Main.rand.NextBool(4))
                            EEWorld.EEWorld.MakeCoral(new Vector2(i, chasmY + j), TileID.Coralstone, Main.rand.Next(4, 8));
                    }
                }
            }


            int barrier = 1000;

            for (int j = 0; j < barrier; j++)
            {
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    Tile tile = Main.tile[i, j];
                    if (tile.type == ModContent.TileType<DarkGemsandTile>() || tile.type == ModContent.TileType<GemsandTile>() || tile.type == ModContent.TileType<LightGemsandTile>())
                    {
                        if (Main.rand.NextBool(2000))
                        {
                            WorldGen.TileRunner(i, j, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(5, 7), ModContent.TileType<LythenOreTile>());
                        }
                    }
                }
            }
            for (int j = 0; j < 2; j++)
                EEWorld.EEWorld.MakeOvalJaggedTop(55, 27, new Vector2(375 + (j * 250) - 25, 1225), ModContent.TileType<DarkGemsandTile>());
            for (int j = 0; j < 2; j++)
                EEWorld.EEWorld.MakeOvalJaggedTop(55, 27, new Vector2(375 + (j * 250) - 25, 1150), ModContent.TileType<DarkGemsandTile>());

            for (int j = 0; j < 2; j++)
                EEWorld.EEWorld.MakeOvalJaggedTop(WorldGen.genRand.Next(40, 50), WorldGen.genRand.Next(25, 30), new Vector2(450 + (j * 100) - 22, 1180), ModContent.TileType<DarkGemsandTile>());

            for (int j = barrier; j < Main.maxTilesY; j++)
            {
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    Tile tile = Main.tile[i, j];
                    if (tile.type == ModContent.TileType<GemsandTile>() || tile.type == ModContent.TileType<DarkGemsandTile>() || tile.type == ModContent.TileType<LightGemsandTile>())
                        if (Main.rand.NextBool(2000))
                            WorldGen.TileRunner(i, j, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(5, 7), ModContent.TileType<HydriteOreTile>());
                }
            }
            for (int i = 2; i < Main.maxTilesX - 2; i++)
            {
                for (int j = 2; j < Main.maxTilesY - 2; j++)
                {
                    Tile.SmoothSlope(i, j);
                }
            }*/
            EEWorld.EEWorld.KillWall(1000, 1000, Vector2.Zero);
            EEWorld.EEWorld.FillRegionWithWater(Main.maxTilesX, Main.maxTilesY - depth, new Vector2(0, depth));
            EEWorld.EEWorld.PlaceShip(boatPos, EEWorld.EEWorld.TileCheckWater(boatPos) - 22, EEWorld.EEWorld.ShipTiles);
            EEWorld.EEWorld.PlaceShipWalls(boatPos, EEWorld.EEWorld.TileCheckWater(boatPos) - 27, EEWorld.EEWorld.ShipWalls);
            CoralBoatPos = new Vector2(boatPos, EEWorld.EEWorld.TileCheckWater(boatPos) - 22);
            /*maxTiles = (int)(Main.maxTilesX * Main.maxTilesY * 9E-04);
            for (int k = 0; k < maxTiles * 60; k++)
            {
                int xPos = 400;
                int yPos = 1600;
                int size = 190;
                int x = WorldGen.genRand.Next(xPos - (size * 2), xPos + (size * 2));
                int y = WorldGen.genRand.Next(yPos - (size * 2), yPos + (size * 2));
                if (EEWorld.EEWorld.OvalCheck(xPos, yPos, x, y, size * 2, size))
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(10, 20), WorldGen.genRand.Next(5, 10), TileID.StoneSlab, true, 0f, 0f, true, true);
            }
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 2000; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.type == TileID.StoneSlab)
                        WorldGen.KillTile(i, j);
                }
            }
            EEWorld.EEWorld.MakeAtlantis(new Vector2(0,1400), new Vector2(900, 500));*/
            EEMod.isSaving = false;
            Main.spawnTileX = boatPos;
            Main.spawnTileY = EEWorld.EEWorld.TileCheckWater(boatPos) - 22;
        }
        public static void Island(int seed, GenerationProgress customProgressObject = null)
        {
            Main.maxTilesX = 600;
            Main.maxTilesY = 500;
            SubworldManager.Reset(seed);
            SubworldManager.PostReset(customProgressObject);


            EEWorld.EEWorld.FillRegionWithWater(Main.maxTilesX, Main.maxTilesY, new Vector2(0, 0));
            EEWorld.EEWorld.RemoveWaterFromRegion(Main.maxTilesX, 170, new Vector2(0, 0));

            EEWorld.EEWorld.MakeOvalJaggedTop(Main.maxTilesX, 50, new Vector2(0, 165), ModContent.TileType<CoralSand>(), 15, 15);

            EEWorld.EEWorld.Island(400, 250, 140);

            EEWorld.EEWorld.FillRegion(Main.maxTilesX, Main.maxTilesY - 190, new Vector2(0, 190), ModContent.TileType<CoralSand>());


            for (int i = 42; i < Main.maxTilesX - 42; i++)
            {
                for (int j = 42; j < Main.maxTilesY - 42; j++)
                {
                    int yes = WorldGen.genRand.Next(0, 5);
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (EEWorld.EEWorld.TileCheck2(i, j) == 2 && yes < 3 && tile.type == ModContent.TileType<CoralSand>())
                    {
                        int selection = WorldGen.genRand.Next(3);
                        switch (selection)
                        {
                            case 0:
                                WorldGen.PlaceTile(i, j - 1, 324);
                                break;
                            case 1:
                                WorldGen.PlaceTile(i, j - 1, 324, style: 2);
                                break;
                            case 2:
                                WorldGen.PlaceTile(i, j - 1, TileID.Coral);
                                break;
                        }
                    }
                    yes = WorldGen.genRand.Next(0, 10);
                    if (EEWorld.EEWorld.TileCheck2(i, j) == 2 && yes == 0 && tile.type == TileID.Grass)
                    {
                        WorldGen.GrowTree(i, j - 1);
                    }
                }
            }

            EEWorld.EEWorld.PlaceShip(50, 150, EEWorld.EEWorld.ShipTiles);
            EEWorld.EEWorld.PlaceShipWalls(50, 150, EEWorld.EEWorld.ShipWalls);

            WorldGen.AddTrees();


            SubworldManager.SettleLiquids();
            EEMod.isSaving = false;
            Main.spawnTileX = 200;
            Main.spawnTileY = 100;
        }
        public static void Cutscene1(int seed, GenerationProgress customProgressObject = null)
        {
            Main.maxTilesX = 400;
            Main.maxTilesY = 400;
            SubworldManager.Reset(seed);
            SubworldManager.PostReset(customProgressObject);
            EEWorld.EEWorld.FillRegion(Main.maxTilesX, Main.maxTilesY, new Vector2(0, 0), ModContent.TileType<VolcanicAshTile>());
            EEWorld.EEWorld.MakeLayer(200, 100, 90, 1, ModContent.TileType<VolcanicAshTile>());
            EEWorld.EEWorld.MakeOvalFlatTop(40, 10, new Vector2(200 - 20, 100), ModContent.TileType<VolcanicAshTile>());
            EEWorld.EEWorld.MakeChasm(200, 140, 110, ModContent.TileType<GemsandTile>(), 0, 5, 0);
            for (int i = 0; i < 400; i++)
            {
                for (int j = 0; j < 400; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.type == ModContent.TileType<GemsandTile>())
                        WorldGen.KillTile(i, j);
                }
            }
            EEWorld.EEWorld.KillWall(Main.maxTilesX, Main.maxTilesY, Vector2.Zero);
            EEWorld.EEWorld.FillWall(Main.maxTilesX, Main.maxTilesY, Vector2.Zero, ModContent.WallType<MagmastoneWallTile>());
            SubworldManager.SettleLiquids();
            EEMod.isSaving = false;
            Main.spawnTileX = 200;
            Main.spawnTileY = 140;
        }
        public static void VolcanoIsland(int seed, GenerationProgress customProgressObject = null)
        {
            Main.maxTilesX = 1200;
            Main.maxTilesY = 800;
            //Main.worldSurface = Main.maxTilesY;
            //Main.rockLayer = Main.maxTilesY;
            SubworldManager.Reset(seed);
            SubworldManager.PostReset(customProgressObject);


            EEWorld.EEWorld.FillRegionWithWater(Main.maxTilesX, Main.maxTilesY, Vector2.Zero);
            EEWorld.EEWorld.RemoveWaterFromRegion(Main.maxTilesX, 360, Vector2.Zero);

            EEWorld.EEWorld.RemoveWaterFromRegion(60, 630, new Vector2(570, 170));
            EEWorld.EEWorld.KillWall(Main.maxTilesX, Main.maxTilesY, Vector2.Zero);
            EEWorld.EEWorld.MakeTriangle(new Vector2(300, 895), 600, 1000, 3, ModContent.TileType<VolcanicAshTile>(), true, true, ModContent.WallType<VolcanicAshWallTile>());
            EEWorld.EEWorld.Island(800, 400, 290);
            EEWorld.EEWorld.FillRegion(Main.maxTilesX, Main.maxTilesY - 190, new Vector2(0, 400), ModContent.TileType<CoralSand>());

            EEWorld.EEWorld.ClearRegionSafely(60, 630, new Vector2(570, 170), ModContent.TileType<CoralSand>());
            EEWorld.EEWorld.ClearRegionSafely(60, 630, new Vector2(570, 170), TileID.Dirt);
            EEWorld.EEWorld.ClearRegionSafely(60, 630, new Vector2(570, 170), TileID.Grass);
            EEWorld.EEWorld.FillRegionWithLava(40, 206, new Vector2(580, 594));
            EEWorld.EEWorld.MakeVolcanoEntrance(598, 596, EEWorld.EEWorld.VolcanoEntrance);

            SubworldManager.SettleLiquids();
            EEMod.isSaving = false;
            Main.spawnTileX = 200;
            Main.spawnTileY = 100;
        }
        public static void VolcanoInside(int seed, GenerationProgress customProgressObject = null)
        {
            Main.maxTilesX = 400;
            Main.maxTilesY = 600;
            //Main.worldSurface = Main.maxTilesY;
            //Main.rockLayer = Main.maxTilesY;
            SubworldManager.Reset(seed);
            SubworldManager.PostReset(customProgressObject);

            EEWorld.EEWorld.FillRegion(Main.maxTilesX, Main.maxTilesY, Vector2.Zero, ModContent.TileType<MagmastoneTile>());
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    if (Main.rand.NextBool(3000))
                        EEWorld.EEWorld.MakeLavaPit(Main.rand.Next(20, 30), Main.rand.Next(7, 20), new Vector2(i, j), Main.rand.NextFloat(0.1f, 0.5f));
                }
            }
            EEWorld.EEWorld.MakeChasm(200, 10, 100, TileID.StoneSlab, 0, 10, 20);
            WorldGen.TileRunner(200, 190, 200, 100, TileID.StoneSlab);
            for (int k = 0; k < Main.maxTilesX; k++)
            {
                for (int l = 0; l < Main.maxTilesY; l++)
                {
                    if (Framing.GetTileSafely(k, l).type == TileID.StoneSlab)
                    {
                        WorldGen.KillTile(k, l);
                    }
                }
            }
            EEWorld.EEWorld.MakeOvalJaggedTop(80, 60, new Vector2(160, 170), ModContent.TileType<MagmastoneTile>());
            EEWorld.EEWorld.KillWall(Main.maxTilesX, Main.maxTilesY, Vector2.Zero);
            EEWorld.EEWorld.FillWall(Main.maxTilesX, Main.maxTilesY, Vector2.Zero, ModContent.WallType<MagmastoneWallTile>());
        }
    }
}