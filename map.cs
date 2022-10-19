using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace compsci_NEA
{
    class map
    {
        private OpenSimplexNoise _accessSimplex;
        private Texture2D _background;

        const int _blockSize = 25;
        const int _mapWidth = 60;
        const int _mapHeight = 60;

        Texture2D _stone;
        private blockType[] _map;

        enum blockType
        {
            sky,
            stone,
            dirt,
        };

        public map(Texture2D background, Texture2D stone)
        {
            // TODO: Add your initialization logic here

            _background = background;
            _stone = stone;

            _accessSimplex = new OpenSimplexNoise();

            float scale = 3f;
            float noiseValue;
            _map = new blockType[_mapWidth * _mapHeight];

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    //get noise value at scale
                    noiseValue = (float)_accessSimplex.Evaluate(x / scale, y / scale);

                    if (noiseValue < 0.12)
                    {
                        _map[(y * _mapWidth) + x] = blockType.stone;
                    }
                    else
                    {
                        _map[(y * _mapWidth) + x] = blockType.sky;
                    }
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_background, new Rectangle(0, 0, 800, 800), Color.Red);

            for (int i = 0; i < _map.Length; i++)
            {
                int y = (i / _mapWidth) * _blockSize;
                int x = (i % _mapWidth) * _blockSize;

                if (_map[i] == blockType.stone)
                {
                    _spriteBatch.Draw(_stone, new Rectangle(x, y, _blockSize, _blockSize), Color.White);
                }
            }
        }

        public bool IsColliding(Rectangle rect)
        {
            for (int x = rect.X; x < rect.X + rect.Width; x++)
            {
                for (int y = rect.Y; y < rect.Y + rect.Height; y++)
                {
                    int currentBlock = ((y / _blockSize) * _mapWidth) + (x / _blockSize);
                    if (_map[currentBlock] != blockType.sky)
                    {
                        if (new Rectangle(x, y, _blockSize, _blockSize).Intersects(rect))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //DELETE THIS PLEASE IT IS BETA DELETE THIS PLEASE IT IS BETA DELETE THIS PLEASE IT IS BETA DELETE THIS PLEASE IT IS BETA DELETE THIS PLEASE IT IS BETA
        public bool Kill(Rectangle rect)
        {
            for (int i = 0; i < _map.Length; i++)
            {
                int y = (i / _mapWidth) * _blockSize;
                int x = (i % _mapWidth) * _blockSize;

                if (_map[i] != blockType.sky)
                {
                    if (new Rectangle(x, y, _blockSize, _blockSize).Intersects(rect))
                    {
                        _map[i] = blockType.sky;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
