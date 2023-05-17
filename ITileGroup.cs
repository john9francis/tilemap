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
    public interface ITileGroup
    {
        public void Initialize();
        public void LoadContent(ContentManager content);
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}