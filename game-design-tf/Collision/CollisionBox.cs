using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class CollisionBox {

        public GameObject owner;
        public Rectangle rect;
        public float lifespan;
        public bool Solid { get; private set; }

        public CollisionBox(GameObject owner, Vector2 position, Vector2 size) {
            this.owner = owner;
            rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public CollisionBox(GameObject owner, int x, int y, int width, int height) {
            this.owner = owner;
            rect = new Rectangle(x, y, width, height);
        }

        public CollisionBox(GameObject owner, Rectangle rect) {
            this.owner = owner;
            this.rect = rect;
            Solid = true;
        }

        public void Update(GameTime gametime) {
            rect.X = (int)(owner.position.X - owner.uvRect.Width * 0.5f);
            rect.Y = (int)(owner.position.Y - owner.uvRect.Height * 0.5f);
        }


        public bool OnCollision(out GameObject objHit) {
            foreach (GameObject obj in owner.game.sceneControl.GetScene().gameObjectList) {
                if (rect.Intersects(obj.collision.rect)) {
                    objHit = obj;
                    return true;
                }
            }
            objHit = null;
            return false;
        }

        public bool OnCollision(GameObject obj) {
            if (rect.Intersects(obj.collision.rect))
                return true;
            else
                return false;
        }
    }
}
