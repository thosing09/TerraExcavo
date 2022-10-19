using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace compsci_NEA
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private entity _character;

        private map _map;

        private Color _saulColor;
        private Texture2D _ballMan;
        private Rectangle _ballManRect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _map = new map(Content.Load<Texture2D>("ballsoodman"), Content.Load<Texture2D>("stone"));
            _character = new entity(new Rectangle(50, 100, 50, 75), Content.Load<Texture2D>("mario"), 800);

            _saulColor = Color.White;

            _ballManRect = new Rectangle(20,20, 15,15);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _ballMan = Content.Load<Texture2D>("ballsoodman");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _ballManRect.Y--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _ballManRect.Y++;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _ballManRect.X--;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _ballManRect.X++;

            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _map.Kill(_ballManRect);
            }

            if (_map.IsColliding(_ballManRect))
            {
                _saulColor = Color.Red;
            }
            else
            {
                _saulColor = Color.White;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointWrap);           
            _character.Draw(_spriteBatch, Color.White);
            _map.Draw(_spriteBatch);

            _spriteBatch.Draw(_ballMan, _ballManRect, _saulColor);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}