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

        private List<string> _map = new List<string>(); // each entry in the _map will be a tilename or a 0

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
            SetTileSize(tileSize);
            SetChunkPosition(new Vector2(chunkPosX, chunkPosY));

            _map = ReadListFromFile(_fileFolderName + "/" + _fileName);

        }

        #region Getters and Setters

        public void SetChunkSize(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void SetTileSize(int tileSize)
        {
            _tileSize = tileSize;
        }

        public void SetPossibleTexture(string textureName, Texture2D texture)
        {
            _possibleTextures.Add(textureName, texture);
        }

        public void SetChunkPosition(Vector2 position)
        {
            _position = position;
        }

        public void SetChunkList(List<string> list)
        {
            _map = list;
        }

        public List<string> GetChunkList()
        {
            return _map;
        }

        public string GetTextureFolder()
        {
            return _textureFolderName;
        }

        public string GetFileFolder()
        {
            return _fileFolderName;
        }

        public string GetFile()
        {
            return _fileName;
        }

        public List<Tile> GetTileList()
        {
            return _tiles;
        }

        #endregion

        public void Create()
        {
            // creates one chunk by creating many tiles and adding them to the _tiles list. 
            float posX = _position.X;
            float posY = _position.Y;
            int count = 1;
            for (int i = 0; i < _map.Count; i++)
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

                if (_possibleTextures.ContainsKey(_map[i]))
                {
                    Tile tile = new();
                    tile.SetTexture(_possibleTextures[_map[i]]);
                    tile.SetPosition(new Vector2(posX, posY));
                    tile.SetHitbox(_tileSize, _tileSize);
                    _tiles.Add(tile);
                }

                posX += _tileSize;
            }
        }

        #region Moving the chunk

        public void MoveDown()
        {
            foreach (Tile t in GetTileList())
            {
                t.SetPosition(new Vector2(t.GetPosition().X, t.GetPosition().Y - 10));
            }
        }
        public void MoveUp()
        {
            foreach (Tile t in GetTileList())
            {
                t.SetPosition(new Vector2(t.GetPosition().X, t.GetPosition().Y + 10));
            }
        }
        public void MoveLeft()
        {
            foreach (Tile t in GetTileList())
            {
                t.SetPosition(new Vector2(t.GetPosition().X + 10, t.GetPosition().Y));
            }
        }
        public void MoveRight()
        {
            foreach (Tile t in GetTileList())
            {
                t.SetPosition(new Vector2(t.GetPosition().X - 10, t.GetPosition().Y));
            }
        }

        #endregion

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
            string root = baseDirectory + "\\tilemap\\Content\\" + GetTextureFolder() + "\\";

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
                SetPossibleTexture(fileName, content.Load<Texture2D>(GetTextureFolder() + fileName));

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _tiles)
            {
                tile.Draw(spriteBatch);
            }

        }

        

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

        public List<string> ReadListFromFile(string fileName)
        {
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

            SetChunkSize(columns, rows);


            // add the data to the list.
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

        private string GetBaseDirectory(string currentDirectory, string folderDesired="tilemap")
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

