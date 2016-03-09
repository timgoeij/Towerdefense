using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Towerdefense
{
    public class Tile
    {
        //variables
        private Texture2D tileTex;
        private Rectangle tileRec;
        private Color Color;

        public Rectangle TileRec
        {
            get { return tileRec; }
        }

        private bool hasTower = false;
        private bool isWay = false;

        public bool HasTower
        {
            get { return hasTower; }
            set { hasTower = value; }
        }

        public bool IsWay
        {
            get { return isWay; }
        }

        private bool mouseInTile = false;

        public Tile(Texture2D tex, bool isWay, Vector2 pos)
        {
            //get the texture
            this.tileTex = tex;

            //get true or false to know if this tile is a way
            this.isWay = isWay;

            //get the position and the with and the height of the tile
            tileRec.X = (int)pos.X;
            tileRec.Y = (int)pos.Y;
            tileRec.Width = tex.Width;
            tileRec.Height = tex.Height;
        }

        /// <summary>
        /// this bool check if the position of the mouse is inside the rectangle of the tile
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns></returns>
        public bool checkMouseInTile(Vector2 mousePos)
        {
            if(tileRec.Contains(mousePos))
                mouseInTile = true;
            else
                mouseInTile = false;

            return mouseInTile;
        }

        public void Update() {
            Color = Color.White;

            if (mouseInTile)
            {
                //check if the tile has a tower or the tile is a way
                if (isWay || hasTower)
                    Color = Color.Red;
                else
                    Color = Color.Green;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(tileTex, tileRec, Color);
            /*
            //check if the mouse is in the rectangle of the tile
            if (mouseInTile)
            {
                //check if the tile has a tower or the tile is a way
                if (isWay || hasTower)
                    spritebatch.Draw(tileTex, tileRec, Color.Red);
                else
                    spritebatch.Draw(tileTex, tileRec, Color.Green);
            }
            else
                spritebatch.Draw(tileTex, tileRec, Color.White);
                */
        }
    }
}
