using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    public class Enemy : Sprite
    {
        private Queue<Vector2> waypoints = new Queue<Vector2>();

        protected float startHealth;
        protected float currentHealth;

        protected bool alive = true;

        protected float speed = 0.5f;
        protected int bountyGiven;

        private float speedModifier;
        private float modifierDuration;
        private float modifiercurrentTime;

        public float SpeedModifier
        {
            get { return speedModifier; }
            set { speedModifier = value; }
        }

        public float ModifierDuration
        {
            get { return modifierDuration; }
            set { modifierDuration = value; modifiercurrentTime = 0; }
        }

        public float CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public float healthpercentage
        {
            get { return currentHealth / startHealth; }
        }

        public bool IsDead
        {
            get { return currentHealth <= 0 || alive == false; }
        }
        public int BountyGiven
        {
            get { return bountyGiven; }
        }

        public float DistanceToDestination
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        public Enemy(Texture2D texture, Vector2 position, float health, int bountyGiven, float speed)
            : base(texture, position)
        {
            this.startHealth = health;
            this.currentHealth = startHealth;

            this.bountyGiven = bountyGiven;
            this.speed = speed;
        }

        public void setWaypoints(Queue<Vector2> waypoints)
        {
            foreach (Vector2 waypoint in waypoints)
                this.waypoints.Enqueue(waypoint);

            this.position = this.waypoints.Dequeue();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (waypoints.Count > 0)
            {
                if (DistanceToDestination < 1f)
                {
                    position = waypoints.Peek();
                    waypoints.Dequeue();
                }

                else
                {
                    Vector2 direction = waypoints.Peek() - position;
                    direction.Normalize();

                    float temporaryspeed = speed;

                    if (modifiercurrentTime > modifierDuration)
                    {
                        speedModifier = 0;
                        modifiercurrentTime = 0;
                    }

                    if (speedModifier != 0 && modifiercurrentTime <= modifierDuration)
                    {
                        temporaryspeed *= speedModifier;
                        modifiercurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }

                    velocity = Vector2.Multiply(direction, temporaryspeed);
                    position += velocity;
                }
            }
            else
                alive = false;

            if (currentHealth <= 0)
                alive = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                base.Draw(spriteBatch, Color.White);
            }
        }
    }
}
