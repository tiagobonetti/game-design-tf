using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Runner : BaseCharacter {
        public IPlayerInput PlayerInput { get; set; }
        bool previousButton1State;
        public Flag flag;

        public Runner(Texture2D spriteSheet, MainGame.Tag tag, Vector2 position, string name, MainGame game, IPlayerInput input)
            : base(spriteSheet, tag, position, name, game, input) {
            this.PlayerInput = null;
        }

        new public void Update(GameTime gametime) {
            CharacterState state = CharacterState.Idle;
            if (canControl) {
                Action(gametime);
                if (canMove) {
                    Movement(gametime);
                }
            }
            PickUpFlag();
            DropFlag();
        }

        void PickUpFlag(){
            if (game.sceneControl.GetScene().flagList.Count > 0) {
                foreach (Flag flag in game.sceneControl.GetScene().flagList) {
                    if (CollisionRectangle.Intersects(flag.CollisionRectangle)) {
                        flag.PickUp(this);
                        System.Diagnostics.Debug.WriteLine("pickup");
                    }
                }
            }
        }

        void DropFlag() {
            bool button1 = input.GetButton1();
            if (button1 && !previousButton1State) {
                if (flag != null) {
                    flag.Drop();
                }
            }
            previousButton1State = button1;
        }
    }
}
