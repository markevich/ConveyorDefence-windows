using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conveyor_Defence
{
    class MapCell
    {
        public int TileID
        {
            get { return BaseTiles.Count > 0 ? BaseTiles[0] : 0; }
            set
            {
                if (BaseTiles.Count > 0)
                    BaseTiles[0] = value;
                else
                    AddBaseTile(value);
            }
        }
        public List<int> BaseTiles = new List<int>();
        public List<int> HeightTiles = new List<int>();
        public List<int> TopperTiles = new List<int>();


        public bool Walkable { get; set; }

        public int SlopeMap { get; set; }

        public MapCell(int tileID)
        {
            TileID = tileID;
            Walkable = true;
            SlopeMap = -1;
        }

        public void AddBaseTile(int tileID)
        {
            BaseTiles.Add(tileID);
        }

        public void AddHeightTile(int tileID)
        {
            HeightTiles.Add(tileID);
        }

        public void AddTopperTile(int tileID)
        {
            TopperTiles.Add(tileID);
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
            foreach (int tileID in BaseTiles)
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
            float depth;
            foreach (int tileID in HeightTiles)
            {
                depth = CalculateDepth(depthOffset, heightRowDepthMod, heightRow);
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
            float depth;
            depth = CalculateDepth(depthOffset, heightRowDepthMod, heightRow);
            foreach (int tileID in TopperTiles)
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
