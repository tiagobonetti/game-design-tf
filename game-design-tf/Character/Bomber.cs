using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Bomber : BaseCharacter {

        const float triggerTime = 1f;
        bool explosionTriggered;
        bool previousButton1State;
        Timer triggerTimer;

        const float explosionMaxSize = 100f;
        const float explosionTime = 1f;
        public bool exploding;
        float explodingSize;
        Timer explosionTimer;

        static Color bomberColor = Color.Yellow;
        static Color explodingColor = Color.Red;

        static Texture2D explosionSprite;
        static Texture2D bomberSprite;
        static public void Load(ContentManager content) {
            bomberSprite = content.Load<Texture2D>("Sprite/Characters/Runner");
            explosionSprite = content.Load<Texture2D>("Sprite/Explosion");
        }

        public Bomber(Vector2 position, string name, MainGame game, IPlayerInput input)
            : base(bomberSprite, MainGame.Tag.Bomber, position, name, game, input) {
            baseColor = bomberColor;
        }

        public override void Reset() {
            base.Reset();
            baseColor = bomberColor;
            explosionTriggered = false;
            triggerTimer = new Timer();
            exploding = false;
            explosionTimer = new Timer();
            explodingSize = 1.0f;
        }

        new public void Update(GameTime gameTime) {
            if (dead) {
                if (exploding) {
                    UpdateExplosion(gameTime);
                }
                else {
                    UpdateRespawn(gameTime);
                }
            }
            else {

                if (canControl) {
                    UpdateExplosionTrigger(gameTime);
                    if (canMove) {
                        UpdateMovement(gameTime);
                    }
                }
                ReturnFlag();
                if (characterHit != null && characterHit is Bomber) {
                    Explode();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            if (explosionTriggered) {
                baseColor = Color.Lerp(baseColor, explodingColor, 0.05f);
            }
            if (exploding) {
                Vector2 origin = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
                explodingSize = MathHelper.Lerp(explodingSize, explosionMaxSize, 0.33f);
                float scale = 2.0f * explodingSize / sprite.Width;
                spriteBatch.Draw(sprite, position, null, null, origin, 0f, Vector2.One * scale, explodingColor, SpriteEffects.None, 0f);
            }
        }

        void UpdateExplosionTrigger(GameTime gameTime) {
            if (explosionTriggered) {
                bool ended = false;
                triggerTimer.TimerCounter(gameTime, explosionTime, out ended);
                if (ended) {
                    Explode();
                }
            }
            else {
                bool button1 = input.GetButton1();
                if (button1 && !previousButton1State) {
                    explosionTriggered = true;
                }
                previousButton1State = button1;
            }
        }

        void UpdateExplosion(GameTime gameTime) {
            List<BaseCharacter> characterList = game.sceneControl.gameplay.characterList;
            characterList = characterList.Where(character => character != this).ToList();
            characterList = characterList.Where(character => !character.dead).ToList();

            foreach (BaseCharacter character in characterList) {
                float distance = Vector2.Distance(position, character.position);
                if (distance <= explodingSize + character.sprite.Width / Math.Sqrt(2)) {
                    character.Die();
                }
            }

            bool ended = false;
            explosionTimer.TimerCounter(gameTime, explosionTime, out ended);
            if (ended) {
                exploding = false;
            }
        }

        void ReturnFlag() {
            if (game.sceneControl.GetScene().flagList.Count > 0) {
                foreach (Flag flag in game.sceneControl.GetScene().flagList) {
                    if (CollisionRectangle.Intersects(flag.CollisionRectangle)) {
                        flag.ResetPosition();
                        System.Diagnostics.Debug.WriteLine("ResetPosition");
                    }
                }
            }
        }

        void Explode() {
            exploding = true;
            dead = true;
        }
    }
}
