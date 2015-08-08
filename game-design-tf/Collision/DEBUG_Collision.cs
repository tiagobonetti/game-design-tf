using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    class DEBUG_Collision {

        public static IList<CollisionBox> CollisionList = new List<CollisionBox>();
        KeyboardState previousButtonState;
        bool enabled = true;

        public void Update(GameTime gameTime) {
            ToggleOnOff();
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (enabled) {
                DrawCollision(spriteBatch);
            }
        }

        void ToggleOnOff() {
            if (Keyboard.GetState().IsKeyDown(Keys.F2) && previousButtonState != Keyboard.GetState()) {
                if (enabled)
                    enabled = false;
                else
                    enabled = true;
            }
            previousButtonState = Keyboard.GetState();
        }

        void DrawCollision(SpriteBatch spriteBatch) {
            if (CollisionList.Count > 0) {
                foreach (CollisionBox col in CollisionList) {
                    Rectangle rect = col.rect;
                    Texture2D rectTexture = new Texture2D(spriteBatch.GraphicsDevice, rect.Width, rect.Height);

                    Color[] data = new Color[rectTexture.Width * rectTexture.Height];
                    for (int i = 0; i < data.Length; ++i) {
                        if (col.owner.tag == MainGame.Tag.Runner)
                            data[i] = new Color(0, 0, 255, 1);
                        else if (col.owner.tag == MainGame.Tag.Bomber)
                            data[i] = new Color(0, 0, 255, 1);
                    }
                    rectTexture.SetData(data);

                    //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
                    spriteBatch.Draw(rectTexture, new Vector2(rect.X, rect.Y), Color.White);
                    //spriteBatch.End();
                }
            }
        }
    }
}
