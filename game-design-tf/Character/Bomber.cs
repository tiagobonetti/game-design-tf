using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Bomber : BaseCharacter {
        public bool bombActive = false;
        bool previousButton1State;
        const float bombSize = 200f;

        public Bomber(Texture2D spriteSheet, MainGame.Tag tag, Vector2 position, string name, MainGame game, IPlayerInput input)
            : base(spriteSheet, tag, position, name, game, input) {
            collision = new CollisionBox(this, baseRectangle);
            DEBUG_Collision.CollisionList.Add(collision);
        }

        new public void Update(GameTime gametime) {
            CharacterState state = CharacterState.Idle;
            StateMachine(gametime, state);
            if (canControl) {
                Action(gametime);
                ActivateBomb();
                if (canMove) {
                    Movement(gametime);
                }
            }
            ApplyPhysics(gametime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
        }

        void ActivateBomb() {
            bool button1 = input.GetButton1();
            if ( button1  && !previousButton1State ) {
                if (!bombActive) {
                    Bomb bomb = new Bomb(bombSize, 3f, this);
                    bombActive = true;
                }
            }
            previousButton1State = button1;
        }
    }
}
