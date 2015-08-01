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
        public PlayerIndex Player { get; set; }

        const float threshold = 0.5f;

        public GamePadInput(PlayerIndex player = PlayerIndex.One) {
            this.DebugPosition = Vector2.Zero;
            this.Player = player;
        }

        public bool GetButton1() {
            GamePadState state = GamePad.GetState(Player, GamePadDeadZone.Circular);
            return state.IsButtonDown(Buttons.X);
        }

        public bool GetButton2() {
            GamePadState state = GamePad.GetState(Player, GamePadDeadZone.Circular);
            return state.IsButtonDown(Buttons.A);
        }

        public Vector2 GetDirection() {
            GamePadState state = GamePad.GetState(Player, GamePadDeadZone.Circular);
            return FilterStick(state.ThumbSticks.Left);
        }

        public float VectorToAngle(Vector2 vector) {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public Vector2 FilterStick(Vector2 ThumbStick) {
            if (ThumbStick.Length() > threshold) {
                float angle = VectorToAngle(ThumbStick);
                int slice_count = (int)(angle / (Math.PI / 8.0f));
                switch (slice_count) {
                    case 0:
                        return new Vector2(1.0f, 0.0f);
                    case 1:
                    case 2:
                        return new Vector2(1.0f, 1.0f);
                    case 3:
                    case 4:
                        return new Vector2(0.0f, 1.0f);
                    case 5:
                    case 6:
                        return new Vector2(-1.0f, 1.0f);
                    case 7:
                    case 8:
                    case -7:
                        return new Vector2(-1.0f, 0.0f);
                    case -6:
                    case -5:
                        return new Vector2(-1.0f, -1.0f);
                    case -4:
                    case -3:
                        return new Vector2(0.0f, -1.0f);
                    case -2:
                    case -1:
                        return new Vector2(1.0f, -1.0f);
                    default:
                        return Vector2.Zero;
                }
            }
            else {
                return Vector2.Zero;
            }
        }

        public void DrawDebug(SpriteBatch sb) {
            GamePadState state = GamePad.GetState(Player, GamePadDeadZone.Circular);
            Vector2 pos = DebugPosition;
            Vector2 stick = GetDirection();

            Debug.DrawText(sb, pos, "P " + Player.ToString() + " : " + state.IsConnected.ToString());
            pos.Y += 30.0f;
            Debug.DrawText(sb, pos, "Stick(x;y): "
                                    + state.ThumbSticks.Left.X.ToString("N2")
                                    + " ; "
                                    + state.ThumbSticks.Left.Y.ToString("N2"));
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
