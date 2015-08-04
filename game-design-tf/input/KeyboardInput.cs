using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace game_design_tf {

    public class KeyboardInput : IPlayerInput {
        public Vector2 DebugPosition { get; set; }

        public KeyboardInput() {
            this.DebugPosition = Vector2.Zero;
        }

        static List<Keys> wasd = new List<Keys>() {
            Keys.W,
            Keys.A,
            Keys.S,
            Keys.D
        };

        public bool GetButton1() {
            MouseState state = Mouse.GetState();
            return (state.LeftButton == ButtonState.Pressed);
        }

        public bool GetButton2() {
            MouseState state = Mouse.GetState();
            return (state.RightButton == ButtonState.Pressed);
        }

        public Vector2 GetDirection() {
            KeyboardState state = Keyboard.GetState();
            var pressed = Keyboard.GetState().GetPressedKeys().ToList();
            var pressed_wasd = pressed.Intersect(wasd);

            float X = 0.0f;
            float Y = 0.0f;
            foreach (Keys key in pressed_wasd) {
                switch (key) {
                    case Keys.W:
                        Y -= 1.0f;
                        break;
                    case Keys.A:
                        X -= 1.0f;
                        break;
                    case Keys.S:
                        Y += 1.0f;
                        break;
                    case Keys.D:
                        X += 1.0f;
                        break;
                    default:
                        break;
                }
            }
            return new Vector2(X, Y);
        }

        public void DrawDebug(SpriteBatch sb) {
            Vector2 pos = DebugPosition;
            string msg;

            KeyboardState state = Keyboard.GetState();
            var pressed = Keyboard.GetState().GetPressedKeys().ToList();
            var pressed_wasd = pressed.Intersect(wasd);

            Debug.DrawText(sb, pos, "keyboard");
            pos.Y += 30.0f;
            msg = "WASD: ";
            foreach (Keys key in pressed_wasd) {
                msg += key.ToString() + " ";
            }
            Debug.DrawText(sb, pos, msg);
            pos.Y += 30.0f;
            Debug.DrawText(sb, pos, "Direction(x;y): "
                                    + GetDirection().X.ToString("N2")
                                    + " ; "
                                    + GetDirection().Y.ToString("N2"));
            pos.Y += 30.0f;
            Debug.DrawText(sb, pos, "Button1: " + GetButton1().ToString());
            pos.Y += 30.0f;
            Debug.DrawText(sb, pos, "Button2: " + GetButton2().ToString());
        }
    }
}
