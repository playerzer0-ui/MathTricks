using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MathTricks
{
    public class Bullet
    {
        private Vector2 pos;
        private Vector2 distance;
        private float rotationAngle;
        private int speed = 1200;
        private Texture2D bulletSprite;
        private bool die = false;

        public bool Die { get => die; set => die = value; }

        public Bullet(Texture2D bulletSprite, Vector2 distance)
        {
            this.bulletSprite = bulletSprite;
            this.distance = distance;
            pos = new Vector2(640, 360);
        }

        public void Update(GameTime gt)
        {
            float dt = (float)gt.ElapsedGameTime.TotalSeconds;

            rotationAngle = (float)Math.Atan2(distance.Y, distance.X);
            if (distance != Vector2.Zero) distance.Normalize();

            pos += distance * dt * speed;

            if(pos.X > 1400 || pos.X < -100 || pos.Y > 800 || pos.Y < -100)
            {
                Die = true;
            }
        }

        public void Draw(SpriteBatch spr)
        {
            spr.Draw(bulletSprite, pos, null, Color.White, rotationAngle, new Vector2(-28, 4), 2f, SpriteEffects.None, 0f);
        }
    }
}
