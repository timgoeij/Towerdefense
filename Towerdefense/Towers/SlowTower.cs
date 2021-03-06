﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Towerdefense.Towers
{
    public class SlowTower : Tower
    {
        private float speedModifier;
        private float modifierDuration;

        public SlowTower(Texture2D texture, Texture2D bulletTexture, Vector2 position)
            : base(texture, bulletTexture, position)
        {
            this.damage = 15;
            this.cost = 15;
            this.speedModifier = 0.6f;
            this.modifierDuration = 2.0f;
            this.radius = 80;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (bulletTimer >= 0.75f && target != null)
            {
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center, new Vector2(bulletTexture.Width / 2)), rotation, 6, damage);

                bulletList.Add(bullet);

                bulletTimer = 0;
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];

                bullet.SetRotation(rotation);
                bullet.Update(gameTime);

                if (!IsInRange(bullet.Center))
                    bullet.Kill();

                if (target != null && Vector2.Distance(bullet.Center, target.Center) < 12)
                {
                    if (target.SpeedModifier <= speedModifier)
                    {
                        target.SpeedModifier = speedModifier;
                        target.ModifierDuration = modifierDuration;
                    }

                    target.CurrentHealth -= bullet.Damage;
                    bullet.Kill();
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
