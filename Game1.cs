using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.IO;
using System.IO.Enumeration;
using System.Linq;

//NOTE: just put all the logic in Tile and then Chunk and Map are literally just groups.
// Chunk and Map are so similar, they just need to loop through each one of the list. 

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
        Chunk chunk1;
        Chunk chunk2;
        Chunk chunk3;
        Map map;

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
            chunk1 = new Chunk("City1","ChunkFiles","road1.txt",100,0,500);
            chunk2 = new Chunk("City1/Building1", "ChunkFiles", "01.txt", 100, 0, 100);
            chunk3 = new Chunk("City1/Building1", "ChunkFiles", "01.txt", 100, 500, 100);
            map = new();
            map.AddChunk(chunk1);
            map.AddChunk(chunk2);
            map.AddChunk(chunk3);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            map.Initialize();

            //map.ReadFromFile("MapFiles/map1.txt");
            
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

            // move the "camera" around with the arrow keys
            // NOTE: move this into the chunk.update() method.
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.A))
            {
                foreach (Chunk chunk in map.GetChunkList())
                {
                    chunk.MoveLeft();
                }
            }
            if (kstate.IsKeyDown(Keys.D))
            {
                foreach (Chunk chunk in map.GetChunkList())
                {
                    chunk.MoveRight();
                }
            }
            if (kstate.IsKeyDown(Keys.W))
            {
                foreach (Chunk chunk in map.GetChunkList())
                {
                    chunk.MoveUp();
                }
            }
            if (kstate.IsKeyDown(Keys.S))
            {
                foreach (Chunk chunk in map.GetChunkList())
                {
                    chunk.MoveDown();
                }
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