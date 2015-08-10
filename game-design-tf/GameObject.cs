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
        public Color baseColor;

        public MainGame.Tag tag;
        public string name;
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
            baseColor = Color.White;
        }

        public virtual void Update(GameTime gametime) {
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            Vector2 origin = new Vector2(baseRectangle.Width * 0.5f, baseRectangle.Height * 0.5f);
            spriteBatch.Draw(sprite, position, null, baseRectangle, origin, 0f, Vector2.One, baseColor, SpriteEffects.None, 0f);
        }

        public Rectangle CollisionRectangle {
            get {
                return new Rectangle((int)position.X, (int)position.Y, (int)baseRectangle.Width, (int)baseRectangle.Height);
            }
        }

    }
}
