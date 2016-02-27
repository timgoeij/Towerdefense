using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    public class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spritebatch;

        enum bstate
        {
            Hover, Up, JustReleased, Down
        }

        const int NumberofButtons = 3, Level1button = 0, Level2button = 1, Level3button = 2, buttonHeight = 75, buttonWidth = 75;

        Rectangle[] buttonrects = new Rectangle[NumberofButtons];
        Texture2D[] buttontext = new Texture2D[NumberofButtons];
        bstate[] buttonstate = new bstate[NumberofButtons];
        Color[] texcolors = new Color[NumberofButtons];
        double[] ButtonTimer = new double[NumberofButtons];
        bool pressed, prevpressed = false;
        int mx, my;
        double frame_time;

        List<Texture2D> levels = new List<Texture2D>();
        Texture2D background;

        public int levelcount = 0;

        public Menu(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            int x = 50;
            int y = 25;

            for (int i = 0; i < NumberofButtons; i++)
            {
                buttonstate[i] = bstate.Up;
                ButtonTimer[i] = 0.0;
                buttonrects[i] = new Rectangle(x, y, buttonWidth, buttonHeight);
                texcolors[i] = Color.White;
                x += buttonWidth * 2;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spritebatch = new SpriteBatch(GraphicsDevice);

            Texture2D level1 = Game.Content.Load<Texture2D>("Level1");
            Texture2D level2 = Game.Content.Load<Texture2D>("Level2");
            Texture2D level3 = Game.Content.Load<Texture2D>("Level3");

            levels.Add(level1);
            levels.Add(level2);
            levels.Add(level3);

            buttontext[Level1button] = Game.Content.Load<Texture2D>("Menu\\Level1");
            buttontext[Level2button] = Game.Content.Load<Texture2D>("Menu\\Level2");
            buttontext[Level3button] = Game.Content.Load<Texture2D>("Menu\\Level3");


            background = Game.Content.Load<Texture2D>("Menu\\backgrounds");

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gametime)
        {
            // TODO: Add your update code here
            frame_time = gametime.ElapsedGameTime.Milliseconds / 1000;

            MouseState mousestate = Mouse.GetState();
            mx = mousestate.X;
            my = mousestate.Y;

            prevpressed = pressed;
            pressed = mousestate.LeftButton == ButtonState.Pressed;

            updatebuttons();
        }

        public void drawbackground()
        {
            spritebatch.Begin();

            spritebatch.Draw(background, new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.White);

            for (int i = 0; i < NumberofButtons; i++)
            {
                spritebatch.Draw(buttontext[i], buttonrects[i], texcolors[i]);
            }

            spritebatch.Draw(levels[levelcount], new Rectangle(Game.Window.ClientBounds.Width / 4, Game.Window.ClientBounds.Height / 4,
                Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2), Color.White);

            spritebatch.End();
        }

        Boolean hitImageAlpha(Rectangle rect, Texture2D tex, int x, int y)
        {
            return hitImageAlpha(0, 0, tex, tex.Width * (x - rect.X) / rect.Width,
                tex.Height * (y - rect.Y) / rect.Height);
        }

        Boolean hitImageAlpha(float tx, float ty, Texture2D tex, int x, int y)
        {
            if (hitImage(tx, ty, tex, x, y))
            {
                uint[] data = new uint[tex.Width * tex.Height];
                tex.GetData<uint>(data);

                if ((x - (int)tx + (y - (int)ty) *
                    tex.Width < tex.Width * tex.Height))
                {
                    return ((data[(x - (int)tx) + (y - (int)ty) * tex.Width] &
                        0xFF000000) >> 24) > 20;
                }

            }
            return false;
        }

        Boolean hitImage(float tx, float ty, Texture2D tex, int x, int y)
        {
            return (x >= tx && x <= tx + tex.Width && y >= ty && y <= ty + tex.Height);
        }

        void updatebuttons()
        {
            for (int i = 0; i < NumberofButtons; i++)
            {
                if (hitImageAlpha(buttonrects[i], buttontext[i], mx, my))
                {
                    ButtonTimer[i] = 0.0;

                    if (pressed)
                    {
                        buttonstate[i] = bstate.Down;
                        texcolors[i] = Color.Blue;
                    }
                    else if (!pressed && prevpressed)
                    {
                        if (buttonstate[i] == bstate.Down)
                        {
                            buttonstate[i] = bstate.JustReleased;
                        }
                    }
                    else
                    {
                        buttonstate[i] = bstate.Hover;
                        texcolors[i] = Color.LightBlue;
                    }
                }
                else
                {
                    buttonstate[i] = bstate.Up;

                    if (ButtonTimer[i] > 0)
                    {
                        ButtonTimer[i] = ButtonTimer[i] - frame_time;
                    }
                    else
                    {
                        texcolors[i] = Color.White;
                    }
                }

                if (buttonstate[i] == bstate.JustReleased)
                {
                    actionbutton(i);
                }
            }
        }
        void actionbutton(int i)
        {
            switch (i)
            {
                case Level1button:
                    levelcount = 0;
                    break;
                case Level2button:
                    levelcount = 1;
                    break;
                case Level3button:
                    levelcount = 2;
                    break;
                default:
                    break;
            }
        }
    }
}
