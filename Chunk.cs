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
    internal class Chunk
    {
        private int _width; // how many tiles wide
        private int _height; // how many tiles tall

        private int _tileSize; // how many pixels tall and wide the tile should be

        private List<Tile> _tiles = new(); // list of tiles

        private Vector2 _position = Vector2.Zero; // the position of the upper left hand corner of the chunk

        private List<string> _fileMap = new List<string>(); // each entry in the _fileMap will be a tilename or a 0

        private Dictionary<string, Texture2D> _possibleTextures = new();

        private string _textureFolderName; // the folder in the content that holds our tiles
        private string _fileFolderName; // the folder that has our .txt file with our map
        private string _fileName;

        //private Texture2D test = ContentManager.Content.Load<Texture2D>("City1/01");

        public Chunk(
            string textureFolder,
            string fileFolder,
            string fileName,
            int tileSize=100,
            int chunkPosX=0,
            int chunkPosY=0
            )
        {
            // set the chunk based on the file
            _textureFolderName = textureFolder;
            _fileFolderName = fileFolder;
            _fileName = fileName;
            _tileSize = tileSize;
            _position = new Vector2(chunkPosX, chunkPosY);

            SetChunkSizeFromData(_fileFolderName + "/" + _fileName);

            _fileMap = MakeTileNameList(_fileFolderName + "/" + _fileName);

        }

        private void SetPossibleTexture(string textureName, Texture2D texture)
        {
            _possibleTextures.Add(textureName, texture);
        }

        public void Create()
        {
            // creates one chunk by creating tiles and adding them to the _tiles list. 
            float posX = _position.X;
            float posY = _position.Y;
            int count = 1;
            for (int i = 0; i < _fileMap.Count; i++)
            {
                if (i == _width * count)
                {
                    posY += _tileSize;
                    posX = _position.X;
                    count++;
                }

                if (posY >= _height * _tileSize + _position.Y)
                {
                    break;
                }

                if (_possibleTextures.ContainsKey(_fileMap[i]))
                {
                    Tile tile = new Tile(posX, posY, _tileSize);
                    tile.SetTexture(_possibleTextures[_fileMap[i]]);
                    _tiles.Add(tile);
                }

                posX += _tileSize;
            }
        }

        #region Moving the chunk

        public void MoveDown()
        {
            foreach (Tile t in _tiles)
            {
                t.SetPosition(new Vector2(t.GetPosition().X, t.GetPosition().Y - 10));
            }
        }
        public void MoveUp()
        {
            foreach (Tile t in _tiles)
            {
                t.SetPosition(new Vector2(t.GetPosition().X, t.GetPosition().Y + 10));
            }
        }
        public void MoveLeft()
        {
            foreach (Tile t in _tiles)
            {
                t.SetPosition(new Vector2(t.GetPosition().X + 10, t.GetPosition().Y));
            }
        }
        public void MoveRight()
        {
            foreach (Tile t in _tiles)
            {
                t.SetPosition(new Vector2(t.GetPosition().X - 10, t.GetPosition().Y));
            }
        }

        #endregion

        #region MonoGame game functions

        public void Initialize()
        {
            Create();
        }

        public void Load(ContentManager content)
        {
            // put all the files that are in the content folder into the possibleTextures list in the chunk
            // setting the directory
            string currentDirectory = Directory.GetCurrentDirectory();
            string baseDirectory = GetBaseDirectory(currentDirectory);
            string root = baseDirectory + "\\tilemap\\Content\\" + _textureFolderName + "\\";

            // getting all the strings of the filenames
            var files = from file in Directory.EnumerateFiles(root) select file;
            foreach (var file in files)
            {
                // get rid of the whole path except the literal name of the file
                string[] allPathFolders = file.Split("\\");
                string fullFilename = allPathFolders.Last();
                string[] seperatedFilename = fullFilename.Split(".");
                string fileName = seperatedFilename[0];

                // now add this to the list of possible textures in the chunk class:
                SetPossibleTexture(fileName, content.Load<Texture2D>(_textureFolderName + "/" + fileName));

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _tiles)
            {
                tile.Draw(spriteBatch);
            }

        }

        #endregion

        #region Reading and writing to the file

        // functions to save and load to file
        public void WriteListToFile(List<string> list, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in list)
                {
                    writer.WriteLine(item);
                }
            }
        }

        private void SetChunkSizeFromData(string fileName)
        {
            // looks at the file map and sets the chunk size:

            List<string> rowList = ReadListFromFile(fileName);

            // get the width and height of the data & set the chunk size
            int rows = rowList.Count;
            string[] firstRow = rowList[0].Split(',');

            // don't forget to get rid of empty characters in the first row:
            List<string> firstRow_ = new();
            for (int i = 0; i < firstRow.Count(); i++)
            {
                if (firstRow[i] != "")
                    firstRow_.Add(firstRow[i]);
            }
            int columns = firstRow_.Count();

            _width = columns;
            _height = rows;
        }

        public List<string> MakeTileNameList(string fileName)
        {
            // add the data to the list.

            List<string> rowList = ReadListFromFile(fileName);

            List<string> list = new List<string>();
            foreach (string row in rowList)
            {
                string[] entries = row.Split(",");
                foreach (string entry in entries)
                {
                    if (entry != "")
                        list.Add(entry);
                }
            }

            return list;
        }

        public List<string> ReadListFromFile(string fileName)
        {
            // reads a file and returns a list of rows

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
                }
            }

            return rowList;
        }

        #endregion


        private string GetBaseDirectory(string currentDirectory, string folderDesired="tilemap")
        {
            // no matter what computer this is on, it should find the right folder. this helps for loading content. 

            string baseDirectory = currentDirectory;
            while (!Directory.Exists(Path.Combine(baseDirectory, folderDesired)))
            {
                baseDirectory = Directory.GetParent(baseDirectory).FullName;
            }
            return baseDirectory;
        }
    }


}

