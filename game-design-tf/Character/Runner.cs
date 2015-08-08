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
        Flag flag;

        public Runner(Texture2D spriteSheet, MainGame.Tag tag, Vector2 position, string name, MainGame game, Flag flag, IPlayerInput input)
            : base(spriteSheet, tag, position, name, game, input) {
            this.PlayerInput = null;
            this.flag = flag;
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
            PickUpFlag();
            ApplyPhysics(gametime);
        }

        void PickUpFlag(){

        }
    }
}
