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
        // NOTE: the anchor point is the top left corner
        // NOTE: the tile renders exactly as big as it's hitbox


        private Texture2D _texture;
        private Vector2 _position;

        private float _hitBoxWidth;
        private float _hitBoxHeight;

        public Tile()
        {
            _hitBoxWidth = 50;
            _hitBoxHeight = 50;
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
        public void SetHitbox(float width, float height)
        {
            _hitBoxWidth = width;
            _hitBoxHeight = height;
        }
        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = Color.White;
            float rotation = 0f;
            Vector2 origin = Vector2.Zero;

            // NOTE: the scale is normalized. that's why it's divided by the texture width and height.
            Vector2 scale = new Vector2(_hitBoxWidth / _texture.Width, _hitBoxHeight / _texture.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(
                _texture,
                _position,
                null,
                color,
                rotation,
                origin,
                scale,
                SpriteEffects.None,
                0f);


            spriteBatch.End();
        }
    }
}
