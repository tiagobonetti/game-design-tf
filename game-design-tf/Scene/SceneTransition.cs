using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class SceneTransition {
        MainGame game;
        Texture2D bg;
        float length = 1.5f;
        float elapsedTime = 0f;
        float alpha;
        Type type;
        SceneType targetScene;

        public enum Type {
            FadeIn,
            FadeOut,
            FadeOutIn,
            None
        }

        public SceneTransition(MainGame game) {
            this.game = game;
            Init();
        }

        public void StartFade(Type type, SceneType scene, float length) {
            this.targetScene = scene;
            this.length = length;
            this.elapsedTime = 0;
            switch (type) {
                case Type.FadeIn:
                    this.type = Type.FadeIn;
                    alpha = 1;
                    break;
                case Type.FadeOut:
                    this.type = Type.FadeOut;
                    alpha = 0;
                    break;
                case Type.FadeOutIn:
                    this.type = Type.FadeOutIn;
                    alpha = 0;
                    break;
            }
        }
        
        public void Update(GameTime gameTime) {
            System.Diagnostics.Debug.WriteLine(type);
            bool fadeEnded;
            switch (type) {
                case Type.FadeIn:
                    FadeIn(gameTime, out fadeEnded);
                    game.sceneControl.EnterScene(targetScene);
                    if (fadeEnded) {
                        game.sceneControl.transitioning = false;
                    }
                    break;
                case Type.FadeOut:
                    FadeOut(gameTime, out fadeEnded);
                    if (fadeEnded) {
                        game.sceneControl.transitioning = false;
                        game.sceneControl.EnterScene(targetScene);
                    }
                    break;
                case Type.FadeOutIn:
                    FadeOut(gameTime, out fadeEnded);
                    if (fadeEnded) {
                        StartFade(Type.FadeIn, targetScene, length);
                    }
                    break;
            }
        }

        public void Draw() {
            Background();
        }

        public void FadeIn(GameTime gameTime, out bool ended) {

            if (alpha > 0 && elapsedTime < length) {
                ended = false;
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                alpha = 1 - (((elapsedTime * 100) / length) / 100);
          //      System.Diagnostics.Debug.WriteLine("FadeIn - Alpha: " + alpha + " Elapsed Time " + elapsedTime);
            }
            else {
                ended = true;
            }
        }

        public void FadeOut(GameTime gameTime, out bool ended) {

            if (alpha < 1 && elapsedTime < length) {
                ended = false;
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                alpha = ((elapsedTime * 100) / length) / 100;
           //     System.Diagnostics.Debug.WriteLine("FadeOut - Alpha: " + alpha + " Elapsed Time " + elapsedTime);
            }
            else {
                ended = true;
            }
        }

        public virtual void Init() {
            bg = game.Content.Load<Texture2D>("Sprites/Background/BlackBg");
            alpha = 0;
        }

        void Background() {
            Vector2 bgPos = new Vector2(game.graphics.PreferredBackBufferWidth * 0.5f, game.graphics.PreferredBackBufferHeight * 0.5f);
            //game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            game.spriteBatch.Draw(bg, bgPos, null, null, new Vector2(bg.Width * 0.5f, bg.Height * 0.5f), 0f, Vector2.One * 3f, new Color(Color.White, alpha), SpriteEffects.None, 0f);
            //game.spriteBatch.End();
        }
    }
}
