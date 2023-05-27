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
        private readonly int windowWidth = 1000;
        private readonly int windowHeight = 1000;

        // objects we are going to use:
        private Map map;

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

            // create map object
            map = new();
            map.CreateChunks("MapFiles/map1.txt");

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            map.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            map.Load(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // camera movements: if camera moves the map moves in the opposite way.
            // This gives the feeling of "exploring" the map.
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.W))
            {
                map.MoveDown();
            }
            if (kstate.IsKeyDown(Keys.S))
            {
                map.MoveUp();
            }
            if (kstate.IsKeyDown(Keys.A))
            {
                map.MoveRight();
            }
            if (kstate.IsKeyDown(Keys.D))
            {
                map.MoveLeft();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            map.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

    }
}