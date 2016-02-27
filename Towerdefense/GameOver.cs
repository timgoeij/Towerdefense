using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    public class GameOver : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spritebatch;
        private SpriteFont GameOverfont;
        private SpriteFont normal;

        public enum state { Level1, Level2, Level3 }

        public state State;

        private Texture2D background;

        public int getround;

        public int lives;

        public int money;

        public GameOver(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spritebatch = new SpriteBatch(GraphicsDevice);

            background = Game.Content.Load<Texture2D>("backgrounds");
            GameOverfont = Game.Content.Load<SpriteFont>("Arial");
            normal = Game.Content.Load<SpriteFont>("Arial");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public void Drawback()
        {
            spritebatch.Begin();

            spritebatch.Draw(background, new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.White);

            spritebatch.DrawString(GameOverfont, "GAMEOVER", new Vector2(Game.Window.ClientBounds.Width / 4, 50), Color.White);

            if (State == state.Level1)
                spritebatch.DrawString(normal, "you did level 1", new Vector2(25, 150), Color.White);
            else if (State == state.Level2)
                spritebatch.DrawString(normal, "you did level 2", new Vector2(25, 150), Color.White);
            else if (State == state.Level3)
                spritebatch.DrawString(normal, "you did level 3", new Vector2(25, 150), Color.White);

            string[] strings = new string[]
            {
                string.Format("you survived {0} rounds",getround),
                string.Format("you have {0} money left", money),
                string.Format("you have {0} lives left", lives)
            };
            Vector2 position = new Vector2(25, 200);

            for (int i = 0; i < strings.Length; i++)
            {
                spritebatch.DrawString(normal, strings[i], position, Color.White);
                position.Y += 50;
            }

            spritebatch.End();

        }
    }
}
