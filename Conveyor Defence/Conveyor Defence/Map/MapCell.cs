using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Conveyor_Defence.Nodes;

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

        public void Draw(SpriteBatch batch, Point index, float depthOffset, float depthOffsetY)
        {

            int rowOffset = (int)index.Y%2 == 1 ? Tile.OddRowXOffset : 0;
            int heightRow = 0;

            DrawBaseTiles(batch, index, rowOffset, depthOffsetY);

            DrawHeightTiles(batch, index, depthOffset, ref heightRow, rowOffset);

            DrawTopperTiles(batch, index, depthOffset, heightRow, rowOffset);

            DrawNode(batch, index, depthOffset, heightRow, rowOffset);

        }


        private void DrawNode(SpriteBatch batch, Point index, float depthOffset, int heightRow, int rowOffset)
        {
            var node = NodeMap.Instance[index.X, index.Y];
            if (node == null)
            {
                return;
            }
            var depth = CalculateDepth(depthOffset, heightRow);
            var position = Camera.WorldToScreen(
                new Vector2((index.X*Tile.TileStepX) + rowOffset, index.Y*Tile.TileStepY));
            node.Draw(batch, position, depth);

        }

        private void DrawBaseTiles(SpriteBatch batch, Point index, int rowOffset, float depthOffsetY)
        {
            foreach (int tileID in _baseTiles)
            {
                depthOffsetY -= Tile.DepthModifier;
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

        private void DrawHeightTiles(SpriteBatch batch, Point index, float depthOffset, ref int heightRow, int rowOffset)
        {
            foreach (int tileID in _heightTiles)
            {
                var depth = CalculateDepth(depthOffset, heightRow);
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

        private void DrawTopperTiles(SpriteBatch batch, Point index, float depthOffset, int heightRow,
                                     int rowOffset)
        {
            var depth = CalculateDepth(depthOffset, heightRow);
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


        private float CalculateDepth(float depthOffset, int heightRow)
        {
            return depthOffset - (heightRow * Tile.DepthModifier);
        }

        
    }
}
