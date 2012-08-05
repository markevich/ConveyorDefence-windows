using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conveyor_Defence.Map
{
    class MapCell
    {
        public int TileID
        {
            get { return _baseTiles.Count > 0 ? _baseTiles[0] : 0; }
            set
            {
                if (_baseTiles.Count > 0)
                    _baseTiles[0] = value;
                else
                    AddBaseTile(value);
            }
        }

        private List<int> _baseTiles = new List<int>();
        private List<int> _heightTiles = new List<int>();
        private List<int> _topperTiles = new List<int>();


        public MapCell(int tileID)
        {
            TileID = tileID;
        }

        public void AddBaseTile(int tileID)
        {
            _baseTiles.Add(tileID);
        }

        public void AddHeightTile(int tileID)
        {
            _heightTiles.Add(tileID);
        }

        public void AddTopperTile(int tileID)
        {
            _topperTiles.Add(tileID);
        }

        public void Draw(SpriteBatch batch, Vector2 index, float depthOffset, float heightRowDepthMod, float depthOffsetY)
        {

            int rowOffset = (int)index.Y%2 == 1 ? Tile.OddRowXOffset : 0;
            int heightRow = 0;

            DrawBaseTiles(batch, index, heightRowDepthMod, rowOffset, depthOffsetY);

            DrawHeightTiles(batch, index, depthOffset, heightRowDepthMod, ref heightRow, rowOffset);

            DrawTopperTiles(batch, index, depthOffset, heightRowDepthMod, heightRow, rowOffset);
        }

        private void DrawBaseTiles(SpriteBatch batch, Vector2 index, float heightRowDepthMod, int rowOffset, float depthOffsetY)
        {
            foreach (int tileID in _baseTiles)
            {
                depthOffsetY -= heightRowDepthMod;
                batch.Draw(
                    Tile.TileSetTexture,
                    Camera.WorldToScreen(
                        new Vector2((index.X*Tile.TileStepX) + rowOffset, index.Y*Tile.TileStepY)),
                    Tile.GetSourceRectangle(tileID),
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    depthOffsetY);
            }
        }

        private void DrawHeightTiles(SpriteBatch batch, Vector2 index, float depthOffset, float heightRowDepthMod,
                                    ref int heightRow, int rowOffset)
        {
            foreach (int tileID in _heightTiles)
            {
                var depth = CalculateDepth(depthOffset, heightRowDepthMod, heightRow);
                batch.Draw(
                    Tile.TileSetTexture,
                    Camera.WorldToScreen(
                        new Vector2(
                            (index.X * Tile.TileStepX) + rowOffset,
                            index.Y * Tile.TileStepY - (heightRow * Tile.HeightTileOffset))),
                    Tile.GetSourceRectangle(tileID),
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    depth);
                heightRow++;
            }
        }

        private void DrawTopperTiles(SpriteBatch batch, Vector2 index, float depthOffset, float heightRowDepthMod, int heightRow,
                                     int rowOffset)
        {
            var depth = CalculateDepth(depthOffset, heightRowDepthMod, heightRow);
            foreach (int tileID in _topperTiles)
            {
                batch.Draw(
                    Tile.TileSetTexture,
                    Camera.WorldToScreen(
                        new Vector2((index.X*Tile.TileStepX) + rowOffset, index.Y*Tile.TileStepY)),
                    Tile.GetSourceRectangle(tileID),
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    depth
                    );
            }
        }


        private float CalculateDepth(float depthOffset, float heightRowDepthMod, int heightRow)
        {
            return depthOffset - (heightRow*heightRowDepthMod);
        }
    }
}
