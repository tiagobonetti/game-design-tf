using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Scene {
        public IList<GameObject> gameObjectList = new List<GameObject>();
        public IList<Bomb> bombList = new List<Bomb>();
        public GameObject obj2;
        public MainGame game;
    }
}
