using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    public class Level
    {
        public List<Texture2D> maptextures = new List<Texture2D>();
        private Queue<Vector2> waypointsMap1 = new Queue<Vector2>();
        private Queue<Vector2> waypointsMap2 = new Queue<Vector2>();
        private Queue<Vector2> waypointsMap31 = new Queue<Vector2>();
        private Queue<Vector2> waypointsMap32 = new Queue<Vector2>();

        private List<Tile> tiles = new List<Tile>();

        public List<Tile> Tiles
        {
            get { return tiles; }
        }

        #region maps
        int[,] map = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, 
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}, 
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}, 
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, 
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, 
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, 
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, 
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}, 
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}, 
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, 
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, 
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, 
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        };

        int[,] map2 = new int[,]
        {
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0}, {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0}, {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0}, {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, {1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1}, {1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1}, {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1},
            {0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,1}, {0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,1}, {0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,1}, {1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1},
            {1,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0}, {1,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0}, {1,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0}, {1,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0},
        };

        int[,] map3 = new int[,]
        {
            {1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1},
            {0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0},
            {1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        };
        #endregion

        public Queue<Vector2> WaypointsMap1
        {
            get { return waypointsMap1; }
        }

        public Queue<Vector2> WaypointsMap2
        {
            get { return waypointsMap2; }
        }

        public Queue<Vector2> WaypointsMap31
        {
            get { return waypointsMap31; }
        }

        public Queue<Vector2> WaypointsMap32
        {
            get { return waypointsMap32; }
        }

        public int width
        {
            get { return map.GetLength(1); }
        }

        public int height
        {
            get { return map.GetLength(0); }
        }

        public int GetIndex(int cellX, int cellY, int levelcount)
        {
            if (cellX < 0 || cellX > width - 1 || cellY < 0 || cellY > height - 1)
                return 0;

            if (levelcount == 0)
                return map[cellX, cellY];
            else if (levelcount == 1)
                return map2[cellX, cellY];
            else if (levelcount == 2)
                return map3[cellX, cellY];

            return 0;
        }

        public Level()
        {
            #region waypoints for maps
            waypointsMap1.Enqueue(new Vector2(0, 0) * 30); waypointsMap1.Enqueue(new Vector2(15, 0) * 30);
            waypointsMap1.Enqueue(new Vector2(15, 4) * 30); waypointsMap1.Enqueue(new Vector2(0, 4) * 30);
            waypointsMap1.Enqueue(new Vector2(0, 8) * 30); waypointsMap1.Enqueue(new Vector2(15, 8) * 30);
            waypointsMap1.Enqueue(new Vector2(15, 12) * 30); waypointsMap1.Enqueue(new Vector2(0, 12) * 30);
            waypointsMap1.Enqueue(new Vector2(0, 15) * 30); waypointsMap1.Enqueue(new Vector2(15, 15) * 30);

            waypointsMap2.Enqueue(new Vector2(0, 0) * 30); waypointsMap2.Enqueue(new Vector2(7, 0) * 30);
            waypointsMap2.Enqueue(new Vector2(7, 7) * 30); waypointsMap2.Enqueue(new Vector2(0, 7) * 30);
            waypointsMap2.Enqueue(new Vector2(0, 4) * 30); waypointsMap2.Enqueue(new Vector2(15, 4) * 30);
            waypointsMap2.Enqueue(new Vector2(15, 11) * 30); waypointsMap2.Enqueue(new Vector2(8, 11) * 30);
            waypointsMap2.Enqueue(new Vector2(8, 8) * 30); waypointsMap2.Enqueue(new Vector2(11, 8) * 30);
            waypointsMap2.Enqueue(new Vector2(11, 15) * 30); waypointsMap2.Enqueue(new Vector2(4, 15) * 30);
            waypointsMap2.Enqueue(new Vector2(4, 11) * 30); waypointsMap2.Enqueue(new Vector2(0, 11) * 30);
            waypointsMap2.Enqueue(new Vector2(0, 15) * 30);

            waypointsMap31.Enqueue(new Vector2(0, 0) * 30); waypointsMap31.Enqueue(new Vector2(3, 0) * 30);
            waypointsMap31.Enqueue(new Vector2(3, 11) * 30); waypointsMap31.Enqueue(new Vector2(15, 11) * 30);
            waypointsMap31.Enqueue(new Vector2(15, 3) * 30); waypointsMap31.Enqueue(new Vector2(9, 3) * 30);
            waypointsMap31.Enqueue(new Vector2(9, 15) * 30); waypointsMap31.Enqueue(new Vector2(0, 15) * 30);

            waypointsMap32.Enqueue(new Vector2(15, 0) * 30); waypointsMap32.Enqueue(new Vector2(12, 0) * 30);
            waypointsMap32.Enqueue(new Vector2(12, 11) * 30); waypointsMap32.Enqueue(new Vector2(0, 11) * 30);
            waypointsMap32.Enqueue(new Vector2(0, 3) * 30); waypointsMap32.Enqueue(new Vector2(6, 3) * 30);
            waypointsMap32.Enqueue(new Vector2(6, 15) * 30); waypointsMap32.Enqueue(new Vector2(15, 15) * 30);
            #endregion
        }

        public void AddTexture(Texture2D field, Texture2D way)
        {
            maptextures.Add(field);
            maptextures.Add(way);
        }

        public void initLevel(int levelCount)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int textureindex = 0;

                    switch(levelCount)
                    {
                        case 0: textureindex = map[y, x];
                            break; 
                        case 1: textureindex = map2[y, x];
                            break;
                        case 2: textureindex = map3[y, x];
                            break;
                    }

                    if (textureindex == -1)
                        continue;

                    Texture2D texture = maptextures[textureindex];

                    Tile tile = new Tile(texture, (textureindex == 1), new Vector2(x * 30, y * 30));
                    tiles.Add(tile);
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Tile tile in tiles)
                tile.Draw(spritebatch);
        }
    }
}
