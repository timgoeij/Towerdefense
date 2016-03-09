using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Towerdefense.Towers;

namespace Towerdefense
{
    public class Player
    {
        private int money = 50;
        private int lives = 30;

        private Texture2D[] towerTexture;
        private Texture2D bulletTexture;

        private List<Tower> towers = new List<Tower>();

        private MouseState mousestate;
        private MouseState oldstate;

        private int cellX;
        private int cellY;

        private Level level;

        private int Levelcount;

        private string newtowertype;

        public string NewTowerType
        {
            set { newtowertype = value; }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public Player(Level level, int levelcount, Texture2D bullettexture, Texture2D[] towertexture)
        {
            this.level = level;
            this.Levelcount = levelcount;
            this.bulletTexture = bullettexture;
            this.towerTexture = towertexture;
        }

        private bool IsCellclear(Tile tile)
        {
            bool inBounds = cellX >= 0 && cellY >= 0 &&
                cellX < level.width && cellY < level.height;

            return inBounds && !tile.HasTower && !tile.IsWay;
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            mousestate = Mouse.GetState();

            foreach(Tile tile in level.Tiles)
            {
                tile.Update();

                if (tile.checkMouseInTile(new Vector2(mousestate.X, mousestate.Y)))
                {

                    if (mousestate.LeftButton == ButtonState.Released
                        && oldstate.LeftButton == ButtonState.Pressed)
                    {
                        if (!string.IsNullOrEmpty(newtowertype))
                        {
                            AddTower(tile);
                        }
                    }
                }
            }

            cellX = (int)(mousestate.X / 30);
            cellY = (int)(mousestate.Y / 30);

            foreach (Tower tower in towers)
            {
                if (!tower.hasTarget)
                {
                    tower.GetClosestEnemy(enemies);
                }

                tower.Update(gameTime);
            }

            oldstate = mousestate;
        }

        public void AddTower(Tile tile)
        {
            Tower towertoadd = null;

            switch (newtowertype)
            {
                case "Arrow Tower":
                    towertoadd = new ArrowTower(towerTexture[0], bulletTexture, new Vector2(tile.TileRec.X, tile.TileRec.Y));
                    break;
                case "Spike Tower":
                    towertoadd = new SpikeTower(towerTexture[1], bulletTexture, new Vector2(tile.TileRec.X, tile.TileRec.Y));
                    break;
                case "Slow Tower":
                    towertoadd = new SlowTower(towerTexture[2], bulletTexture, new Vector2(tile.TileRec.X, tile.TileRec.Y));
                    break;
            }

            if (IsCellclear(tile) && towertoadd.Cost <= money)
            {
                towers.Add(towertoadd);
                money -= towertoadd.Cost;
                newtowertype = string.Empty;
                tile.HasTower = true;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Tower tower in towers)
            {
                tower.Draw(spritebatch);
            }
        }
    }
}
