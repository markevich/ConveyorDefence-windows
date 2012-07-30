using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

namespace Conveyor_Defence
{
    class TileMap
    {
        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth = 20;
        public int MapHeight = 35;
        private const float heightRowDepthMod = 0.0000001f;
        public int BaseOffsetX = -32;
        public int BaseOffsetY = -64;

        private readonly Texture2D mouseMap;
        private readonly SpriteFont tileIndexer; //for debug tiles.
        private Texture2D tileHighligter;

        private float maxdepth;
        private readonly Random rnd;

        public TileMap(Texture2D mouseMap, SpriteFont tileIndexer,  Texture2D tileHighligter)
        {
            rnd = new Random();
            this.mouseMap = mouseMap;
            this.tileIndexer = tileIndexer;
            this.tileHighligter = tileHighligter;
            maxdepth = ((MapWidth + 1) + ((MapHeight + 1) * Tile.TileWidth)) * 10;
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Columns.Add(new MapCell(rnd.Next(0,7)));
                }
                Rows.Add(thisRow);
            }

            // Create Sample Map Data

            Rows[3].Columns[2].AddTopperTile(10);
            Rows[4].Columns[2].AddTopperTile(10);
            Rows[5].Columns[1].AddTopperTile(10);

            Rows[3].Columns[2].TileID = 3;
            Rows[3].Columns[3].TileID = 1;
            Rows[3].Columns[4].TileID = 1;
            Rows[3].Columns[5].TileID = 2;
            Rows[3].Columns[6].TileID = 2;
            Rows[3].Columns[7].TileID = 2;

            Rows[4].Columns[2].TileID = 3;
            Rows[4].Columns[3].TileID = 1;
            Rows[4].Columns[4].TileID = 1;
            Rows[4].Columns[5].TileID = 2;
            Rows[4].Columns[6].TileID = 2;
            Rows[4].Columns[7].TileID = 2;

            Rows[5].Columns[2].TileID = 3;
            Rows[5].Columns[3].TileID = 1;
            Rows[5].Columns[4].TileID = 1;
            Rows[5].Columns[5].TileID = 2;
            Rows[5].Columns[6].TileID = 2;
            Rows[5].Columns[7].TileID = 2;

            Rows[3].Columns[5].AddBaseTile(30);
            Rows[4].Columns[5].AddBaseTile(27);
            Rows[5].Columns[5].AddBaseTile(28);

            Rows[3].Columns[6].AddBaseTile(25);
            Rows[5].Columns[6].AddBaseTile(24);

            Rows[3].Columns[7].AddBaseTile(31);
            Rows[4].Columns[7].AddBaseTile(26);
            Rows[5].Columns[7].AddBaseTile(29);

            Rows[4].Columns[6].AddBaseTile(104);

            Rows[16].Columns[4].AddHeightTile(54);

            Rows[17].Columns[3].AddHeightTile(54);

            Rows[15].Columns[3].AddHeightTile(54);
            Rows[16].Columns[3].AddHeightTile(53);

            Rows[15].Columns[4].AddHeightTile(54);
            Rows[15].Columns[4].AddHeightTile(54);
            Rows[15].Columns[4].AddHeightTile(51);

            Rows[18].Columns[3].AddHeightTile(51);
            Rows[19].Columns[3].AddHeightTile(50);
            Rows[18].Columns[4].AddHeightTile(55);

            Rows[14].Columns[4].AddHeightTile(54);

            Rows[14].Columns[5].AddHeightTile(62);
            Rows[14].Columns[5].AddHeightTile(61);
            Rows[14].Columns[5].AddHeightTile(63);

            Rows[17].Columns[4].AddTopperTile(114);
            Rows[16].Columns[5].AddTopperTile(115);
            Rows[14].Columns[4].AddTopperTile(125);
            Rows[15].Columns[5].AddTopperTile(91);
            Rows[16].Columns[6].AddTopperTile(94);

            Rows[15].Columns[5].Walkable = false;
            Rows[16].Columns[6].Walkable = false;

            Rows[12].Columns[9].AddHeightTile(34);
            Rows[11].Columns[9].AddHeightTile(34);
            Rows[11].Columns[8].AddHeightTile(34);
            Rows[10].Columns[9].AddHeightTile(34);

            Rows[12].Columns[8].AddTopperTile(31);
            Rows[12].Columns[8].SlopeMap = 0;
            Rows[13].Columns[8].AddTopperTile(31);
            Rows[13].Columns[8].SlopeMap = 0;

            Rows[12].Columns[10].AddTopperTile(32);
            Rows[12].Columns[10].SlopeMap = 1;
            Rows[13].Columns[9].AddTopperTile(32);
            Rows[13].Columns[9].SlopeMap = 1;

            Rows[14].Columns[9].AddTopperTile(30);
            Rows[14].Columns[9].SlopeMap = 4;
            // End Create Sample Map Data
        }

        public Point WorldToMapCell(Point worldPoint)
        {
            Point dummy;
            return WorldToMapCell(worldPoint, out dummy);
        }

        public Point WorldToMapCell(Point worldPoint, out Point localPoint)
        {
            Point mapCell = new Point(
               (int)(worldPoint.X / mouseMap.Width),
               ((int)(worldPoint.Y / mouseMap.Height)) * 2
               );

            int localPointX = worldPoint.X % mouseMap.Width;
            int localPointY = worldPoint.Y % mouseMap.Height;

            int dx = 0;
            int dy = 0;

            uint[] myUint = new uint[1];

            if (new Rectangle(0, 0, mouseMap.Width, mouseMap.Height).Contains(localPointX, localPointY))
            {
                mouseMap.GetData(0, new Rectangle(localPointX, localPointY, 1, 1), myUint, 0, 1);

                if (myUint[0] == 0xFF0000FF) // Red
                {
                    dx = -1;
                    dy = -1;
                    localPointX = localPointX + (mouseMap.Width / 2);
                    localPointY = localPointY + (mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFF00FF00) // Green
                {
                    dx = -1;
                    localPointX = localPointX + (mouseMap.Width / 2);
                    dy = 1;
                    localPointY = localPointY - (mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFF00FFFF) // Yellow
                {
                    dy = -1;
                    localPointX = localPointX - (mouseMap.Width / 2);
                    localPointY = localPointY + (mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFFFF0000) // Blue
                {
                    dy = +1;
                    localPointX = localPointX - (mouseMap.Width / 2);
                    localPointY = localPointY - (mouseMap.Height / 2);
                }
            }

            mapCell.X += dx;
            mapCell.Y += dy - 2;

            localPoint = new Point(localPointX, localPointY);

            return mapCell;
        }

        public Point WorldToMapCell(Vector2 worldPoint)
        {
            return WorldToMapCell(new Point((int)worldPoint.X, (int)worldPoint.Y));
        }

        public MapCell GetCellAtWorldPoint(Point worldPoint)
        {
            Point mapPoint = WorldToMapCell(worldPoint);
            return Rows[mapPoint.Y].Columns[mapPoint.X];
        }

        public MapCell GetCellAtWorldPoint(Vector2 worldPoint)
        {
            return GetCellAtWorldPoint(new Point((int)worldPoint.X, (int)worldPoint.Y));
        }

        public void Draw(SpriteBatch batch)
        {
            var firstVisibleTile = FirstVisibleTile;

            var firstX = (int)firstVisibleTile.X;
            var firstY = (int)firstVisibleTile.Y;
            float depthOffsetY = 0.9f;
            for (int y = 0; y < MapHeight; y++)
            {
                depthOffsetY -= heightRowDepthMod;

                for (int x = 0; x < MapWidth; x++)
                {
                    var tileIndex = new Vector2(firstX + x, firstY + y);
                    float depthOffset = 0.7f - ((tileIndex.X + (tileIndex.Y * Tile.TileWidth)) / maxdepth);
                    if (IsTileOutsideOfMap(tileIndex)) continue;
                        
                    var tile = Rows[(int)tileIndex.Y].Columns[(int)tileIndex.X];

                    tile.Draw(batch, tileIndex, depthOffset, heightRowDepthMod, depthOffsetY);



                    DrawTileIndexes(batch, tileIndex, x, y); //helper method
                }
            }
            DrawTileHighLight(batch);
        }

        private void DrawTileIndexes(SpriteBatch batch, Vector2 tileIndex, int x, int y)
        {
            var tileoffset = new Vector2(Camera.Location.X%Tile.TileStepX, Camera.Location.Y%Tile.TileStepY);
            var offsetx = (int) tileoffset.X;
            var offsety = (int) tileoffset.Y;
            var rowoffset = (int)tileIndex.Y % 2 == 1 ? Tile.OddRowXOffset : 0;
            batch.DrawString(tileIndexer, (tileIndex.X).ToString() + ", " + (tileIndex.Y).ToString(),
                             new Vector2((x*Tile.TileStepX) - offsetx + rowoffset + BaseOffsetX + 24,
                                         (y*Tile.TileStepY) - offsety + BaseOffsetY + 48), Color.White, 0f,
                             Vector2.Zero,
                             1.0f, SpriteEffects.None, 0.0f);
        }

        private void DrawTileHighLight(SpriteBatch batch)
        {
            Vector2 hilightLoc = Camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            Point hilightPoint = WorldToMapCell(new Point((int)hilightLoc.X, (int)hilightLoc.Y));

            int hilightrowOffset = 0;
            if ((hilightPoint.Y) % 2 == 1)
                hilightrowOffset = Tile.OddRowXOffset;

            batch.Draw(
                            tileHighligter,
                            Camera.WorldToScreen(

                                new Vector2(

                                    (hilightPoint.X * Tile.TileStepX) + hilightrowOffset,

                                    (hilightPoint.Y + 2) * Tile.TileStepY)),
                            new Rectangle(0, 0, 64, 32),
                            Color.White * 0.3f,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            0.0f);
        }
        private bool IsTileOutsideOfMap(Vector2 tileIndex)
        {
            return (tileIndex.X >= MapWidth) || (tileIndex.Y >= MapHeight);
        }

        private Vector2 FirstVisibleTile
        {
            get { return new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY); }
        }

        

    }
}
