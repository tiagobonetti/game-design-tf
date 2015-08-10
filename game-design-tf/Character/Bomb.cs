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
            float scale = 2.0f * Radius / sprite.Width;
            game.spriteBatch.Draw(sprite, position, null, null, origin, 0f, Vector2.One * scale, Color.Yellow, SpriteEffects.None, 0f);
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
            foreach (BaseCharacter character in GetObjectsHit()) {
                character.Die();
            }
        }

        List<BaseCharacter> GetObjectsHit() {
            List<BaseCharacter> charactersHit = new List<BaseCharacter>();
            foreach (BaseCharacter character in game.sceneControl.gameplay.characterList) {
                float distance = Vector2.Distance(position, character.position);
                if (distance <= Radius + character.sprite.Width / Math.Sqrt(2)) {
                    charactersHit.Add(character);
                }
            }
            return charactersHit;
        }

    }
}
