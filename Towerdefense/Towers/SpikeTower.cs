using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense.Towers
{
    public class SpikeTower : Tower
    {
        private Vector2[] directions = new Vector2[8];

        private List<Enemy> targets = new List<Enemy>();

        public override bool hasTarget
        {
            get { return false; }
        }

        public SpikeTower(Texture2D texture, Texture2D bullet, Vector2 position)
           : base(texture, bullet, position)
        {
            this.damage = 20;
            this.cost = 40;
            this.radius = 48;

            directions = new Vector2[]
            {
                new Vector2(-1, -1),
                new Vector2(0, -1),
                new Vector2(1, -1),
                new Vector2(-1, 0),
                new Vector2(1, 0),
                new Vector2(-1, 1),
                new Vector2(0, 1),
                new Vector2(1, 1),
            };
        }

        public override void GetClosestEnemy(List<Enemy> enemies)
        {
            targets.Clear();

            foreach (Enemy enemy in enemies)
            {
                if (IsInRange(enemy.Center))
                {
                    targets.Add(enemy);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (bulletTimer >= 1.0f && targets.Count != 0)
            {
                for (int i = 0; i < directions.Length; i++)
                {
                    Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center,
                        new Vector2(bulletTexture.Width / 2)), directions[i], 6, damage);
                    bulletList.Add(bullet);
                }

                bulletTimer = 0;
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];
                bullet.Update(gameTime);

                if (!IsInRange(bullet.Center))
                {
                    bullet.Kill();
                }

                for (int t = 0; t < targets.Count; t++)
                {
                    if (targets[t] != null && Vector2.Distance(bullet.Center, targets[t].Center) < 12)
                    {
                        targets[t].CurrentHealth -= bullet.Damage;
                        bullet.Kill();
                        break;
                    }
                }
                if (bullet.IsDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }
            }
        }
    }
}
