using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Flag : GameObject{

        public BaseCharacter Carrier { get; set; }
        public const float resetPositionCountdown = 5f;
        Vector2 defaultPosition;
        Timer timer = new Timer();

        public Flag(MainGame game) : base(game.Content.Load<Texture2D>("Sprite/Flag"), MainGame.Tag.Runner, Vector2.One, "Flag", game) {
            defaultPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.1f, game.graphics.PreferredBackBufferHeight * 0.9f);
        }

        public void Update(GameTime gameTime) {
            ResetCountdown(gameTime);
        }

        public void PickUp(BaseCharacter character) {
            Carrier = character;
        }

        public void Drop() {
            if (Carrier != null){
                position = Carrier.position;
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
