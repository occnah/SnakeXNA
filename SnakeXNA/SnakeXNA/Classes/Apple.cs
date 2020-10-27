using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeXNA.Classes
{
    public class Apple : GameObject
    {
        private int textureWidth;

        public Apple(Texture2D texture, Vector2 position, int dimensions) : base(texture, position)
        {
            Collision = new Rectangle((int)Position.X, (int)Position.Y, dimensions, dimensions);
            textureWidth = dimensions;
        }

        public void SetRandomPosition()
        {
            Random rng = new Random();

            int screenWidth = MainGame.Instance.GraphicsDevice.Viewport.Width / textureWidth;
            int screenHeight = MainGame.Instance.GraphicsDevice.Viewport.Height / textureWidth;

            Position.X = textureWidth * rng.Next(1, screenWidth);
            Position.Y = textureWidth * rng.Next(1, screenHeight);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Collision, Color.Red);
        }
    }
}
