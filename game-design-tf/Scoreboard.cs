using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Scoreboard {
        public float[] Score {get; private set;}

        public Scoreboard() {
            Score = new float[2];
            Score[0] = 0;
            Score[1] = 0;
        }

        public void AddScore(MainGame.Tag tag, float score) {
            if (tag == MainGame.Tag.Runner)
                this.Score[0] += score;
            else
                this.Score[1] += score;
            System.Diagnostics.Debug.WriteLine("Score!");
        }
    }
}
