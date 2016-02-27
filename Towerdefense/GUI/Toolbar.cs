using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense.GUI
{
    public class Toolbar
    {
        private Texture2D texture;
        private SpriteFont font;

        private Vector2 position;
        private Vector2 moneyPosition;
        private Vector2 livesPosition;

        public Toolbar(Texture2D texture, SpriteFont font, Vector2 position)
        {
            this.texture = texture;
            this.font = font;

            this.position = position;
            moneyPosition = new Vector2(10, position.Y + 10);
            livesPosition = new Vector2(150, position.Y + 10);
        }

        public void Draw(SpriteBatch spritebatch, Player player)
        {
            spritebatch.Draw(texture, position, Color.White);

            string money = string.Format("Gold : {0}     Lives : {1}", player.Money, player.Lives);
            spritebatch.DrawString(font, money, moneyPosition, Color.White);

            string towercost = string.Format("Arrow : {0}          Spike : {1}          Slow : {2}", 15, 40, 15);
            spritebatch.DrawString(font, towercost, livesPosition, Color.White);
        }
    }
}
