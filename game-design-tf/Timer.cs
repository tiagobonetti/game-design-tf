using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    class Timer {
        float elapsedTime = 0;
        float length;

        public void TimerCounter(GameTime gameTime, float length, out bool ended) {
            this.length = length;
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

        public float GetTimeIncreasing() {
            return elapsedTime;
        }

        public float GetTimeDecreasing() {
            return length - elapsedTime;
        }
    }
}
