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


namespace tilemap
{
    internal class Chunk
    {
        private int _width; // how many tiles wide
        private int _height; // how many tiles tall

        private int _tileSize; // how many pixels tall and wide the tile should be

        private List<Tile> _tiles = new();

        private Vector2 _position = Vector2.Zero; // the position of the upper left hand corner of the chunk

        // defining the map
        // (next step would be having this in a file or something)
        private List<string> _map = new List<string>();

        private Dictionary<string,Texture2D> _possibleTextures = new();

        public Chunk(int width=5, int height = 5, int tileSize=100) 
        {
            _width = width;
            _height = height;
            _tileSize = tileSize;
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
            _possibleTextures.Add(textureName,texture);
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

        #endregion

        public void Create()
        {
            // creates one chunk
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

                if (_map[i] != "000" && _possibleTextures.ContainsKey(_map[i]))
                {
                    Tile tile = new();
                    tile.SetTexture(_possibleTextures[_map[i]]);
                    tile.SetPosition(new Vector2(posX, posY));
                    tile.SetHitbox(_tileSize,_tileSize);
                    _tiles.Add(tile);
                }

                posX += _tileSize;
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

        public List<string> ReadListFromFile(string filePath)
        {
            List<string> list = new List<string>();

            using (StreamReader reader = new StreamReader("C:/Users/john9/OneDrive/Desktop/My own projects/GitHub/tilemap/" + filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            return list;
        }
    }
}

