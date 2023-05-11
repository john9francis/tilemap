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

        private List<Tile> _tiles = new();
        private List<string> _map = new List<string> { "0","0","0","block1","0","block1" };

        private Dictionary<string,Texture2D> _possibleTextures = new();

        public Chunk() 
        {

        }
        public Chunk(int size) 
        {

        }

        public void SetPossibleTexture(string textureName, Texture2D texture)
        {
            _possibleTextures.Add(textureName,texture);
        }

        public void Create()
        {
            float positionX = 0;
            float positionY = 0;
            for (int i = 0; i < _map.Count; i++)
            {
                if (_map[i] != "0" && _possibleTextures.ContainsKey(_map[i]))
                {
                    Tile tile = new Tile();
                    tile.SetTexture(_possibleTextures[_map[i]]);
                    tile.SetPosition(new Vector2(positionX, positionY));
                    tile.SetHitbox(100,100);
                    _tiles.Add(tile);
                }

                positionX += 100;
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
