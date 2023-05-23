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


        private Texture2D _texture;
        private Vector2 _position;

        // assuming a square tile
        private float _size;

        public Tile(float positionX, float positionY, int size=50)
        {
            _position = new Vector2(positionX, positionY);
            _size = size;

        }

        #region Getters and Setters

        public Vector2 GetPosition()
        {
            return _position;
        }

        public void SetTexture(Texture2D texture)
        {
            _texture = texture;
        }
        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        #endregion

        #region Moving

        public void MoveUp()
        {
            _position = new Vector2(GetPosition().X, GetPosition().Y - 10);
        }

        public void MoveDown()
        {
            _position = new Vector2(GetPosition().X, GetPosition().Y + 10);

        }

        public void MoveLeft()
        {
            _position = new Vector2(GetPosition().X - 10, GetPosition().Y);

        }

        public void MoveRight()
        {
            _position = new Vector2(GetPosition().X + 10, GetPosition().Y);

        }

        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = Color.White;
            float rotation = 0f;
            Vector2 origin = Vector2.Zero;

            // NOTE: the scale is normalized. that's why it's divided by the texture width and height.
            Vector2 scale = new Vector2(_size / _texture.Width, _size / _texture.Height);

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
