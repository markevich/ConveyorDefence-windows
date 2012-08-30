using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using Conveyor_Defence.Nodes;

namespace Conveyor_Defence.Map
{
    class TileMap
    {
        private readonly List<MapRow> _rows = new List<MapRow>();
        public const int MapWidth = 30;
        public const int MapHeight = 45;
        public const int BaseOffsetX = -32;
        public const int BaseOffsetY = -64;
        private readonly Texture2D _mouseMap;
        private readonly SpriteFont _tileIndexer; //for debug tiles.
        private readonly Texture2D _tileHighligter;

        private readonly float _maxdepth;
        private readonly Random _rnd;

        public TileMap(Texture2D mouseMap, SpriteFont tileIndexer,  Texture2D tileHighligter)
        {
            _rnd = new Random();
            _mouseMap = mouseMap;
            _tileIndexer = tileIndexer;
            _tileHighligter = tileHighligter;
            _maxdepth = ((MapWidth + 1) + ((MapHeight + 1) * Tile.TileWidth)) * 10;
            for (int y = 0; y < MapHeight; y++)
            {
                var thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Columns.Add(new MapCell(_rnd.Next(0,7)));
                }
                _rows.Add(thisRow);
            }

            // Create Sample Map Data

            _rows[3].Columns[2 + 7].TileID = 3;
            _rows[3].Columns[3 + 7].TileID = 1;
            _rows[3].Columns[4 + 7].TileID = 1;
            _rows[3].Columns[5 + 7].TileID = 2;
            _rows[3].Columns[6 + 7].TileID = 2;
            _rows[3].Columns[7 + 7].TileID = 2;

            _rows[4].Columns[2 + 7].TileID = 3;
            _rows[4].Columns[3 + 7].TileID = 1;
            _rows[4].Columns[4 + 7].TileID = 1;
            _rows[4].Columns[5 + 7].TileID = 2;
            _rows[4].Columns[6 + 7].TileID = 2;
            _rows[4].Columns[7 + 7].TileID = 2;

            _rows[5].Columns[2 + 7].TileID = 3;
            _rows[5].Columns[3 + 7].TileID = 1;
            _rows[5].Columns[4 + 7].TileID = 1;
            _rows[5].Columns[5 + 7].TileID = 2;
            _rows[5].Columns[6 + 7].TileID = 2;
            _rows[5].Columns[7 + 7].TileID = 2;

            _rows[4].Columns[6 + 7].AddBaseTile(104);

            _rows[16].Columns[4 + 7].AddHeightTile(54);

            _rows[17].Columns[3 + 7].AddHeightTile(54);

            _rows[15].Columns[3 + 7].AddHeightTile(54);
            _rows[16].Columns[3 + 7].AddHeightTile(53);

            _rows[15].Columns[4 + 7].AddHeightTile(54);
            _rows[15].Columns[4 + 7].AddHeightTile(54);
            _rows[15].Columns[4 + 7].AddHeightTile(51);

            _rows[18].Columns[3 + 7].AddHeightTile(51);
            _rows[19].Columns[3 + 7].AddHeightTile(50);
            _rows[18].Columns[4 + 7].AddHeightTile(55);

            _rows[14].Columns[4 + 7].AddHeightTile(54);

            _rows[14].Columns[5 + 7].AddHeightTile(62);
            _rows[14].Columns[5 + 7].AddHeightTile(61);
            _rows[14].Columns[5 + 7].AddHeightTile(63);

            _rows[17].Columns[4 + 7].AddTopperTile(114);
            _rows[16].Columns[5 + 7].AddTopperTile(115);
            _rows[14].Columns[4 + 7].AddTopperTile(125);
            _rows[15].Columns[5 + 7].AddTopperTile(91);
            _rows[16].Columns[6 + 7].AddTopperTile(94);

            // End Create Sample Map Data
        }

        public Point WorldToMapCell(Point worldPoint)
        {
            Point dummy;
            return WorldToMapCell(worldPoint, out dummy);
        }

        public Point WorldToMapCell(Point worldPoint, out Point localPoint)
        {
            var mapCell = new Point(
               worldPoint.X / _mouseMap.Width,
               worldPoint.Y / _mouseMap.Height * 2
               );

            int localPointX = worldPoint.X % _mouseMap.Width;
            int localPointY = worldPoint.Y % _mouseMap.Height;

            int dx = 0;
            int dy = 0;

            var myUint = new uint[1];

            if (new Rectangle(0, 0, _mouseMap.Width, _mouseMap.Height).Contains(localPointX, localPointY))
            {
                _mouseMap.GetData(0, new Rectangle(localPointX, localPointY, 1, 1), myUint, 0, 1);

                if (myUint[0] == 0xFF0000FF) // Red
                {
                    dx = -1;
                    dy = -1;
                    localPointX = localPointX + (_mouseMap.Width / 2);
                    localPointY = localPointY + (_mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFF00FF00) // Green
                {
                    dx = -1;
                    localPointX = localPointX + (_mouseMap.Width / 2);
                    dy = 1;
                    localPointY = localPointY - (_mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFF00FFFF) // Yellow
                {
                    dy = -1;
                    localPointX = localPointX - (_mouseMap.Width / 2);
                    localPointY = localPointY + (_mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFFFF0000) // Blue
                {
                    dy = +1;
                    localPointX = localPointX - (_mouseMap.Width / 2);
                    localPointY = localPointY - (_mouseMap.Height / 2);
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
            return _rows[mapPoint.Y].Columns[mapPoint.X];
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
                depthOffsetY -= Tile.DepthModifier;

                for (int x = 0; x < MapWidth; x++)
                {
                    var tileIndex = new Point(firstX + x, firstY + y);
                    float depthOffset = 0.7f - ((tileIndex.X + (tileIndex.Y * Tile.TileWidth)) / _maxdepth);
                    if (IsTileOutsideOfMap(tileIndex)) continue;
                        
                    var cell = _rows[tileIndex.Y].Columns[tileIndex.X];

                    cell.Draw(batch, tileIndex, depthOffset, depthOffsetY);

                    DrawTileIndexes(batch, tileIndex, x, y); //helper method
                }
            }
            DrawTileHighLight(batch);
        }

        private void DrawTileIndexes(SpriteBatch batch, Point tileIndex, int x, int y)
        {
            var tileoffset = new Vector2(Camera.Location.X%Tile.TileStepX, Camera.Location.Y%Tile.TileStepY);
            var offsetx = (int) tileoffset.X;
            var offsety = (int) tileoffset.Y;
            var rowoffset = (int)tileIndex.Y % 2 == 1 ? Tile.OddRowXOffset : 0;
            var index = String.Format("{0},{1}", tileIndex.X, tileIndex.Y);
            batch.DrawString(_tileIndexer, index,
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
                            _tileHighligter,
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
        private bool IsTileOutsideOfMap(Point tileIndex)
        {
            return (tileIndex.X >= MapWidth) || (tileIndex.Y >= MapHeight);
        }

        private Vector2 FirstVisibleTile
        {
            get { return new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY); }
        }

        

    }
}
