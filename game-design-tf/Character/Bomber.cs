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

        public bool bombActive = false;
        bool previousButton1State;
        const float bombSize = 100f;
        const float bombDuration = 1f;

        static Texture2D bomberSprite;
        static public void Load(ContentManager content) {
            bomberSprite = content.Load<Texture2D>("Sprite/Characters/Runner");
        }

        public Bomber(Vector2 position, string name, MainGame game, IPlayerInput input)
            : base(bomberSprite, MainGame.Tag.Bomber, position, name, game, input) {
        }

        new public void Update(GameTime gametime) {
            CharacterState state = CharacterState.Idle;
            if (canControl) {
                UpdateExplosion();
                if (canMove) {
                    UpdateMovement(gametime);
                }
            }
            ReturnFlag();
            if (characterHit != null && characterHit is Bomber) {
                Bomb bomb = new Bomb(bombSize, bombDuration, this);
                bombActive = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
        }

        void UpdateExplosion() {
            bool button1 = input.GetButton1();
            if (button1 && !previousButton1State) {
                if (!bombActive) {
                    Bomb bomb = new Bomb(bombSize, bombDuration, this);
                    bombActive = true;
                }
            }
            previousButton1State = button1;
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
    }
}
