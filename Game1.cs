using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace tilemap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _tileTexture;

        // window size
        private int windowWidth = 1000;
        private int windowHeight = 1000;

        // objects we are going to use:
        Chunk chunk;

        public Game1()
        {
            // monogame stuff
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            // setting window size
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges();

            // create tile object
            chunk = new Chunk();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            // create is under base.Initialize because we want to load the content first.
            chunk.SetChunkPosition(new Vector2(100, 100));
            chunk.SetTileSize(100);
            chunk.SetChunkSize(5,5);
            foreach (string s in chunk.GetChunkList())
            {
                Debug.WriteLine(s);

            }
            chunk.Create();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            chunk.SetPossibleTexture("block1",Content.Load<Texture2D>("Tiles/block1"));

            // load the chunk list
            chunk.SetChunkList(chunk.ReadListFromFile("ChunkFiles/01.txt"));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            chunk.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}