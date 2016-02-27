using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    public class Wave
    {
        private int numOfEnemies;
        private int waveNumber;
        private Random random = new Random();

        private float spawnTimer = 0;
        private int enemiesSpawned = 0;

        private bool enemyAtend;
        private bool spawningEnemies;

        private Level level;
        private Player player;

        private Texture2D[] enemyTexture;

        private List<Enemy> enemies = new List<Enemy>();

        private int levelcount;

        private Texture2D healthTexture;

        public bool RoundOver
        {
            get { return enemies.Count == 0 && enemiesSpawned == numOfEnemies; }
        }

        public int RoundNumber
        {
            get { return waveNumber; }
        }

        public bool EnemyAtEnd
        {
            get { return enemyAtend; }
            set { enemyAtend = value; }
        }

        public List<Enemy> Enemies
        {
            get { return enemies; }
        }

        public Wave(int waveNumber, int numOfEnemies, Level level, Texture2D[] enemyTexture, int levelcount, Player player, Texture2D healthtexture)
        {
            this.waveNumber = waveNumber;
            this.numOfEnemies = numOfEnemies;
            this.player = player;
            this.level = level;
            this.enemyTexture = enemyTexture;
            this.levelcount = levelcount;
            this.healthTexture = healthtexture;
        }

        private void AddEnemy()
        {
            if (levelcount == 0)
            {
                Enemy enemy = new Enemy(enemyTexture[random.Next(5)], level.WaypointsMap1.Peek(), 50 + (float)random.NextDouble() * 100, 1, (float)random.NextDouble());
                enemy.setWaypoints(level.WaypointsMap1);
                enemies.Add(enemy);
            }
            else if (levelcount == 1)
            {
                Enemy enemy = new Enemy(enemyTexture[random.Next(5)], level.WaypointsMap2.Peek(), 50 + (float)random.NextDouble() * 100, 1, (float)random.NextDouble());
                enemy.setWaypoints(level.WaypointsMap2);
                enemies.Add(enemy);
            }
            else if (levelcount == 2)
            {
                int kies = random.Next(2);

                if (kies == 0)
                {
                    Enemy enemy = new Enemy(enemyTexture[random.Next(5)], level.WaypointsMap31.Peek(), 50 + (float)random.NextDouble() * 100, 1, (float)random.NextDouble());
                    enemy.setWaypoints(level.WaypointsMap31);
                    enemies.Add(enemy);
                }
                else if (kies == 1)
                {
                    Enemy enemy = new Enemy(enemyTexture[random.Next(5)], level.WaypointsMap32.Peek(), 50 + (float)random.NextDouble() * 100, 1, (float)random.NextDouble());
                    enemy.setWaypoints(level.WaypointsMap32);
                    enemies.Add(enemy);
                }
            }
            spawnTimer = 0;
            enemiesSpawned++;
        }

        public void Start()
        {
            spawningEnemies = true;
        }

        public void Update(GameTime gameTime)
        {
            if (enemiesSpawned == numOfEnemies)
                spawningEnemies = false;

            if (spawningEnemies)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (spawnTimer > 2)
                    AddEnemy();
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                enemy.Update(gameTime);

                if (enemy.IsDead)
                {
                    if (enemy.CurrentHealth > 0)
                    {
                        enemyAtend = true;
                        player.Lives -= 1;
                    }
                    else
                        player.Money += enemy.BountyGiven;

                    enemies.Remove(enemy);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);

                Rectangle healthrectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, healthTexture.Width, healthTexture.Height);
                spriteBatch.Draw(healthTexture, healthrectangle, Color.Gray);

                float healtpercentage = enemy.healthpercentage;
                float visiblewidth = (float)healthTexture.Width * healtpercentage;

                float red = (healtpercentage < 0.5 ? 1 : 1 - (2 * healtpercentage - 1));
                float green = (healtpercentage > 0.5 ? 1 : (2 * healtpercentage));
                Color healthcolor = new Color(red, green, 0);

                healthrectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, (int)(visiblewidth), healthTexture.Height);
                spriteBatch.Draw(healthTexture, healthrectangle, healthcolor);
            }
        }
    }
}
