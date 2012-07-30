using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Conveyor_Defence.Deposits;

namespace Conveyor_Defence
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private TileMap map;
        private NodeMap nodeMap;
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tile.TileSetTexture = Content.Load<Texture2D>(@"Textures\TileSets\tileset");
            map = new TileMap(
                Content.Load<Texture2D>(@"Textures\TileSets\mousemap"),
                Content.Load<SpriteFont>(@"Fonts\Pericles"),
                Content.Load<Texture2D>(@"Textures\TileSets\highlight")
                );

            InitializeCamera();
            nodeMap = new NodeMap();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    nodeMap.SetNode(new Conveyor(1000), i, j);
            
            var conveyor3 = new Conveyor(1000f){Direction = NodeDirection.LeftDown};
            var conveyor2 = new Conveyor(1000f){Direction = NodeDirection.LeftDown};
            var conveyor1 = new Conveyor(1000f) { Direction = NodeDirection.RightDown };
            var mine = new Mine(2000f){Direction = NodeDirection.RightDown};
            var deposit = new RockDeposit(10000f) {Direction = NodeDirection.LeftDown};
            nodeMap.SetNode(deposit, 3, 4);
            nodeMap.SetNode(mine, 2, 5);
            nodeMap.SetNode(conveyor1, 3, 6);
            nodeMap.SetNode(conveyor2, 3, 7);
            nodeMap.SetNode(conveyor3, 3, 8);
            nodeMap.UpdateSiblings();
        }

        private void InitializeCamera()
        {
            Camera.ViewWidth = graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((map.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((map.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(map.BaseOffsetX, map.BaseOffsetY);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            HandleInput();
            nodeMap.Update(gameTime);
            base.Update(gameTime);
        }

        private void HandleInput()
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.W))
            {
                Camera.Move(new Vector2(0, -6));
            }
            if (ks.IsKeyDown(Keys.S))
            {
                Camera.Move(new Vector2(0, 6));
            }
            if (ks.IsKeyDown(Keys.A))
            {
                Camera.Move(new Vector2(-6, 0));
            }
            if (ks.IsKeyDown(Keys.D))
            {
                Camera.Move(new Vector2(6, 0));
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend); ;
            map.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
