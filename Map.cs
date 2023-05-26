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

        public List<Chunk> GetChunkList() { return chunkList; }

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
            foreach (Chunk c in chunkList)
            {
                c.MoveUp();
            }
        }
        public void MoveDown()
        {
            foreach (Chunk c in chunkList)
            {
                c.MoveDown();
            }
        }
        public void MoveLeft()
        {
            foreach (Chunk c in chunkList)
            {
                c.MoveLeft();
            }
        }
        public void MoveRight()
        {
            foreach (Chunk c in chunkList)
            {
                c.MoveRight();
            }
        }



        #endregion

        public void CreateChunks(string fileName)
        {
            // adds all the chunks from the file into the chunk list.

            // step one: get list from file
            List<string> stringList = ReadFromFile(fileName);

            // This List is in the form:
            //      Chunk,
            //      Content folder name,
            //      txt file folder name,
            //      txt file name,
            //      tile size,
            //      x coordinate of the chunk,
            //      y coordinate of the chunk.

            // p.s. # symbol means comment. 

            // step 1: take each list item by line:
            foreach (string line in stringList)
            {
                string[] lineParts = line.Split(",");
                if (lineParts[0] == "Chunk")
                {
                    // create a new chunk based on the file info
                    string contentFolder = lineParts[1];
                    string fileFolder = lineParts[2];
                    string chunkFileName = lineParts[3];
                    int tileSize = int.Parse(lineParts[4]);
                    int xPos = int.Parse(lineParts[5]);
                    int yPos = int.Parse(lineParts[6]);

                    Chunk c = new Chunk(contentFolder, fileFolder, chunkFileName, tileSize, xPos, yPos);

                    // add that new chunk to the chunklist
                    chunkList.Add(c);
                }
            }
        }

        public List<string> ReadFromFile(string fileName)
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
                    try
                    {
                        if (line[0] != '#')
                        {
                            rowList.Add(line);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // it must be a blank line, so do nothing
                    }
                }
            }

            return rowList;
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