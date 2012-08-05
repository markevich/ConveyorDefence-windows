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
        private NodeMap _nodeMap;
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
            _nodeMap = new NodeMap(_map);
            //for (int i = 0; i < 10; i++)
            //    for (int j = 0; j < 10; j++)
            //        _nodeMap.SetNode(new Conveyor(1000), i, j);
            
            var conveyor3 = new Conveyor(1000f){Direction = NodeDirection.LeftDown};
            var conveyor2 = new Conveyor(1000f){Direction = NodeDirection.LeftDown};
            var conveyor1 = new Conveyor(1000f) { Direction = NodeDirection.RightDown };
            var mine = new Mine(2000f){Direction = NodeDirection.RightDown};
            var deposit = new RockDeposit(10000f) {Direction = NodeDirection.LeftDown};
            _nodeMap.SetNode(deposit, 3, 4);
            _nodeMap.SetNode(mine, 2, 5);
            _nodeMap.SetNode(conveyor1, 3, 6);
            _nodeMap.SetNode(conveyor2, 3, 7);
            _nodeMap.SetNode(conveyor3, 3, 8);
            _nodeMap.UpdateSiblings();
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
            _nodeMap.Update(gameTime);
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
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _map.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
