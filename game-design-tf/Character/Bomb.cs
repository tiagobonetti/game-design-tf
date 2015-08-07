using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_design_tf {
    public class Bomb {
        public Bomber Owner { get; private set; }
        public bool Dead { get; private set; }
        public float Radius { get; private set; }
        public float Duration { get; private set; }
        Vector2 position;
        Timer timer = new Timer();
        MainGame game;
        Texture2D sprite;

        public Bomb(float radius, float duration, Bomber owner) {
            this.Owner = owner;
            this.game = owner.game;
            sprite = game.Content.Load<Texture2D>("Sprite/Explosion");
            this.Radius = radius;
            this.Duration = duration;
            this.position = owner.position;
            Dead = false;
            game.sceneControl.GetScene().bombList.Add(this);
        }

        public void Update(GameTime gameTime) {
            DieAfterDuration(gameTime);
            Kill();
        }

        public void Draw() {
            Vector2 origin = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            game.spriteBatch.Draw(sprite, position, null, null, origin, 0f, Vector2.One * 2f, Color.Yellow, SpriteEffects.None, 0f);
            game.spriteBatch.End();
            DrawDiameter(position, new Vector2(position.X + Radius, position.Y));
        }

        void DieAfterDuration(GameTime gameTime) {
            bool timerEnded;
            timer.TimerCounter(gameTime, Duration, out timerEnded);
            if (timerEnded) {
                Dead = true;
                Owner.bombActive = false;
            }
        }

        void Kill() {
            foreach (GameObject obj in GetObjectsHit()) {
                obj.Explode();
            }
        }

        IList<GameObject> GetObjectsHit() {
            IList<GameObject> objList = game.sceneControl.GetScene().gameObjectList;
            IList<GameObject> objHit = new List<GameObject>();
            foreach (GameObject obj in objList) {
                float distance = Vector2.Distance(position, obj.position - new Vector2(obj.spriteSheet.Width * 0.5f, obj.spriteSheet.Height * 0.5f));
                if (distance <= Radius) {
                    objHit.Add(obj);
                }
            }
            return objHit;
        }

        void DrawDiameter(Vector2 p1, Vector2 p2) {

            Texture2D line = game.Content.Load<Texture2D>("line");
            float angle = (float)Math.Atan2(p1.Y - p2.Y, p1.X - p2.X);
            float dist = Vector2.Distance(p1, p2);

            game.spriteBatch.Begin();
            game.spriteBatch.Draw(line, new Rectangle((int)p2.X, (int)p2.Y, (int)dist, 2), null, Color.Blue, angle, Vector2.Zero, SpriteEffects.None, 0f);
            game.spriteBatch.End();
        }
    }
}
