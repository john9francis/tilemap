using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tilemap
{
    internal class Tile
    {
        private Texture2D _texture;
        private Vector2 _position;

        private float _hitBoxWidth;
        private float _hitBoxHeight;

        public Tile()
        {
 
        }

        #region Getters and Setters
        public void SetTexture(Texture2D texture)
        {
            _texture = texture;
        }
        public void SetPosition(Vector2 position)
        {
            _position = position;
        }
        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(
                _texture,
                _position,
                null,
                Color.White,
                0f,
                new Vector2(_texture.Width / 2, _texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);

            spriteBatch.End();
        }
    }
}
