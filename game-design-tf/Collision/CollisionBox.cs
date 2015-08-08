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
            rect.X = (int)(owner.position.X - owner.baseRectangle.Width * 0.5f);
            rect.Y = (int)(owner.position.Y - owner.baseRectangle.Height * 0.5f);
        }
        /*
        public bool Blocked(out Vector2 hitDirection, out GameObject objHit) {
            GameObject obj = owner.game.sceneControl.GetScene().obj2;
            if (Solid && obj.collision.Solid) {
                if (OnCollision(obj)) {
                    Vector2 rect1Center = new Vector2(rect.X + rect.Width * 0.5f, rect.Y + rect.Height * 0.5f);
                    Vector2 rect2Center = new Vector2(obj.collision.rect.X + obj.collision.rect.Width * 0.5f, obj.collision.rect.Y + obj.collision.rect.Height * 0.5f);
                    hitDirection = Vector2.Normalize(rect2Center - rect1Center);
                    if (hitDirection.X > 0)
                        System.Diagnostics.Debug.WriteLine("Collision X RIGHT");
                    else if (hitDirection.X < 0)
                        System.Diagnostics.Debug.WriteLine("Collision X LEFT");
                    else
                        System.Diagnostics.Debug.WriteLine("Collision X SAME");

                    if (hitDirection.Y > 0)
                        System.Diagnostics.Debug.WriteLine("Collision Y BELOW");
                    else if (hitDirection.Y < 0)
                        System.Diagnostics.Debug.WriteLine("Collision Y ABOVE");
                    else
                        System.Diagnostics.Debug.WriteLine("Collision Y SAME");

                    objHit = obj;
                    return true;
                }
            }
            objHit = null;
            hitDirection = Vector2.Zero;
            return false;
        }*/

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
