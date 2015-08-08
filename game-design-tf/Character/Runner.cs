using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    class Runner : BaseCharacter {
        public IPlayerInput PlayerInput { get; set; }

        public Runner(Texture2D spriteSheet, MainGame.Tag tag, Vector2 position, string name, MainGame game, IPlayerInput input)
            : base(spriteSheet, tag, position, name, game, input) {
            this.PlayerInput = null;
            collision = new CollisionBox(this, baseRectangle);
            DEBUG_Collision.CollisionList.Add(collision);
        }

        new public void Update(GameTime gametime) {
            CharacterState state = CharacterState.Idle;
            StateMachine(gametime, state);
            if (canControl) {
                Action(gametime);
                if (canMove) {
                    Movement(gametime);
                }
            }
            ApplyPhysics(gametime);
        }
    }
}
