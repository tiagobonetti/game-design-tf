using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    class Timer {
        float elapsedTime = 0;

        public void TimerCounter(GameTime gameTime, float length, out bool ended) {
            if (length > 0) {
                if (elapsedTime < length) {
                    elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    ended = false;
                }
                else {
                    elapsedTime = 0;
                    ended = true;
                }
         //       System.Diagnostics.Debug.WriteLine("Timer: " + elapsedTime);
            }
            else {
                ended = false;
            }
        }
    }
}
