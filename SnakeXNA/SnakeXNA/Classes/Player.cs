using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SnakeXNA.Classes
{
    public class Player : GameObject
    {
        public Vector2 Direction;
        public Vector2 PreviousPosition;
        public int Speed;
        public int LeapDistance;
        public int Length = 1;

        public Player(Texture2D texture, Vector2 position, int dimensions, int speed) : base(texture, position)
        {
            Speed = speed;
            LeapDistance = dimensions;
            Collision = new Rectangle((int)Position.X, (int)Position.Y, dimensions, dimensions);
            Direction.X = 1;
        }

        public override void Update(GameTime gameTime)
        {
            if (Position.X < 0)
                Position.X = MainGame.Instance.GraphicsDevice.Viewport.Width - LeapDistance;

            if (Position.X > MainGame.Instance.GraphicsDevice.Viewport.Width - LeapDistance)
                Position.X = 0;

            if (Position.Y < 0)
                Position.Y = MainGame.Instance.GraphicsDevice.Viewport.Height - LeapDistance;

            if (Position.Y > MainGame.Instance.GraphicsDevice.Viewport.Height - LeapDistance)
                Position.Y = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Collision, Color.Green);
        }
    }
}
