using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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

        // defining the map
        private List<string> _map = new List<string> 
        { "block1","block1", "block1","block1","block1",
            "000","000","000","block1","000",
            "block1","000","block1","block1","000",
        "block1","block1", "block1","block1","block1",
        "block1","block1", "block1","block1","block1",
        "block1","block1", "block1","block1","block1",
        "block1","block1", "block1","block1","block1",
        "block1","block1", "block1","block1","block1",};

        private Dictionary<string,Texture2D> _possibleTextures = new();

        public Chunk(int width=5, int height = 5, int tileSize=100) 
        {
            _width = width;
            _height = height;
            _tileSize = tileSize;
        }

        public void SetPossibleTexture(string textureName, Texture2D texture)
        {
            _possibleTextures.Add(textureName,texture);
        }

        public void Create()
        {
            float positionX = 0;
            float positionY = 0;
            int count = 1;
            for (int i = 0; i < _map.Count; i++)
            {
                if (_map[i] != "000" && _possibleTextures.ContainsKey(_map[i]))
                {
                    Tile tile = new Tile();
                    tile.SetTexture(_possibleTextures[_map[i]]);
                    tile.SetPosition(new Vector2(positionX, positionY));
                    tile.SetHitbox(100,100);
                    _tiles.Add(tile);
                }

                positionX += _tileSize;

                if (i==_width*count)
                {
                    positionY += _tileSize;
                    positionX = 0;
                    count++;
                }

                if (positionY/_tileSize >= _height)
                {
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _tiles)
            {
                tile.Draw(spriteBatch);
            }
            
        }
    }
}
