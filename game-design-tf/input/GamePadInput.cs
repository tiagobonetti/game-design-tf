using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game_design_tf {

    class GamePadInput : IPlayerInput {
        public Vector2 DebugPosition { get; set; }
        const float threshold = 0.75f;
        PlayerIndex player;

        public GamePadInput(PlayerIndex player = PlayerIndex.One) {
            this.DebugPosition = Vector2.Zero;
            this.player = player;
        }
        public InputState GetInput() {
            return InputState.None;
        }

        public InputState GetStick(Vector2 ThumbStick) {
            if (ThumbStick.X > threshold) {
            }
            else if (ThumbStick.X < -threshold) {
            }
            else if (ThumbStick.Y > threshold) {
            }
            else if (ThumbStick.Y < -threshold) {
            }
            else {
                ;
            }
                return InputState.None;
        }

        public void DrawDebug(SpriteBatch sb) {
            /*
            GamePadState state = GamePad.GetState(player, GamePadDeadZone.IndependentAxes);
            Vector2 pos = DebugPosition;
            InputState left = GetStick(state.ThumbSticks.Left, orientation);
            InputState right = GetStick(state.ThumbSticks.Right, orientation);
            MenuInput menu = GetMenuInput();

            Debug.DrawText(sb, pos, "P " + player.ToString() + " : " + state.IsConnected.ToString());
            pos.Y += 30.0f;
            Debug.DrawText(sb, pos, "Lx: " + state.ThumbSticks.Left.X.ToString());
            pos.Y += 25.0f;
            Debug.DrawText(sb, pos, "Ly: " + state.ThumbSticks.Left.Y.ToString());
            pos.Y += 25.0f;
            Debug.DrawText(sb, pos, "Rx: " + state.ThumbSticks.Right.X.ToString());
            pos.Y += 25.0f;
            Debug.DrawText(sb, pos, "Ry: " + state.ThumbSticks.Right.Y.ToString());
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
