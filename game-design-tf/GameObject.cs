using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class GameObject {

        public Texture2D sprite;
        public Rectangle baseRectangle;
        public MainGame.Tag tag;
        public string name;
        public CollisionBox collision;
        public Vector2 position;
        public MainGame game;
        public Vector2 velocity = Vector2.Zero;

        public GameObject(Texture2D sprite, MainGame.Tag tag, Vector2 position, string name, MainGame game) {
            this.game = game;
            this.name = name;
            this.sprite = sprite;
            this.tag = tag;
            this.position = position;
            baseRectangle = new Rectangle(0, 0, 1, 1);
            collision = new CollisionBox(this, baseRectangle);
            game.sceneControl.GetScene().gameObjectList.Add(this);
        }

        public virtual void Update(GameTime gametime) {
            ApplyPhysics(gametime);
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            Vector2 origin = new Vector2(baseRectangle.Width * 0.5f, baseRectangle.Height * 0.5f);
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            spriteBatch.Draw(sprite, position, null, baseRectangle, origin, 0f, Vector2.One, Color.White, SpriteEffects.None, 0f);
            //spriteBatch.End();
        }

        protected void ApplyPhysics(GameTime gametime) {
            collision.Update(gametime);
        }

        public void Explode() {
            System.Diagnostics.Debug.WriteLine("Explode " + name);
        }
    }
}
