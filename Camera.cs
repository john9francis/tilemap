using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.IO;
using System.IO.Enumeration;
using System.Linq;

namespace tilemap
{
    public class Camera
    {
        private Matrix _translation;
        private float _dy = 0;
        private float _dx = 0;

        public Camera()
        {
        }

        private void CalculateTranslation()
        {
            _translation = Matrix.CreateTranslation(_dx,_dy,0f);
        }

        public void Update()
        {
            var kstate = Keyboard.GetState();
            CalculateTranslation();
        }
    }

}
