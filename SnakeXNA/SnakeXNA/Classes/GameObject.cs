using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeXNA.Classes
{
    public abstract class GameObject
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Rectangle Collision;

        public GameObject(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Collision, Color.White);
        }
    }
}
