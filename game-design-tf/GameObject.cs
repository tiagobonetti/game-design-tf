using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class GameObject {

        public Texture2D spriteSheet;
        public Rectangle uvRect;
        public MainGame.Tag tag;
        public string name;
        public CollisionBox collision;
        public Vector2 position;
        public MainGame game;

        public GameObject(Texture2D spriteSheet, MainGame.Tag tag, Vector2 position, string name, MainGame game) {
            this.game = game;
            this.name = name;
            this.spriteSheet = spriteSheet;
            this.tag = tag;
            this.position = position;
            game.sceneControl.GetScene().gameObjectList.Add(this);
        }

        public void Draw(SpriteBatch spriteBatch) {
            //      System.Diagnostics.Debug.WriteLine("uv: " + uvRect.Location.X / 83 + "," + uvRect.Location.Y / 53);
            Vector2 origin = new Vector2(uvRect.Width * 0.5f, uvRect.Height * 0.5f);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            spriteBatch.Draw(spriteSheet, position, null, uvRect, Vector2.One, 0f, Vector2.One, Color.White, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
