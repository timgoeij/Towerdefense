using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense.GUI
{
    public class Button : Sprite
    {
        public enum buttonstatus
        {
            Normal,
            Hover,
            Pressed
        }

        private MouseState previousState;

        private Texture2D hoverTexture;
        private Texture2D pressedTexture;

        private Rectangle bounds;
        private buttonstatus state = buttonstatus.Normal;

        public event EventHandler clicked;

        public Button(Texture2D texture, Texture2D hover, Texture2D pressed, Vector2 position)
            : base(texture, position)
        {
            this.hoverTexture = hover;
            this.pressedTexture = pressed;

            this.bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mousestate = Mouse.GetState();
            int mouseX = mousestate.X;
            int mouseY = mousestate.Y;

            bool isMouseOver = bounds.Contains(mouseX, mouseY);

            if (isMouseOver && state != buttonstatus.Pressed)
            {
                state = buttonstatus.Hover;
            }
            else if (!isMouseOver && state != buttonstatus.Pressed)
            {
                state = buttonstatus.Normal;
            }

            if (mousestate.LeftButton == ButtonState.Pressed &&
                previousState.LeftButton == ButtonState.Released)
            {
                if (isMouseOver)
                {
                    state = buttonstatus.Pressed;
                }

            }

            if (mousestate.LeftButton == ButtonState.Released &&
                previousState.LeftButton == ButtonState.Pressed)
            {
                if (isMouseOver)
                {
                    state = buttonstatus.Hover;

                    if (clicked != null)
                    {
                        clicked(this, EventArgs.Empty);
                    }
                }
                else if (state == buttonstatus.Pressed)
                {
                    state = buttonstatus.Normal;
                }
            }

            previousState = mousestate;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case buttonstatus.Normal:
                    spriteBatch.Draw(texture, bounds, Color.White);
                    break;
                case buttonstatus.Hover:
                    spriteBatch.Draw(hoverTexture, bounds, Color.White);
                    break;
                case buttonstatus.Pressed:
                    spriteBatch.Draw(pressedTexture, bounds, Color.White);
                    break;
                default:
                    spriteBatch.Draw(texture, bounds, Color.White);
                    break;
            }
        }
    }
}
