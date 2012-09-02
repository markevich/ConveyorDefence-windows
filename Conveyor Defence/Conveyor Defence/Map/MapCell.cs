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

        public void Draw(SpriteBatch batch, Point index)
        {

            int rowOffset = index.Y%2 == 1 ? Tile.OddRowXOffset : 0;
            int heightRow = 0;
            DrawBaseTiles(batch, index, rowOffset);

            DrawHeightTiles(batch, index, ref heightRow, rowOffset);

            DrawTopperTiles(batch, index, heightRow, rowOffset);

            DrawNode(batch, index, heightRow, rowOffset);

        }


        private void DrawNode(SpriteBatch batch, Point index, int heightRow, int rowOffset)
        {
            var node = NodeMap.Instance[index.X, index.Y];
            if (node == null)
            {
                return;
            }
            var depth = DepthCalculator.CalculateDepth(index.X, index.Y, heightRow);
            var position = Camera.WorldToScreen(
                new Vector2((index.X*Tile.TileStepX) + rowOffset, index.Y*Tile.TileStepY));
            node.Draw(batch, position, depth);

        }

        private void DrawBaseTiles(SpriteBatch batch, Point index, int rowOffset)
        {
            foreach (int tileID in _baseTiles)
            {
                var depthOffsetY = DepthCalculator.CalculateDepthOffsetY(index.Y);
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

        private void DrawHeightTiles(SpriteBatch batch, Point index, ref int heightRow, int rowOffset)
        {
            foreach (int tileID in _heightTiles)
            {
                var depth = DepthCalculator.CalculateDepth(index.X, index.Y, heightRow);
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

        private void DrawTopperTiles(SpriteBatch batch, Point index, int heightRow,
                                     int rowOffset)
        {
            var depth = DepthCalculator.CalculateDepth(index.X, index.Y, heightRow);
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


        
    }
}
