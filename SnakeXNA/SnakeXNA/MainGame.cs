using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeXNA.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SnakeXNA
{
    public class MainGame : Game
    {
        private Player player;
        private Apple apple;
        private List<Tail> tailList;
        private int fixedDimensions;
        private int movementTimer;
        private SpriteFont stdFont;
        private Texture2D playerTexture;
        public static MainGame Instance;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public MainGame()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            fixedDimensions = 20;
            movementTimer = 0;
            tailList = new List<Tail>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("white-square");
            stdFont = Content.Load<SpriteFont>("std-font");

            player = new Player(playerTexture, new Vector2(fixedDimensions, 0), fixedDimensions, 5);
            apple = new Apple(playerTexture, Vector2.Zero, fixedDimensions);

            for (int i = 0; i < player.Length; i++)
            {
                tailList.Add(new Tail(playerTexture, Vector2.Zero, fixedDimensions, player.Speed));
            }

            apple.SetRandomPosition();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void HandleInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                player.Direction.X = 0;
                player.Direction.Y = -1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                player.Direction.X = 0;
                player.Direction.Y = 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.Direction.X = -1;
                player.Direction.Y = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.Direction.X = 1;
                player.Direction.Y = 0;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            #region Movement
            HandleInput();

            movementTimer++;

            if (movementTimer < player.Speed)
            {
                player.PreviousPosition = player.Position;

                for (int i = 0; i < tailList.Count; i++)
                {
                    tailList[i].PreviousPosition = tailList[i].Position;
                }
            }
            else
            {
                player.Position += player.Direction * player.LeapDistance;
                tailList[0].Position = player.PreviousPosition;

                for (int i = 1; i < tailList.Count; i++)
                {
                    tailList[i].Position = tailList[i - 1].PreviousPosition;
                }

                movementTimer = 0;
            }
            #endregion

            if (player.Position == apple.Position)
            {
                player.Length++;
                tailList.Add(new Tail(playerTexture, player.PreviousPosition, fixedDimensions, player.Speed));
                apple.SetRandomPosition();
            }

            for (int currentTailIndex = 0; currentTailIndex < tailList.Count; currentTailIndex++)
            {
                tailList[currentTailIndex].Update(gameTime);

                if (tailList[currentTailIndex].Position == player.Position)
                {
                    for (int removeIndex = tailList.Count - 1; removeIndex >= currentTailIndex; removeIndex--)
                    {
                        tailList.RemoveAt(removeIndex);
                    }

                    // Adjusting the displayed length
                    player.Length = 0;

                    for (int k = 0; k < tailList.Count; k++)
                    {
                        player.Length++;
                    }
                }
            }

            player.Update(gameTime);
            apple.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            player.Draw(spriteBatch);
            apple.Draw(spriteBatch);

            for (int i = 0; i < tailList.Count; i++)
            {
                tailList[i].Draw(spriteBatch);
            }

            spriteBatch.DrawString(stdFont, $"Length: {player.Length + 1}", new Vector2(10, 10), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}