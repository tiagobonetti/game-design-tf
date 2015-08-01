using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_design_tf {

    public interface IPlayerInput {
        Vector2 DebugPosition { get; set; }
        Vector2 GetDirection();
        bool GetButton1();
        bool GetButton2();
        void DrawDebug(SpriteBatch sb);
    }
}
