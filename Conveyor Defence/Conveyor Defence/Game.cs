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
        public static SpriteFont DebugFont;
        public static SpriteFont CountFont;
        SpriteBatch _spriteBatch;
        private TileMap _map;
        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Tile.TileTexture = Content.Load<Texture2D>(@"Textures\TileSets\tileset");
            DebugFont = Content.Load<SpriteFont>(@"Fonts\Pericles");
            CountFont = Content.Load<SpriteFont>(@"Fonts\Count");
            _map = new TileMap(
                Content.Load<Texture2D>(@"Textures\TileSets\mousemap"),
                Content.Load<Texture2D>(@"Textures\TileSets\highlight")
                );

            InitializeCamera();
            NodeMap.CreateInstance();
            //for (int i = 0; i < 10; i++)
            //    for (int j = 0; j < 10; j++)
            //        _nodeMap.SetNode(new Conveyor(1000), i, j);
            
            var nodeMap = NodeMap.Instance;
            
            nodeMap.SetNode(new RockDeposit(2000f) {Direction = NodeDirection.LeftDown}, 10, 4);
            nodeMap.SetNode(new Mine(500f) { Direction = NodeDirection.RightDown }, 9, 5);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 10, 6);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 10, 7);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 10, 8);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 9, 9);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 9, 10);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 8, 11);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 8, 12);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 8, 13);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 9, 14);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 9, 15);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 9, 16);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 8, 17);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 9, 18);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 9, 19);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 10, 20);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 10, 21);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 11, 22);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 11, 23);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.RightDown), 12, 24);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 12, 25);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 12, 26);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 11, 27);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 11, 28);

            nodeMap.SetNode(new RockDeposit(1500f) { Direction = NodeDirection.RightDown }, 15, 22);
            nodeMap.SetNode(new Mine(500f) { Direction = NodeDirection.LeftDown }, 15, 23);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 15, 24);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 14, 25);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 14, 26);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 13, 27);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 13, 28);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 12, 29);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 12, 30);

            nodeMap.AddTower(NodeDirection.LeftDown, 10, 29);
            nodeMap.AddTower(NodeDirection.LeftDown, 11, 31);

            nodeMap.SetNode(new RockDeposit(1000f) { Direction = NodeDirection.RightDown }, 17, 24);
            nodeMap.SetNode(new Mine(500f) { Direction = NodeDirection.LeftDown }, 17, 25);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 17, 26);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 16, 27);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 16, 28);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 15, 29);
            nodeMap.SetNode(NodeFactory.GetConveyour(NodeDirection.LeftDown), 15, 30);

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
            if(ms.LeftButton == ButtonState.Pressed)
            {
                var towers = NodeMap.Instance.GetTowers();
                var cursorLocation = Camera.ScreenToWorld(new Vector2(ms.X, ms.Y));
                foreach (var tower in towers)
                {
                    tower.Shoot((int)cursorLocation.X, (int)cursorLocation.Y);
                }
            }


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
            NodeMap.Instance.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
