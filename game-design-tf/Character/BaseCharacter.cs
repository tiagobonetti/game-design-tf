using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {

    public enum CharacterState {
        Idle,
        Moving,
        Dead,
    }

    public class BaseCharacter : GameObject {
        public const float speed_Walk = 500;
        public const float acceleration_Walk = 0.1f;
        public const float deceleration_Walk = 0.05f;
        public const float speed_Run = 1000;
        public const float acceleration_Run = 0.1f;
        public const float deceleration_Run = 0.05f;

        public bool canControl = true;
        public bool canMove = true;

        //collision
        GameObject objHit = null;
        public bool collided = false;

        public IPlayerInput input;
        Animator animator = new Animator();

        public BaseCharacter(Texture2D sprite, MainGame.Tag tag, Vector2 position, string name, MainGame game, IPlayerInput input)
            : base(sprite, tag, position, name, game) {

            baseRectangle = new Rectangle(0, 0, 50, 50);
            velocity = Vector2.Zero;
            this.input = input;

        }

        override public void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            if (input != null) {
                input.DrawDebug(spriteBatch);
            }
            if (this is Bomber) {
                Debug.DrawText(game.spriteBatch, new Vector2(0.0f, 600.0f), "Vel: " + velocity.ToString() );
            }
        }

        protected void Movement(GameTime gameTime) {

            Vector2 direction = input.GetDirection();
            if (direction == Vector2.Zero) {
                velocity.X = MathHelper.Lerp(velocity.X, 0, deceleration_Walk);
                if (MathHelper.Distance(velocity.X, 0) < 0.01f)
                    velocity.X = 0;
                velocity.Y = MathHelper.Lerp(velocity.Y, 0, deceleration_Walk);
                if (MathHelper.Distance(velocity.Y, 0) < 0.01f)
                    velocity.Y = 0;
            }
            else {
                velocity += direction * acceleration_Walk;
            }
            velocity = new Vector2(MathHelper.Clamp(velocity.X, -1f, 1f), MathHelper.Clamp(velocity.Y, -1f, 1f));

            Vector2 newPosition = position + (velocity * speed_Walk) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Clamp position to scene borders
            newPosition = new Vector2(MathHelper.Clamp(newPosition.X, 0 + baseRectangle.Width * 0.5f,
                                                       game.graphics.PreferredBackBufferWidth - baseRectangle.Width * 0.5f),
                                      MathHelper.Clamp(newPosition.Y, 0 + baseRectangle.Height * 0.5f,
                                                       game.graphics.PreferredBackBufferHeight - baseRectangle.Height * 0.5f));

            IList<GameObject> objList = game.sceneControl.GetScene().gameObjectList.Where(gameObject => gameObject != this).ToList();

            Rectangle collisionRectangle = new Rectangle((int)newPosition.X, (int)newPosition.Y, (int) baseRectangle.Width, (int)baseRectangle.Height);

            objHit = null;
            foreach (GameObject obj in objList) {
                if (obj.tag == MainGame.Tag.Bomber || 
                    obj.tag == MainGame.Tag.Runner || 
                    obj.tag == MainGame.Tag.Wall) {
                    if (collisionRectangle.Intersects(obj.CollisionRectangle)) {
                        //bool what = this.CollisionRectangle.Intersects(obj.CollisionRectangle);
                        objHit = obj;
                        break;
                    }
                }
            }
            
            if (objHit == null) {
                position = newPosition;
            }
            else {
                collided = true;
                velocity = Vector2.Zero;
            }
        }

        protected void Action(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                //Run modifier.
            }
        }
    }
}