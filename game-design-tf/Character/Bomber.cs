using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Bomber : BaseCharacter {
        public IPlayerInput PlayerInput { get; set; }
        public bool bombActive = false;
        KeyboardState previousButtonState;

        public Bomber(Texture2D spriteSheet, MainGame.Tag tag, Vector2 position, string name, MainGame game, IPlayerInput input)
            : base(spriteSheet, tag, position, name, game, input) {
            this.PlayerInput = null;
            collision = new CollisionBox(this, uvRect);
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

        void ActivateBomb() {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && previousButtonState != Keyboard.GetState()) {
                if (!bombActive) {
                    Bomb bomb = new Bomb(100f, 3f, this);
                    bombActive = true;
                }
            }
            previousButtonState = Keyboard.GetState();
        }
    }
}
