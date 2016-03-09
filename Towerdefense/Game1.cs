using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Towerdefense.GUI;

namespace Towerdefense
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Menu menu;
        GameOver gameover;
        Level level = new Level();
        Player player;
        WaveManager wavemanager;
        Toolbar toolbar;
        Button arrowbutton;
        Button spikebutton;
        Button slowbutton;

        Texture2D[] Enemytexture;
        Texture2D[] towertexture;
        Texture2D bullettexture;
        Texture2D grass;
        Texture2D way;
        Texture2D waymap2;
        Texture2D ground;
        Texture2D sand;
        Texture2D water;
        Texture2D healthtexture;

        enum GameState { Menu, Level1, Level2, Level3, GameOver };

        GameState currentState = GameState.Menu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            menu = new Menu(this);
            Components.Add(menu);

            gameover = new GameOver(this);
            Components.Add(gameover);

            graphics.PreferredBackBufferHeight = 30 + level.height * 30;
            graphics.PreferredBackBufferWidth = level.width * 30;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            grass = Content.Load<Texture2D>("Maps\\Level1\\grass");
            way = Content.Load<Texture2D>("Maps\\Level1\\way");
            waymap2 = Content.Load<Texture2D>("Maps\\Level2\\wayred");
            ground = Content.Load<Texture2D>("Maps\\Level2\\ground");
            sand = Content.Load<Texture2D>("Maps\\Level3\\sand");
            water = Content.Load<Texture2D>("Maps\\Level3\\water");
            healthtexture = Content.Load<Texture2D>("health bar");
            Enemytexture = new Texture2D[]
            {
                Content.Load<Texture2D>("Enemy\\Enemy"),
                Content.Load<Texture2D>("Enemy\\Enemy2"),
                Content.Load<Texture2D>("Enemy\\Enemy3"),
                Content.Load<Texture2D>("Enemy\\Enemy4"),
                Content.Load<Texture2D>("Enemy\\Enemy5"),
            };


            towertexture = new Texture2D[]
            {
                Content.Load<Texture2D>("Towers\\arrow tower"),
                Content.Load<Texture2D>("Towers\\spike tower"),
                Content.Load<Texture2D>("Towers\\slow tower")
            };


            bullettexture = Content.Load<Texture2D>("Towers\\bullet");

            Texture2D topbar = Content.Load<Texture2D>("GUI\\tool bar");
            SpriteFont font = Content.Load<SpriteFont>("Arial");

            Texture2D arrowhover = Content.Load<Texture2D>("GUI\\arrow-hover");
            Texture2D arrowpressed = Content.Load<Texture2D>("GUI\\arrow pressed");
            Texture2D spikehover = Content.Load<Texture2D>("GUI\\spike hover");
            Texture2D spikepressed = Content.Load<Texture2D>("GUI\\spike pressed");
            Texture2D slowhover = Content.Load<Texture2D>("GUI\\slow hover");
            Texture2D slowpressed = Content.Load<Texture2D>("GUI\\slow pressed");

            toolbar = new Toolbar(topbar, font, new Vector2(0, level.height * 30));

            arrowbutton = new Button(towertexture[0], arrowhover, arrowpressed,
                new Vector2(210, level.height * 30));

            arrowbutton.clicked += new EventHandler(arrowbutton_clicked);

            spikebutton = new Button(towertexture[1], spikehover, spikepressed,
                new Vector2(295, level.height * 30));

            spikebutton.clicked += new EventHandler(spikebutton_clicked);

            slowbutton = new Button(towertexture[2], slowhover, slowpressed,
                new Vector2(375, level.height * 30));

            slowbutton.clicked += new EventHandler(slowbutton_clicked);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (currentState == GameState.Menu)
            {
                menu.Update(gameTime);

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (menu.levelcount == 0)
                    {
                        level.AddTexture(grass, way);
                        currentState = GameState.Level1; player = new Player(level, menu.levelcount, bullettexture, towertexture);
                        wavemanager = new WaveManager(level, 20, Enemytexture, menu.levelcount, player, healthtexture);
                    }
                    else if (menu.levelcount == 1)
                    {
                        level.AddTexture(ground, waymap2);
                        currentState = GameState.Level2; player = new Player(level, menu.levelcount, bullettexture, towertexture);
                        wavemanager = new WaveManager(level, 25, Enemytexture, menu.levelcount, player, healthtexture);
                    }
                    else if (menu.levelcount == 2)
                    {
                        level.AddTexture(water, sand);
                        currentState = GameState.Level3; player = new Player(level, menu.levelcount, bullettexture, towertexture);
                        wavemanager = new WaveManager(level, 30, Enemytexture, menu.levelcount, player, healthtexture);
                    }

                    level.initLevel(menu.levelcount);
                }
            }
            else if (currentState == GameState.Level1)
            {
                if (wavemanager.Round != wavemanager.NumberOfWaves && player.Lives != 0)
                {
                    wavemanager.Update(gameTime);
                    player.Update(gameTime, wavemanager.Enemies);
                    arrowbutton.Update(gameTime);
                    spikebutton.Update(gameTime);
                    slowbutton.Update(gameTime);
                }
                else
                {
                    gameover.State = GameOver.state.Level1;
                    gameover.getround = wavemanager.Round;
                    gameover.lives = player.Lives;
                    gameover.money = player.Money;
                    currentState = GameState.GameOver;
                }
            }
            else if (currentState == GameState.Level2)
            {
                if (wavemanager.Round != wavemanager.NumberOfWaves && player.Lives != 0)
                {
                    wavemanager.Update(gameTime);
                    player.Update(gameTime, wavemanager.Enemies);
                    arrowbutton.Update(gameTime);
                    spikebutton.Update(gameTime);
                    slowbutton.Update(gameTime);
                }
                else
                {
                    gameover.State = GameOver.state.Level2;
                    gameover.getround = wavemanager.Round;
                    gameover.lives = player.Lives;
                    gameover.money = player.Money;
                    currentState = GameState.GameOver; }
            }
            else if (currentState == GameState.Level3)
            {
                if (wavemanager.Round != wavemanager.NumberOfWaves && player.Lives != 0)
                {
                    wavemanager.Update(gameTime);
                    player.Update(gameTime, wavemanager.Enemies);
                    arrowbutton.Update(gameTime);
                    spikebutton.Update(gameTime);
                    slowbutton.Update(gameTime);
                }
                else
                {
                    gameover.State = GameOver.state.Level3;
                    gameover.getround = wavemanager.Round;
                    gameover.lives = player.Lives;
                    gameover.money = player.Money;
                    currentState = GameState.GameOver; }
            }
            else if (currentState == GameState.GameOver)
            {
                gameover.Update(gameTime);

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    level.maptextures = new List<Texture2D>();
                    currentState = GameState.Menu;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (currentState == GameState.Menu)
                menu.drawbackground();
            else if (currentState == GameState.GameOver)
            {
                gameover.Drawback();
            }
            else
            {
                level.Draw(spriteBatch);
                wavemanager.Draw(spriteBatch);
                player.Draw(spriteBatch);
                toolbar.Draw(spriteBatch, player);
                arrowbutton.Draw(spriteBatch);
                spikebutton.Draw(spriteBatch);
                slowbutton.Draw(spriteBatch);
            }
          
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void arrowbutton_clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Arrow Tower";
        }

        private void spikebutton_clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Spike Tower";
        }

        private void slowbutton_clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Slow Tower";
        }
    }
}