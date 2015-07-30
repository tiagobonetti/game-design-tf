using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    class Runner : BaseCharacter {
        public IPlayerInput PlayerInput { get; set; }
        

        public Runner(Texture2D spriteSheet, MainGame.Tag tag, Vector2 position, string name, MainGame game)
            : base(spriteSheet, tag, position, name, game) {
            this.PlayerInput = null;
            collision = new CollisionBox(this, new Vector2(uvRect.Center.X, uvRect.Center.Y), new Vector2(13, 48));
            DEBUG_Collision.bodyCollisionList.Add(collision);
        }

        public void Update(GameTime gameTime) {

            CharacterState state = CharacterState.Idle;
            if (state == null)
                state = CharacterState.Idle;

            BaseUpdate(gameTime, state);
        }
    }
}
