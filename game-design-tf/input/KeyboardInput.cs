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
        static List<Keys> arrows = new List<Keys>() {
            Keys.Up,
            Keys.Left,
            Keys.Down,
            Keys.Right
        };
       
        public InputState GetInput() {
            KeyboardState state = Keyboard.GetState();
            var pressed = Keyboard.GetState().GetPressedKeys().ToList();
            var pressed_wasd = pressed.Intersect(wasd);
            //var pressed_arrows = pressed.Intersect(arrows);

            return InputState.None;

        }

        public void DrawDebug(SpriteBatch sb) {
            /*
            Vector2 pos = DebugPosition;
            string msg;

            KeyboardState state = Keyboard.GetState();
            var pressed = Keyboard.GetState().GetPressedKeys().ToList();
            var pressed_wasd = pressed.Intersect(wasd);
            var pressed_arrows = pressed.Intersect(arrows);
            var pressed_menu = pressed.Intersect(menu_list);
 
            InputState left  = ((pressed_wasd.Count() == 1) ? pressed_wasd.First() : Keys.None).ToInput(orientation);
            InputState right = ((pressed_arrows.Count() == 1) ? pressed_arrows.First() : Keys.None).ToInput(orientation);
            MenuInput menu = ((pressed_menu.Count() > 0) ? pressed_menu.First() : Keys.None).ToMenuInput();

            Debug.DrawText(sb, pos, "keyboard");
            pos.Y += 30.0f;
            msg = "WASD: ";
            foreach (Keys key in pressed_wasd) {
                msg += key.ToString() + " ";
            }
            Debug.DrawText(sb, pos, msg);
            pos.Y += 25.0f;
            msg = "Arrows: ";
            foreach (Keys key in pressed_arrows) {
                msg += key.ToString() + " ";
            }
            Debug.DrawText(sb, pos, msg);
            pos.Y += 30.0f;
            Debug.DrawText(sb, pos, "L: " + left.ToString());
            pos.Y += 25.0f;
            Debug.DrawText(sb, pos, "R: " + right.ToString());
            pos.Y += 30.0f;
            Debug.DrawText(sb, pos, "Move: " + InputDictionary.GetMove(left, right, Modifier.None).ToString());
            pos.Y += 30.0f;
            Debug.DrawText(sb, pos, "Menu: " + menu);
            */
        }
    }
}
