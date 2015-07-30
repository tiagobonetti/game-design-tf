using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class Scene_Gameplay : Scene {

        public const float floor = 430;
        public int roundNumber = 0;
        public Texture2D colSprite;
        Scoreboard scoreboard = new Scoreboard();
        DEBUG_Collision debugCollision = new DEBUG_Collision();
        Timer timer = new Timer();
        State state;
        Runner runner;
        public IPlayerInput WhiteInput { get; set; }
        Texture2D spritesheet;
        Texture2D bg;

        public Scene_Gameplay(MainGame game) {
            this.game = game;
            Init();
        }

        public Scene_Gameplay(MainGame game, int roundNumber, Scoreboard scoreBoard) {
            this.game = game;
            this.roundNumber = roundNumber;
            this.scoreboard = scoreBoard;
            System.Diagnostics.Debug.WriteLine("Round " + roundNumber + " Score " + scoreboard.Score[0]);
            Init();
        }

        enum State {
            BuildGameObjects,
            PreGame,
            Play,
            EndRound,
        }

        public void Update(GameTime gameTime) {
            switch (state) {
                case State.BuildGameObjects:
                    BuildGameObjects();
                    break;
                case State.PreGame:
                    PreFight(gameTime);
                    runner.canControl = false;
                    break;
                case State.Play:
                    runner.canControl = true;
                    break;
                case State.EndRound:
                    runner.canControl = true;
                    EndRound(gameTime);
                    break;
            }
            debugCollision.Update(gameTime);
            Debug.Update();
            runner.Update(gameTime);
        }

        public void Draw() {
            switch (state) {
                case State.BuildGameObjects:
                    DrawBackground();
                    break;
                case State.PreGame:
                    DrawBackground();
                    runner.Draw(game.spriteBatch);
                    break;
                case State.Play:
                    DrawBackground();
                    runner.Draw(game.spriteBatch);
                    break;
                case State.EndRound:
                    DrawBackground();
                    runner.Draw(game.spriteBatch);
                    break;
            }
            debugCollision.Draw(game.spriteBatch);
        }

        void PreFight(GameTime gameTime) {
            bool timerEnded;
            timer.TimerCounter(gameTime, 0.5f, out timerEnded);
            if (timerEnded)
                state = State.Play;
        }

        bool GameEnded() {
            for (int i = 0; i < scoreboard.Score.Length; i++)
                if (scoreboard.Score[i] >= 2)
                    return true;
            return false;
        }

        void EndRound(GameTime gameTime) {
            bool timerEnded;
            timer.TimerCounter(gameTime, 2f, out timerEnded);
            if (timerEnded) {
                if (GameEnded()) {
                    game.sceneControl.gameplay = new Scene_Gameplay(game);
                    game.sceneControl.EnterScene(SceneType.MainMenu);
                }
                else
                    Restart();
            }
        }

        void Restart() {
            game.sceneControl.gameplay = new Scene_Gameplay(game, roundNumber + 1, scoreboard);
            game.sceneControl.EnterScene(SceneType.Gameplay);
        }

        void DrawBackground() {
            /*
            Vector2 bgPos = new Vector2(game.graphics.PreferredBackBufferWidth * 0.5f, game.graphics.PreferredBackBufferHeight * 0.5f);
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            game.spriteBatch.Draw(bg, bgPos, null, null, new Vector2(bg.Width * 0.5f, bg.Height * 0.5f), 0f, Vector2.One * 0.75f, Color.White, SpriteEffects.None, 0f);
            game.spriteBatch.End();
             * */
        }

        void BuildGameObjects() {

            Vector2 runnerStartingPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.5f, game.graphics.PreferredBackBufferHeight * 0.5f);
            runner = new Runner(spritesheet, MainGame.Tag.Runner, runnerStartingPosition, "p1", game);
            runner.velocity = Vector2.Zero;
            state = State.PreGame;
        }

        public virtual void Init() {
            /*
            colSprite = game.Content.Load<Texture2D>("Sprites/Main Character/AttackCollision");
            bg = game.Content.Load<Texture2D>("Sprites/Background/Bg");
            fightText = game.Content.Load<Texture2D>("GUI/Fight");
            koText = game.Content.Load<Texture2D>("GUI/KO");
             * */
            spritesheet = game.Content.Load<Texture2D>("Sprite/Characters/Runner");
            state = State.BuildGameObjects;
        }
    }
}
