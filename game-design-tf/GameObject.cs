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
        public Vector2 velocity = Vector2.Zero;

        public GameObject(Texture2D spriteSheet, MainGame.Tag tag, Vector2 position, string name, MainGame game) {
            this.game = game;
            this.name = name;
            this.spriteSheet = spriteSheet;
            this.tag = tag;
            this.position = position;
            uvRect = new Rectangle(0, 0, 1, 1);
            collision = new CollisionBox(this, uvRect);
            game.sceneControl.GetScene().gameObjectList.Add(this);
        }
        /*
        public void Update(GameTime gametime) {
            ApplyPhysics(gametime);
        }
        */
        public void Draw(SpriteBatch spriteBatch) {
            Vector2 origin = new Vector2(uvRect.Width * 0.5f, uvRect.Height * 0.5f);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            spriteBatch.Draw(spriteSheet, position, null, uvRect, origin, 0f, Vector2.One, Color.White, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        protected void ApplyPhysics(GameTime gametime) {
            collision.Update(gametime);
        }

        public void Explode() {
            System.Diagnostics.Debug.WriteLine("Explode " + name);
        }
    }
}
