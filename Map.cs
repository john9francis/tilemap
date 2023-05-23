using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.IO.Enumeration;
using Microsoft.Xna.Framework.Content;

namespace tilemap
{
    internal class Map
    {
        // this is a class that combines a couple of chunks into a tilemap.
        // it's gonna read from a file as well.

        private List<Chunk> chunkList; 

        public Map()
        {
            chunkList = new List<Chunk>();
        }

        public void AddChunk(Chunk c)
        {
            chunkList.Add(c);
        }

        public List<Chunk> GetChunkList() {  return chunkList; }

        // the monogame functions
        /*
        public void Initialize();
        public void Load();
        public void Update();
        public void Draw();
        */

        public void Initialize()
        {
            foreach (Chunk c in chunkList)
            {
                c.Initialize();
            }
        }
        public void Load(ContentManager content)
        {
            foreach (Chunk c in chunkList)
            {
                c.Load(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            // move the "camera" around with the arrow keys
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Chunk c in chunkList)
            {
                c.Draw(spriteBatch);
            }
        }

        #region Movements

        public void MoveUp()
        {
            foreach(Chunk c in chunkList)
            {
                c.MoveUp();
            }
        }
        public void MoveDown()
        {
            foreach(Chunk c in chunkList)
            {
                c.MoveDown();
            }
        }
        public void MoveLeft()
        {
            foreach(Chunk c in chunkList)
            {
                c.MoveLeft();
            }
        }
        public void MoveRight()
        {
            foreach(Chunk c in chunkList)
            {
                c.MoveRight();
            }
        }



        #endregion


        public void ReadFromFile(string fileName)
        {
            // reads the specialized type of txt file that is for TileMaps.

            // step 1: open the tilemap file
            string currentDirectory = Directory.GetCurrentDirectory();
            string baseDirectory = GetBaseDirectory(currentDirectory);
            string filePath = Path.Combine(baseDirectory, "tilemap", fileName);
            List<string> rowList = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    rowList.Add(line);
                    Debug.WriteLine(line);
                }
            }


        }

        private string GetBaseDirectory(string currentDirectory, string folderDesired = "tilemap")
        {
            string baseDirectory = currentDirectory;
            while (!Directory.Exists(Path.Combine(baseDirectory, folderDesired)))
            {
                baseDirectory = Directory.GetParent(baseDirectory).FullName;
            }
            return baseDirectory;
        }
    }
}