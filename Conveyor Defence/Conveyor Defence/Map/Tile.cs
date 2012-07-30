using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conveyor_Defence
{
    static class Tile
    {
        public static Texture2D TileSetTexture;
        static public int TileWidth = 64;
        static public int TileHeight = 64;
        static public int TileStepX = 64;
        static public int TileStepY = 16;
        static public int OddRowXOffset = 32;
        static public int HeightTileOffset = 32;
        static public Rectangle GetSourceRectangle(int tileIndex)
        {
            var tileY = tileIndex / (TileSetTexture.Width / TileWidth);
            var tileX = tileIndex % (TileSetTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}
