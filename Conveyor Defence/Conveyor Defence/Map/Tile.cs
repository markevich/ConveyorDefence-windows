using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conveyor_Defence.Map
{
    static class Tile
    {
        public static Texture2D TileSetTexture;
        public const int TileWidth = 64;
        public const int TileHeight = 64;
        public const int TileStepX = 64;
        public const int TileStepY = 16;
        public const int OddRowXOffset = 32;
        public const int HeightTileOffset = 32;
        public const float DepthModifier = 0.0000001f;

        static public Rectangle GetSourceRectangle(int tileIndex)
        {
            var tileY = tileIndex / (TileSetTexture.Width / TileWidth);
            var tileX = tileIndex % (TileSetTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}
