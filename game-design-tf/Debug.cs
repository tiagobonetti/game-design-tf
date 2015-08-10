using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game_design_tf {
    public static class Debug {
        static SpriteFont arial20;
        static bool enabled = true;
        static KeyboardState previousButtonState;

        public static void LoadContent(ContentManager content) {
            arial20 = content.Load<SpriteFont>("Arial20");
        }

        public static void DrawText(SpriteBatch sb, Vector2 pos, string text) {
            if (enabled)
                sb.DrawString(arial20, text, pos, Color.Cyan, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
        }
        public static Vector2 MeasureString(string text) {
                return arial20.MeasureString(text);
        }

        public static void Update() {
            if (Keyboard.GetState().IsKeyDown(Keys.F1) && previousButtonState != Keyboard.GetState()) {
                if (enabled)
                    enabled = false;
                else
                    enabled = true;
            }
            previousButtonState = Keyboard.GetState();
        }
    }
}
