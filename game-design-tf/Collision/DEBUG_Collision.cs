using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    class DEBUG_Collision {

        public static IList<CollisionBox> bodyCollisionList = new List<CollisionBox>();
        public static CollisionBox p1AttackCollisionLeft;
        public static CollisionBox p1AttackCollisionRight;
        KeyboardState previousButtonState;
        bool enabled = true;

        public void Update(GameTime gameTime){
            if (Keyboard.GetState().IsKeyDown(Keys.F2) && previousButtonState != Keyboard.GetState()) {
                if (enabled)
                    enabled = false;
                else
                    enabled = true;
            }
            previousButtonState = Keyboard.GetState();
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (enabled) {
                if (bodyCollisionList.Count > 0) {
                    foreach (CollisionBox col in bodyCollisionList) {
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

                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
                        spriteBatch.Draw(rectTexture, new Vector2(rect.X, rect.Y), Color.White);
                        spriteBatch.End();
                    }
                }

                if (p1AttackCollisionLeft != null) {
                    if (p1AttackCollisionLeft.rect.Width <= 0 || p1AttackCollisionLeft.rect.Height <= 0)
                        throw new Exception("Width/Height can't be zero!");

                    Rectangle p1Rect = p1AttackCollisionLeft.rect;
                    Texture2D p1RectTexture = new Texture2D(spriteBatch.GraphicsDevice, p1Rect.Width, p1Rect.Height);

                    Color[] p1Data = new Color[p1RectTexture.Width * p1RectTexture.Height];
                    for (int i = 0; i < p1Data.Length; ++i) {
                        if (p1AttackCollisionLeft.owner.tag == MainGame.Tag.Runner)
                            p1Data[i] = new Color(0, 255, 0, 1);
                        else if (p1AttackCollisionLeft.owner.tag == MainGame.Tag.Bomber)
                            p1Data[i] = new Color(255, 0, 0, 1);
                    }
                    p1RectTexture.SetData(p1Data);

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
                    spriteBatch.Draw(p1RectTexture, new Vector2(p1Rect.X, p1Rect.Y), Color.White);
                    spriteBatch.End();
                }

                if (p1AttackCollisionRight != null) {
                    if (p1AttackCollisionRight.rect.Width <= 0 || p1AttackCollisionRight.rect.Height <= 0)
                        throw new Exception("Width/Height can't be zero!");
                    Rectangle p1Rect = p1AttackCollisionRight.rect;
                    Texture2D p1RectTexture = new Texture2D(spriteBatch.GraphicsDevice, p1Rect.Width, p1Rect.Height);

                    Color[] p1Data = new Color[p1RectTexture.Width * p1RectTexture.Height];
                    for (int i = 0; i < p1Data.Length; ++i) {
                        if (p1AttackCollisionRight.owner.tag == MainGame.Tag.Runner)
                            p1Data[i] = new Color(255, 0, 0, 1);
                        else if (p1AttackCollisionRight.owner.tag == MainGame.Tag.Bomber)
                            p1Data[i] = new Color(255, 0, 0, 1);
                    }
                    p1RectTexture.SetData(p1Data);

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
                    spriteBatch.Draw(p1RectTexture, new Vector2(p1Rect.X, p1Rect.Y), Color.White);
                    spriteBatch.End();
                }
            }
        }
    }
}
