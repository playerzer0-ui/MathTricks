using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MathTricks
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont font;
        private float rotationAngle = 0;
        private Vector2 distance = new Vector2();
        private float circle;
        private Vector2 mousePosition = new Vector2();
        private MouseState oldState;
        private List<Bullet> bullets;
        private Gun gunObj;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("File");

            gunObj = new Gun(Content.Load<Texture2D>("gun_barrel"), Content.Load<Texture2D>("gun"));

            bullets = new List<Bullet>();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState mState = Mouse.GetState();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here
            //rotationAngle += elapsed;
            //circle = MathHelper.Pi * 2;
            //rotationAngle %= circle;

            mousePosition.X = mState.Position.X;
            mousePosition.Y = mState.Position.Y;

            distance.X = mousePosition.X - 640;
            distance.Y = mousePosition.Y - 360;

            rotationAngle = (float)Math.Atan2(distance.Y, distance.X);

            if (mState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                bullets.Add(new Bullet(Content.Load<Texture2D>("bullet"), distance));
                gunObj.Cock();
            }

            foreach(Bullet bullet in bullets)
            {
                bullet.Update(gameTime);
            }

            bullets.RemoveAll(bullet => bullet.Die);

            oldState = mState;

            gunObj.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            foreach(Bullet bullet in bullets)
            {
                bullet.Draw(_spriteBatch);
            }

            if (distance.X < 0)
            {
                gunObj.Draw(_spriteBatch, rotationAngle, true);
            }
            else
            {
                gunObj.Draw(_spriteBatch, rotationAngle, false);
            }
            _spriteBatch.DrawString(font, "rotAngle:" + rotationAngle, new Vector2(200, 200), Color.Black);
            _spriteBatch.DrawString(font, "flipAngle:" + circle, new Vector2(200, 220), Color.Black);
            _spriteBatch.DrawString(font, "distance:" + new Vector2(mousePosition.X - 640, mousePosition.Y - 360), new Vector2(200, 260), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
