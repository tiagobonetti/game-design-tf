using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Flag : GameObject{

        public Runner Carrier { get; set; }
        public const float resetPositionCountdown = 7.5f;
        Vector2 defaultPosition;
        Timer timer = new Timer();

        public Flag(MainGame game) : base(game.Content.Load<Texture2D>("Sprite/Flag"), MainGame.Tag.Flag, Vector2.One, "Flag", game) {
            defaultPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.9f, game.graphics.PreferredBackBufferHeight * 0.5f);
            position = defaultPosition;
            baseRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
            game.sceneControl.GetScene().flagList.Add(this);
        }

        public void Update(GameTime gameTime) {
            ResetCountdown(gameTime);
            DropIfDeadCarrier();
        }

        override public void Draw(SpriteBatch spriteBatch) {
            if (Carrier == null) {
                Vector2 origin = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
                spriteBatch.Draw(sprite, position, null, null, origin, 0f, Vector2.One, Color.White, SpriteEffects.None, 0f);
            }
        }

        void DropIfDeadCarrier() {
            if (Carrier != null && Carrier.dead) {
                Drop();
            }
        }

        public void PickUp(Runner runner) {
            Carrier = runner;
            runner.flag = this;
        }

        public void Drop() {
            if (Carrier != null){
                position = Carrier.position;
                Carrier.baseColor = Color.White;
                Carrier.flag = null;
                Carrier = null;
            }
        }

        public void ResetCountdown(GameTime gameTime){
            bool timerEnded;
            timer.TimerCounter(gameTime, resetPositionCountdown, out timerEnded);
            if (timerEnded){
                ResetPosition();
            }
        }

        public void ResetPosition() {
            position = defaultPosition;
        }
    }
}
