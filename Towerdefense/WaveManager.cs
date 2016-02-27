using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    public class WaveManager
    {
        private int numberOfWaves;
        private float timeSinceLastWave;

        private Queue<Wave> waves = new Queue<Wave>();

        private Texture2D[] enemytexture;

        private bool waveFinished = false;

        private Level level;

        private int levelcount;

        private Player player;

        public Wave CurrentWave
        {
            get { return waves.Peek(); }
        }

        public List<Enemy> Enemies
        {
            get { return CurrentWave.Enemies; }
        }

        public int Round
        {
            get { return CurrentWave.RoundNumber + 1; }
        }

        public int NumberOfWaves
        {
            get { return numberOfWaves; }
        }

        public WaveManager(Level level, int numberOfWaves, Texture2D[] enemytexture, int levelcount, Player player, Texture2D healthtexture)
        {
            this.numberOfWaves = numberOfWaves;
            this.enemytexture = enemytexture;
            this.player = player;
            this.level = level;
            this.levelcount = levelcount;

            for (int i = 0; i < numberOfWaves; i++)
            {
                int initialNumberOfEnemies = 30;
                int numberModifier = (i / 30) + 1;

                Wave wave = new Wave(i, initialNumberOfEnemies * numberModifier, level, enemytexture, levelcount, player, healthtexture);
                waves.Enqueue(wave);
            }

            StartNextWave();
        }

        private void StartNextWave()
        {
            if (waves.Count > 0)
            {
                waves.Peek().Start();

                timeSinceLastWave = 0;
                waveFinished = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentWave.Update(gameTime);

            if (CurrentWave.RoundOver)
            {
                waveFinished = true;
            }

            if (waveFinished)
            {
                timeSinceLastWave += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timeSinceLastWave > 5.0f)
            {
                waves.Dequeue();
                StartNextWave();
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            CurrentWave.Draw(spritebatch);
        }
    }
}
