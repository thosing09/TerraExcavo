using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace compsci_NEA
{
    class entity
    {
        public Vector2 _velocity;
        public Rectangle _rectangle;
        private Texture2D _texture;
        private int _bounds;
        public bool _isGrounded;

        public entity(Rectangle rectangle, Texture2D texture, int bounds)
        {
            _velocity = new Vector2(0, 0);
            _rectangle = rectangle;
            _texture = texture;
            _bounds = bounds;

        }

        public void Update(Rectangle platform)
        {
            if (_rectangle.Intersects(platform))
            {
                _rectangle.Y -= (_rectangle.Y + _rectangle.Height) - platform.Y;
                _velocity.Y = 0;
                _isGrounded = true;
            }

            if (_rectangle.X < 0)
            {
                _velocity *= -0.9f;
                _rectangle.X -= _rectangle.X;
            }

            if (_rectangle.X > _bounds)
            {
                _velocity *= -0.9f;
                _rectangle.X -= (_rectangle.X + _rectangle.Width) - _bounds;
            }

            _rectangle.X += (int)_velocity.X;
            _rectangle.Y += (int)_velocity.Y;

            if (!_isGrounded)
            {
                _velocity.Y += 1.16f;
            }
            else
            {
                _velocity.X *= 0.9f;
            }
        }

        public void Draw(SpriteBatch s, Color col)
        {
            s.Draw(_texture, _rectangle, col);
        }


    }
}