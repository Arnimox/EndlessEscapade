﻿using EEMod.ID;
using EEMod.Tiles;
using EEMod.Tiles.Furniture;
using EEMod.Tiles.Furniture.Coral;
using EEMod.Tiles.Ores;
using EEMod.Tiles.Walls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using EEMod.Tiles.Furniture.Coral.HangingCoral;
using EEMod.Tiles.Furniture.Coral.WallCoral;

namespace EEMod.EEWorld
{
    public partial class EEWorld
    {
        public static void MakeKramkenArena(int xPos, int yPos, int size)
        {
            int maxTiles = (int)(Main.maxTilesX * Main.maxTilesY * 9E-04);
            for (int k = 0; k < maxTiles * 60; k++)
            {
                int x = WorldGen.genRand.Next(xPos - (size * 2), xPos + (size * 2));
                int y = WorldGen.genRand.Next(yPos - (size * 2), yPos + (size * 2));
                if (OvalCheck(xPos, yPos, x, y, size * 2, size))
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(10, 20), WorldGen.genRand.Next(5, 10), TileID.StoneSlab, true, 0f, 0f, true, true);
                }
            }
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.type == TileID.StoneSlab)
                    {
                        WorldGen.KillTile(i, j);
                    }
                }
            }
        }

        public static PerlinNoiseFunction perlinNoise;

        public static void PlaceKelp(int height, Vector2 startingPoint)
        {
            for (int i = 0; i < height; i++)
            {
                Tile tile = Framing.GetTileSafely((int)startingPoint.X, (int)startingPoint.Y - i);
                if (!tile.active())
                {
                    tile.type = (ushort)ModContent.TileType<BlueKelpTile>();
                }

                tile.active(true);
            }
        }

        public static void MakeCoralRoom(int xPos, int yPos, int size, int type, int minibiome, bool ensureNoise = false)
        {
            int sizeX = size;
            int sizeY = size / 2;
            Vector2 TL = new Vector2(xPos - sizeX / 2f, yPos - sizeY / 2f);
            Vector2 BR = new Vector2(xPos + sizeX / 2f, yPos + sizeY / 2f);

            Vector2 startingPoint = new Vector2(xPos - sizeX, yPos - sizeY);
            int tile2;
            if (TL.Y < Main.maxTilesY * 0.33f)
            {
                tile2 = (ushort)ModContent.TileType<LightGemsandTile>();
            }
            else if (TL.Y < Main.maxTilesY * 0.66f)
            {
                tile2 = (ushort)ModContent.TileType<GemsandTile>();
            }
            else
            {
                tile2 = (ushort)ModContent.TileType<DarkGemsandTile>();
            }
            if (TL.Y < Main.maxTilesY / 10)
            {
                tile2 = (ushort)ModContent.TileType<CoralSandTile>();
            }
            void CreateNoise(bool ensureN, int width, int height, float thresh)
            {
                perlinNoise = new PerlinNoiseFunction(1000, 1000, width, height, thresh);
                int[,] perlinNoiseFunction = perlinNoise.perlinBinary;
                if (ensureN)
                {
                    for (int i = (int)startingPoint.X; i < (int)startingPoint.X + sizeX * 2; i++)
                    {
                        for (int j = (int)startingPoint.Y; j < (int)startingPoint.Y + sizeY * 2; j++)
                        {
                            if (perlinNoiseFunction[i - (int)startingPoint.X, j - (int)startingPoint.Y] == 1 && OvalCheck(xPos, yPos, i, j, sizeX, sizeY) && WorldGen.InWorld(i, j))
                            {
                                Tile tile = Framing.GetTileSafely(i, j);
                                if (j < Main.maxTilesY * 0.33f)
                                {
                                    tile.type = (ushort)ModContent.TileType<LightGemsandTile>();
                                }
                                else if (j < Main.maxTilesY * 0.66f)
                                {
                                    tile.type = (ushort)ModContent.TileType<GemsandTile>();
                                }
                                else if (j > Main.maxTilesY * 0.66f)
                                {
                                    tile.type = (ushort)ModContent.TileType<DarkGemsandTile>();
                                }

                                if (j < Main.maxTilesY / 10)
                                {
                                    tile.type = (ushort)ModContent.TileType<CoralSandTile>();
                                }
                            }
                        }
                    }
                }
            }
            RemoveStoneSlabs();

            switch (type) //Creating the formation of the room(the shape)
            {
                case -1:
                    MakeJaggedOval(sizeX, sizeY, new Vector2(TL.X, TL.Y), TileID.StoneSlab, true);
                    MakeOvalFlatTop(sizeX / 3, sizeY / 3, new Vector2(xPos + 0, yPos + 0), tile2);
                    MakeOvalFlatTop(sizeX / 3, sizeY / 3, new Vector2(xPos + (-sizeX / 5 - sizeX / 6), yPos + (-sizeY / 5 - sizeY / 6)), tile2);
                    MakeOvalFlatTop(sizeX / 3, sizeY / 3, new Vector2(xPos + (sizeX / 5 - sizeX / 6), yPos + (-sizeY / 5 - sizeY / 6)), tile2);
                    MakeOvalFlatTop(sizeX / 3, sizeY / 3, new Vector2(xPos + (sizeX / 5 - sizeX / 6), yPos + (sizeY / 5 - sizeY / 6)), tile2);
                    MakeOvalFlatTop(sizeX / 3, sizeY / 3, new Vector2(xPos + (-sizeX / 5 - sizeX / 6), yPos + (sizeY / 5 - sizeY / 6)), tile2);
                    break;

                case 0:
                    MakeJaggedOval(sizeX, sizeY, new Vector2(TL.X, TL.Y), TileID.StoneSlab, true);
                    MakeOvalFlatTop(sizeX / 3, sizeY / 3, new Vector2(xPos + 0, yPos + 0), tile2);
                    MakeOvalFlatTop(sizeX / 3, sizeY / 3, new Vector2(xPos + (-sizeX / 5), yPos + (-sizeY / 5)), tile2);
                    MakeOvalFlatTop(sizeX / 3, sizeY / 3, new Vector2(xPos + (sizeX / 5), yPos + (-sizeY / 5)), tile2);
                    MakeOvalFlatTop(sizeX / 3, sizeY / 3, new Vector2(xPos + (sizeX / 5), yPos + (sizeY / 5)), tile2);
                    MakeOvalFlatTop(sizeX / 3, sizeY / 3, new Vector2(xPos + (-sizeX / 5), yPos + (sizeY / 5)), tile2);
                    CreateNoise(!ensureNoise, 100, 10, 0.45f);
                    break;

                case 1:
                    MakeJaggedOval(sizeX, sizeY, new Vector2(TL.X, TL.Y), TileID.StoneSlab, true);
                    CreateNoise(!ensureNoise, 20, 200, 0.5f);
                    break;

                case 2:
                    MakeJaggedOval(sizeX, sizeY, new Vector2(TL.X, TL.Y), TileID.StoneSlab, true);
                    CreateNoise(!ensureNoise, 200, 20, 0.5f);
                    break;

                case 3:
                    MakeJaggedOval(sizeX, sizeY, new Vector2(TL.X, TL.Y), TileID.StoneSlab, true);
                    MakeWavyChasm3(new Vector2(TL.X - 50 + WorldGen.genRand.Next(-30, 30), TL.Y - 10), new Vector2(BR.X - 50 + WorldGen.genRand.Next(-30, 30), BR.Y - 10), tile2, 100, 4, true, new Vector2(10, 13), 50, 20);
                    MakeWavyChasm3(new Vector2(TL.X + 50 + WorldGen.genRand.Next(-30, 30), yPos + 10), new Vector2(BR.X + 50 + WorldGen.genRand.Next(-30, 30), yPos + 10), tile2, 100, 4, true, new Vector2(10, 13), 50, 20);
                    MakeWavyChasm3(new Vector2(TL.X + WorldGen.genRand.Next(-30, 30), TL.Y - 10), new Vector2(BR.X + WorldGen.genRand.Next(-30, 30), BR.Y - 10), tile2, 100, 4, true, new Vector2(10, 13), 50, 20);
                    MakeWavyChasm3(new Vector2(TL.X + WorldGen.genRand.Next(-100, 100), TL.Y - 10), new Vector2(BR.X + WorldGen.genRand.Next(-30, 30), BR.Y - 10), tile2, 100, 4, true, new Vector2(10, 13), 50, 20);
                    MakeWavyChasm3(new Vector2(TL.X + WorldGen.genRand.Next(-100, 100), yPos - 10), new Vector2(BR.X + WorldGen.genRand.Next(-30, 30), BR.Y - 10), tile2, 100, 4, true, new Vector2(10, 13), 50, 20);
                    break;

                case 4:
                    MakeJaggedOval(sizeX, sizeY, new Vector2(TL.X, TL.Y), TileID.StoneSlab, true);
                    for (int i = 0; i < 20; i++)
                    {
                        MakeCircle(WorldGen.genRand.Next(5, 20), new Vector2(TL.X + WorldGen.genRand.Next(sizeX), TL.Y + WorldGen.genRand.Next(sizeY)), tile2, true);
                    }

                    break;

                case 5:
                    MakeJaggedOval(sizeX, sizeY * 2, new Vector2(TL.X, yPos - sizeY), TileID.StoneSlab, true);
                    MakeJaggedOval((int)(sizeX * 0.8f), (int)(sizeY * 1.6f), new Vector2(xPos - sizeX * 0.4f, yPos - sizeY * 0.8f), tile2, true);
                    MakeJaggedOval(sizeX / 10, sizeY / 5, new Vector2(xPos - sizeX / 20, yPos - sizeY / 10), TileID.StoneSlab, true);
                    for (int i = 0; i < 30; i++)
                    {
                        MakeCircle(WorldGen.genRand.Next(5, 20), new Vector2(TL.X + WorldGen.genRand.Next(sizeX), yPos - sizeY + WorldGen.genRand.Next(sizeY * 2)), TileID.StoneSlab, true);
                    }

                    break;

                case 6:
                    MakeJaggedOval((int)(sizeX * 1.3f), sizeY, new Vector2(TL.X, TL.Y), TileID.StoneSlab, true);
                    MakeWavyChasm3(new Vector2(TL.X - 50 + WorldGen.genRand.Next(-30, 30), TL.Y - 10), new Vector2(BR.X - 50 + WorldGen.genRand.Next(-30, 30), BR.Y - 10), tile2, 100, 4, true, new Vector2(10, 13), 50, 20);
                    MakeWavyChasm3(new Vector2(TL.X + 50 + WorldGen.genRand.Next(-30, 30), yPos + 10), new Vector2(BR.X + 50 + WorldGen.genRand.Next(-30, 30), yPos + 10), tile2, 100, 4, true, new Vector2(10, 13), 50, 20);
                    MakeWavyChasm3(new Vector2(TL.X + WorldGen.genRand.Next(-30, 30), TL.Y - 10), new Vector2(BR.X + WorldGen.genRand.Next(-30, 30), BR.Y - 10), tile2, 100, 4, true, new Vector2(10, 13), 50, 20);
                    MakeWavyChasm3(new Vector2(TL.X + WorldGen.genRand.Next(-100, 100), TL.Y - 10), new Vector2(BR.X + WorldGen.genRand.Next(-30, 30), BR.Y - 10), tile2, 100, 4, true, new Vector2(10, 13), 50, 20);
                    MakeWavyChasm3(new Vector2(TL.X + WorldGen.genRand.Next(-100, 100), yPos - 10), new Vector2(BR.X + WorldGen.genRand.Next(-30, 30), BR.Y - 10), tile2, 100, 4, true, new Vector2(10, 13), 50, 20);
                    CreateNoise(true, 100, 20, 0.2f);
                    break;
            }

            CreateNoise(ensureNoise, 50, 20, 0.5f);

            for (int i = -20; i < sizeX + 20; i++)
            {
                for (int j = -20; j < sizeY + 20; j++)
                {
                    Vector2 basePos = new Vector2(i + xPos - size / 2f, j + yPos - size / 4f);
                    if (WorldGen.InWorld((int)basePos.X, (int)basePos.Y, 20))
                    {
                        switch (minibiome)
                        {
                            case 0: //Default
                                if (!WorldGen.genRand.NextBool(6))
                                {
                                    int selection;
                                    switch (TileCheck2((int)basePos.X, (int)basePos.Y))
                                    {
                                        case 1:
                                            selection = WorldGen.genRand.Next(4);
                                            switch (selection)
                                            {
                                                case 0:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging1x2Coral>());
                                                    break;

                                                case 1:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging1x3Coral>());
                                                    break;

                                                case 2:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging2x3Coral>());
                                                    break;

                                                case 3:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging2x4Coral>());
                                                    break;
                                            }
                                            break;
                                        case 2:
                                        {
                                            selection = WorldGen.genRand.Next(14);
                                            switch (selection)
                                            {
                                                case 0:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 8, ModContent.TileType<Floor6x8Coral>());
                                                    break;

                                                case 1:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 8, ModContent.TileType<Floor8x8Coral>());
                                                    break;

                                                case 2:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 3, ModContent.TileType<Floor3x3Coral>(), style: WorldGen.genRand.Next(2));
                                                    break;

                                                case 3:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 2, ModContent.TileType<Floor1x2Coral>(), style: WorldGen.genRand.Next(3));
                                                    break;

                                                case 4:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 1, ModContent.TileType<Floor1x1Coral>(), style: WorldGen.genRand.Next(3));
                                                    break;

                                                case 5:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 2, ModContent.TileType<Floor2x2Coral>(), style: WorldGen.genRand.Next(5));
                                                    break;

                                                case 6:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 7, ModContent.TileType<Floor7x7Coral>());
                                                    break;

                                                case 7:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 7, ModContent.TileType<Floor6x8Coral>());
                                                    break;

                                                case 8:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 6, ModContent.TileType<Floor2x6Coral>());
                                                    break;

                                                case 9:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 3, ModContent.TileType<Floor8x3Coral>());
                                                    break;

                                                case 11:
                                                    PlaceKelp(WorldGen.genRand.Next(sizeY / 30, sizeY / 20), new Vector2((int)basePos.X, (int)basePos.Y - 1));
                                                    break;

                                                case 12:
                                                    switch (WorldGen.genRand.Next(3))
                                                    {
                                                        case 0:
                                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 2, ModContent.TileType<GlowCoral1>());
                                                            break;

                                                        case 1:
                                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 2, ModContent.TileType<GlowCoral2>());
                                                            break;

                                                        case 2:
                                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 2, ModContent.TileType<GlowCoral3>());
                                                            break;
                                                    }
                                                    break;

                                                case 13:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 3, ModContent.TileType<ThermalVent>());
                                                    break;

                                                case 14:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 1, ModContent.TileType<Floor2x1Coral>(), style: WorldGen.genRand.Next(4));
                                                    break;
                                                case 15:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 1, ModContent.TileType<Floor4x3Coral>());
                                                    break;
                                                case 16:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 1, ModContent.TileType<Floor5x3Coral>());
                                                    break;
                                            }
                                            break;
                                        }
                                        case 3:
                                            selection = WorldGen.genRand.Next(4);
                                            switch (selection)
                                            {
                                                case 0:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y, ModContent.TileType<Wall2x2CoralL>());
                                                    break;

                                                case 1:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y, ModContent.TileType<Wall3x2CoralL>());
                                                    break;

                                                case 2:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y, ModContent.TileType<Wall4x2CoralL>());
                                                    break;

                                                case 3:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y, ModContent.TileType<Wall4x3CoralL>());
                                                    break;
                                            }
                                            break;
                                        case 4:
                                            selection = WorldGen.genRand.Next(4);
                                            switch (selection)
                                            {
                                                case 0:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y, ModContent.TileType<Wall2x2CoralR>());
                                                    break;

                                                case 1:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y, ModContent.TileType<Wall3x2CoralR>());
                                                    break;

                                                case 2:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y, ModContent.TileType<Wall4x2CoralR>());
                                                    break;

                                                case 3:
                                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y, ModContent.TileType<Wall4x3CoralR>());
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case 1: //Kelp Forest
                                if (TileCheck2((int)basePos.X, (int)basePos.Y) == 2 && !WorldGen.genRand.NextBool(6))
                                {
                                    if (WorldGen.genRand.NextBool())
                                    {
                                        PlaceKelp(WorldGen.genRand.Next(sizeY / 9, sizeY / 2), new Vector2((int)basePos.X, (int)basePos.Y - 1));
                                    }
                                    else
                                    {
                                        int selection = WorldGen.genRand.Next(4);
                                        switch (selection)
                                        {
                                            case 0:
                                                WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 2, ModContent.TileType<Floor1x2Coral>(), style: WorldGen.genRand.Next(0, 3));
                                                break;

                                            case 1:
                                                WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 1, ModContent.TileType<Floor1x1Coral>(), style: WorldGen.genRand.Next(0, 3));
                                                break;
                                            case 2:
                                                if (TileCheck2((int)basePos.X, (int)basePos.Y) == 2 && TileCheck2((int)basePos.X + 1, (int)basePos.Y) == 2 && TileCheck2((int)basePos.X + 2, (int)basePos.Y) == 2)
                                                {
                                                    ModContent.GetInstance<GroundGlowCoralTE>().Place(i, j - 13);
                                                    WorldGen.PlaceTile(i, j - 13, ModContent.TileType<GroundGlowCoral>());
                                                }
                                                else
                                                {
                                                    ModContent.GetInstance<GroundGlowCoral3TE>().Place(i, j - 4);
                                                    WorldGen.PlaceTile(i, j - 13, ModContent.TileType<GroundGlowCoral3>());
                                                }
                                                break;
                                            case 3:
                                                if (TileCheck2((int)basePos.X, (int)basePos.Y) == 2 && TileCheck2((int)basePos.X + 1, (int)basePos.Y) == 2 && TileCheck2((int)basePos.X + 2, (int)basePos.Y) == 2)
                                                {
                                                    ModContent.GetInstance<GroundGlowCoral2TE>().Place(i, j - 5);
                                                    WorldGen.PlaceTile(i, j - 13, ModContent.TileType<GroundGlowCoral2>());
                                                }
                                                else
                                                {
                                                    ModContent.GetInstance<GroundGlowCoral3TE>().Place(i, j - 4);
                                                    WorldGen.PlaceTile(i, j - 13, ModContent.TileType<GroundGlowCoral3>());
                                                }
                                                break;
                                        }
                                    }
                                }
                                if (TileCheck2((int)basePos.X, (int)basePos.Y) == 1 && !WorldGen.genRand.NextBool(6))
                                {
                                    ModContent.GetInstance<GlowHangCoral1TE>().Place((int)basePos.X, (int)basePos.Y + 1);
                                    WorldGen.PlaceTile(i, j + 1, ModContent.TileType<GlowHangCoral1>());
                                }
                                break;
                            case 2: //Polyp Zone/Anemone
                                if (TileCheck2((int)basePos.X, (int)basePos.Y) == 2 && !WorldGen.genRand.NextBool(6))
                                {
                                    if (WorldGen.genRand.NextBool())
                                    {
                                        PlaceKelp(WorldGen.genRand.Next(sizeY / 10, sizeY / 3), new Vector2((int)basePos.X, (int)basePos.Y - 1));
                                    }
                                    else
                                    {
                                        int selection = WorldGen.genRand.Next(2);
                                        switch (selection)
                                        {
                                            case 0:
                                                WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 2, ModContent.TileType<Floor1x2Coral>(), style: WorldGen.genRand.Next(3));
                                                break;

                                            case 1:
                                                WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 1, ModContent.TileType<Floor1x1Coral>(), style: WorldGen.genRand.Next(3));
                                                break;
                                        }
                                    }
                                }
                                break;
                            case 3: //Jellyfish Caverns
                                if (TileCheck2((int)basePos.X, (int)basePos.Y) == 1 && !WorldGen.genRand.NextBool(5))
                                {
                                    int selection = WorldGen.genRand.Next(4);
                                    switch (selection)
                                    {
                                        case 0:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging1x2Coral>());
                                            break;

                                        case 1:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging1x3Coral>());
                                            break;

                                        case 2:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging2x3Coral>());
                                            break;

                                        case 3:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging2x4Coral>());
                                            break;
                                    }
                                }
                                if (TileCheck2((int)basePos.X, (int)basePos.Y) == 2 && !WorldGen.genRand.NextBool(6))
                                {
                                    int selection = WorldGen.genRand.Next(13);
                                    switch (selection)
                                    {
                                        case 0:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 8, ModContent.TileType<Floor6x8Coral>());
                                            break;
                                    }
                                }
                                break;
                            case 4: //Bulbous Grove
                                if (TileCheck2((int)basePos.X, (int)basePos.Y) == 2 && WorldGen.genRand.NextBool() && !WorldGen.genRand.NextBool(6))
                                {
                                    int selection = WorldGen.genRand.Next(5);
                                    switch (selection)
                                    {
                                        case 0:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 2, ModContent.TileType<Floor2x2Coral>(), style: 3);
                                            break;

                                        case 1:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 2, ModContent.TileType<GlowCoral1>());
                                            break;

                                        case 2:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 2, ModContent.TileType<GlowCoral2>());
                                            break;

                                        case 3:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 1, ModContent.TileType<Floor1x1Coral>(), style: 0);
                                            break;

                                        case 4:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 1, ModContent.TileType<Floor8x3Coral>(), style: Main.rand.Next(2));
                                            break;
                                    }
                                }
                                break;

                            case 5: //Thermal Vents
                                if (TileCheck2((int)basePos.X, (int)basePos.Y) == 1 && !WorldGen.genRand.NextBool(5))
                                {
                                    int selection = WorldGen.genRand.Next(4);
                                    switch (selection)
                                    {
                                        case 0:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging1x2Coral>());
                                            break;

                                        case 1:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging1x3Coral>());
                                            break;

                                        case 2:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging2x3Coral>());
                                            break;

                                        case 3:
                                            WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y + 1, ModContent.TileType<Hanging2x4Coral>());
                                            break;
                                    }
                                }
                                if (TileCheck2((int)basePos.X, (int)basePos.Y) == 2 && !WorldGen.genRand.NextBool(5))
                                {
                                    WorldGen.PlaceTile((int)basePos.X, (int)basePos.Y - 3, ModContent.TileType<ThermalVent>());
                                }
                                break;

                            case 6: //Subterranean Waters
                                break;
                        }
                    }
                }
            }
        }

        public static void PlaceCoral()
        {
            for (int i = 42; i < Main.maxTilesX - 42; i++)
            {
                for (int j = 42; j < Main.maxTilesY - 42; j++)
                {
                    if (j < Main.maxTilesY / 10)
                    {
                        if (TileCheck2(i, j) == 2 && WorldGen.InWorld(i, j) && !Main.rand.NextBool(3))
                        {
                            int selection = WorldGen.genRand.Next(7);
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

                                case 3:
                                    WorldGen.PlaceTile(i, j - 3, ModContent.TileType<Floor3x3Coral>());
                                    break;

                                case 4:
                                    WorldGen.PlaceTile(i, j - 2, ModContent.TileType<Floor1x2Coral>(), style: WorldGen.genRand.Next(3));
                                    break;

                                case 5:
                                    WorldGen.PlaceTile(i, j - 1, ModContent.TileType<Floor1x1Coral>(), style: WorldGen.genRand.Next(3));
                                    break;

                                case 6:
                                    WorldGen.PlaceTile(i, j - 2, ModContent.TileType<Floor2x2Coral>(), style: WorldGen.genRand.Next(5));
                                    break;
                            }
                        }
                    }
                    else
                    {

                        if (TileCheck2(i, j) == 1 && !WorldGen.genRand.NextBool(3) && WorldGen.InWorld(i, j))
                        {
                            int selection = WorldGen.genRand.Next(6);
                            switch (selection)
                            {
                                case 0:
                                    WorldGen.PlaceTile(i, j, ModContent.TileType<Hanging1x2Coral>());
                                    break;

                                case 1:
                                    WorldGen.PlaceTile(i, j, ModContent.TileType<Hanging1x3Coral>());
                                    break;             
                                                       
                                case 2:                
                                    WorldGen.PlaceTile(i, j, ModContent.TileType<Hanging2x3Coral>());
                                    break;             
                                                       
                                case 3:                
                                    WorldGen.PlaceTile(i, j, ModContent.TileType<Hanging2x4Coral>());
                                    break;
                            }
                        }
                        if (TileCheck2(i, j) == 2 && !WorldGen.genRand.NextBool(3) && WorldGen.InWorld(i, j))
                        {
                            int selection = WorldGen.genRand.Next(18);
                            switch (selection)
                            {
                                case 0:
                                    WorldGen.PlaceTile(i, j - 8, ModContent.TileType<Floor6x8Coral>());
                                    break;

                                case 1:
                                    WorldGen.PlaceTile(i, j - 8, ModContent.TileType<Floor8x8Coral>());
                                    break;

                                case 2:
                                    WorldGen.PlaceTile(i, j - 3, ModContent.TileType<Floor3x3Coral>(), style: WorldGen.genRand.Next(2));
                                    break;

                                case 3:
                                    WorldGen.PlaceTile(i, j - 2, ModContent.TileType<Floor1x2Coral>(), style: WorldGen.genRand.Next(3));
                                    break;

                                case 4:
                                    WorldGen.PlaceTile(i, j - 1, ModContent.TileType<Floor1x1Coral>(), style: WorldGen.genRand.Next(3));
                                    break;

                                case 5:
                                    WorldGen.PlaceTile(i, j - 2, ModContent.TileType<Floor2x2Coral>(), style: WorldGen.genRand.Next(5));
                                    break;

                                case 6:
                                    WorldGen.PlaceTile(i, j - 7, ModContent.TileType<Floor7x7Coral>());
                                    break;

                                case 7:
                                    WorldGen.PlaceTile(i, j - 7, ModContent.TileType<Floor8x7Coral>());
                                    break;

                                case 8:
                                    WorldGen.PlaceTile(i, j - 6, ModContent.TileType<Floor2x6Coral>());
                                    break;

                                case 9:
                                    WorldGen.PlaceTile(i, j - 3, ModContent.TileType<Floor8x3Coral>());
                                    break;

                                case 10:
                                    ModContent.GetInstance<GroundGlowCoral3TE>().Place(i, j - 4);
                                    WorldGen.PlaceTile(i, j - 4, ModContent.TileType<GroundGlowCoral3>());
                                    break;

                                case 11:
                                    PlaceKelp(WorldGen.genRand.Next(3, 9), new Vector2(i, j - 1));
                                    break;

                                case 12:
                                    WorldGen.PlaceTile(i, j - 2, ModContent.TileType<GlowCoral1>());
                                    break;

                                case 13:
                                    WorldGen.PlaceTile(i, j - 2, ModContent.TileType<GlowCoral2>());
                                    break;

                                case 14:
                                    WorldGen.PlaceTile(i, j - 2, ModContent.TileType<GlowCoral3>());
                                    break;

                                case 15:
                                    ModContent.GetInstance<GroundGlowCoralTE>().Place(i, j - 13);
                                    WorldGen.PlaceTile(i, j - 13, ModContent.TileType<GroundGlowCoral>());
                                    break;

                                case 16:
                                    ModContent.GetInstance<GroundGlowCoral2TE>().Place(i, j - 5);
                                    WorldGen.PlaceTile(i, j - 5, ModContent.TileType<GroundGlowCoral2>());
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}