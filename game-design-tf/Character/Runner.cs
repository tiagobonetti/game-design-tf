using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Runner : BaseCharacter {
        public IPlayerInput PlayerInput { get; set; }

        bool previousButton1State;
        public Flag flag;

        static Texture2D runnerSprite;
        static public void Load(ContentManager content) {
            runnerSprite = content.Load<Texture2D>("Sprite/Characters/Runner");
        }

        public Runner(Vector2 position, string name, MainGame game, IPlayerInput input)
            : base(runnerSprite, MainGame.Tag.Runner, position, name, game, input) {
            this.PlayerInput = null;
            speed_Walk = 550;
        }

        public void Update(GameTime gametime) {
            if (dead) {
                UpdateRespawn(gametime);
            }
            else {
                if (canControl && canMove) {
                    UpdateMovement(gametime);
                }
                PickUpFlag();
                DropFlag();
                if (characterHit != null) {
                    characterHit.Die(this);
                }
            }
        }

        void PickUpFlag() {
            if (game.sceneControl.GetScene().flagList.Count > 0) {
                foreach (Flag flag in game.sceneControl.GetScene().flagList) {
                    if (CollisionRectangle.Intersects(flag.CollisionRectangle)) {
                        flag.PickUp(this);
                        baseColor = Color.ForestGreen;
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
