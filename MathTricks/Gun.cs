using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MathTricks
{
    public class Gun
    {
        private Texture2D barrel;
        private int barrelPos = 8;
        private int gunPos = 8;
        private Texture2D gun;
        private float cockTime = 0f;
        private float maxCockTime = 0.10f;
        
        public Gun(Texture2D barrel, Texture2D gun)
        {
            this.barrel = barrel;
            this.gun = gun;
        }

        public void Cock()
        {
            cockTime = maxCockTime;
        }

        public void Update(GameTime gt)
        {
            float dt = (float)gt.ElapsedGameTime.TotalSeconds;

            if(cockTime > 0)
            {
                cockTime -= dt;
                barrelPos = 12;
                gunPos = 10;
            }
            else
            {
                barrelPos = 8;
                gunPos = 8;
            }
        }

        public void Draw(SpriteBatch spr, float angle, bool flip)
        {
            if (flip)
            {
                spr.Draw(gun, new Vector2(640, 360), null, Color.White, angle, new Vector2(gunPos, 8), 10f, SpriteEffects.FlipVertically, 0f);
                spr.Draw(barrel, new Vector2(640, 360), null, Color.White, angle, new Vector2(barrelPos, 8), 10f, SpriteEffects.FlipVertically, 0f);
            }
            else
            {
                spr.Draw(gun, new Vector2(640, 360), null, Color.White, angle, new Vector2(gunPos, 8), 10f, SpriteEffects.None, 0f);
                spr.Draw(barrel, new Vector2(640, 360), null, Color.White, angle, new Vector2(barrelPos, 8), 10f, SpriteEffects.None, 0f);
            }
        }
    }
}
