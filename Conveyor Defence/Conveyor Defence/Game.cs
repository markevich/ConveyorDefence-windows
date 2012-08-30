using Conveyor_Defence.Map;
using Conveyor_Defence.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Conveyor_Defence
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private TileMap _map;
        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Tile.TileSetTexture = Content.Load<Texture2D>(@"Textures\TileSets\tileset");
            _map = new TileMap(
                Content.Load<Texture2D>(@"Textures\TileSets\mousemap"),
                Content.Load<SpriteFont>(@"Fonts\Pericles"),
                Content.Load<Texture2D>(@"Textures\TileSets\highlight")
                );

            InitializeCamera();
            NodeMap.CreateInstance();
            //for (int i = 0; i < 10; i++)
            //    for (int j = 0; j < 10; j++)
            //        _nodeMap.SetNode(new Conveyor(1000), i, j);
            
            var nodeMap = NodeMap.Instance;
            nodeMap.SetNode(new RockDeposit(7500f) {Direction = NodeDirection.LeftDown}, 3, 4);
            nodeMap.SetNode(new Mine(2000f){Direction = NodeDirection.RightDown}, 2, 5);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 3, 6);
            nodeMap.SetNode(new Conveyor(1000f){Direction = NodeDirection.LeftDown}, 3, 7);
            nodeMap.SetNode(new Conveyor(1000f){Direction = NodeDirection.LeftDown}, 3, 8);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 2, 9);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 2, 10);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 1, 11);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 1, 12);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 1, 13);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 2, 14);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 2, 15);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 2, 16);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 1, 17);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 2, 18);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 2, 19);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 3, 20);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 3, 21);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 4, 22);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 4, 23);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.RightDown }, 5, 24);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 5, 25);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 5, 26);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 4, 27);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 4, 28);

            nodeMap.SetNode(new RockDeposit(6000) { Direction = NodeDirection.RightDown }, 8, 22);
            nodeMap.SetNode(new Mine(2000f) { Direction = NodeDirection.LeftDown }, 8, 23);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 8, 24);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 7, 25);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 7, 26);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 6, 27);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 6, 28);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 5, 29);
            nodeMap.SetNode(new Conveyor(1000f) { Direction = NodeDirection.LeftDown }, 5, 30);


            nodeMap.UpdateSiblings();
        }

        private void InitializeCamera()
        {
            Camera.ViewWidth = _graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = _graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((TileMap.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((TileMap.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(TileMap.BaseOffsetX, TileMap.BaseOffsetY);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            HandleInput();
            NodeMap.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        private void HandleInput()
        {

            var ms = Mouse.GetState();
            var heightPercentage = ms.Y * 100 / _graphics.PreferredBackBufferHeight;
            var widthPercentage = ms.X * 100 / _graphics.PreferredBackBufferWidth;
            if (heightPercentage > 90)
            {
                Camera.Move(new Vector2(0, 6));
            }
            if (heightPercentage < 10)
            {
                Camera.Move(new Vector2(0, -6));
            }
            if (widthPercentage > 90)
            {
                Camera.Move(new Vector2(6, 0));
            }
            if (widthPercentage < 10)
            {
                Camera.Move(new Vector2(-6, 0));
            }
            var ks = Keyboard.GetState();

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
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _map.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
