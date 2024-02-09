using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace SpooninDrawer.Engine.Map
{
    public class TilemapManager
    {
        TmxMap _map;
        Texture2D tileset;
        int tilesetTilesWide;
        int tileWidth;
        int tileHeight;

        public TilemapManager( TmxMap map, Texture2D tileset, int tilesetTilesWide, int tileWidth, int tileHeight)
        {
            _map = map;
            this.tileset = tileset;
            this.tilesetTilesWide = tilesetTilesWide;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }

        public void Draw(SpriteBatch _spriteBatch) { 
            //_spriteBatch.Begin();
            for(int i = 0; i < _map.Layers.Count; i++) 
            { 
                for(int j =0;  j < _map.Layers[i].Tiles.Count; j++)
                {
                     int gid = _map.Layers[i].Tiles[j].Gid;
                    if (gid == 0)
                    {
                        //do nithing
                    }
                    else
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                        float x = (j % _map.Width) * _map.TileWidth;
                        float y = (float)Math.Floor(j / (double)_map.Width) * _map.TileHeight;
                        Rectangle tilesetRec = new Rectangle((tileWidth) * column, (tileHeight * row), tileWidth, tileHeight);
                        _spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    }
                }
            }
            //_spriteBatch.End();
        }
    }
}
