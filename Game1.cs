using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.IO;
using System.IO.Enumeration;
using System.Linq;


namespace tilemap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // window size
        private int windowWidth = 1000;
        private int windowHeight = 1000;

        // objects we are going to use:
        Chunk chunk;

        Camera c = new Camera();

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

            // create chunk object
            chunk = new Chunk("City1/Building1/","ChunkFiles","01.txt");

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            // create is under base.Initialize because we want to load the content first.
            chunk.SetChunkPosition(new Vector2(50, 50));
            chunk.SetTileSize(100);
            chunk.Create();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            chunk.Load(Content);

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