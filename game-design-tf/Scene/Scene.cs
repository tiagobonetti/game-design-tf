using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Scene {

        public IList<Bomb> bombList = new List<Bomb>();
        public IList<Flag> flagList = new List<Flag>();
        public MainGame game;
    }
}
