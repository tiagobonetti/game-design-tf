using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_design_tf {

    public enum InputState {
        None,
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft,
    }

    public interface IPlayerInput {
        Vector2 DebugPosition { get; set; }
        InputState GetInput();
        void DrawDebug(SpriteBatch sb);
    }
}
