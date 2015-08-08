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

        public IPlayerInput WhiteInput { get; set; }
        public const int maxScore = 1;
        public Rectangle scoringArea;
        public int roundNumber = 0;
        Scoreboard scoreboard = new Scoreboard();
        Timer timer = new Timer();
        SpriteFont arial20;
        Timer endGameTimer = new Timer();
        State state;
        Runner runner;
        Runner testDummy;
        Bomber bomber;
        Flag flag;
        Texture2D characterSprite;
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
            GameEnd
        }

        public void Update(GameTime gameTime) {
            switch (state) {
                case State.BuildGameObjects:
                    BuildGameObjects();
                    break;
                case State.PreGame:
                    PreFight(gameTime);
                    runner.canControl = true;
                    break;
                case State.Play:
                    CheckGameEnd(gameTime);
                    runner.canControl = true;
                    Score();
                    break;
                case State.EndRound:
                    runner.canControl = false;
                    RestartRound();
                    break;
                case State.GameEnd:
                    RestartGame();
                    break;
            }
            Debug.Update();
            runner.Update(gameTime);
            testDummy.Update(gameTime);
            bomber.Update(gameTime);
            flag.Update(gameTime);
            UpdateBombs(gameTime);
        }

        public void Draw() {

            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            switch (state) {
                case State.BuildGameObjects:
                    DrawBackground();
                    DrawClock();
                    break;
                case State.PreGame:
                    DrawBackground();
                    DrawClock();
                    flag.Draw(game.spriteBatch);
                    runner.Draw(game.spriteBatch);
                    bomber.Draw(game.spriteBatch);
                    testDummy.Draw(game.spriteBatch);
                    break;
                case State.Play:
                    DrawBackground();
                    DrawClock();
                    flag.Draw(game.spriteBatch);
                    runner.Draw(game.spriteBatch);
                    bomber.Draw(game.spriteBatch);
                    testDummy.Draw(game.spriteBatch);
                    break;
                case State.EndRound:
                    DrawBackground();
                    flag.Draw(game.spriteBatch);
                    runner.Draw(game.spriteBatch);
                    bomber.Draw(game.spriteBatch);
                    testDummy.Draw(game.spriteBatch);
                    break;
                case State.GameEnd:
                    break;
            }
            DrawBombs();
            game.spriteBatch.End();
        }

        void PreFight(GameTime gameTime) {
            bool timerEnded;
            timer.TimerCounter(gameTime, 0.01f, out timerEnded);
            if (timerEnded)
                state = State.Play;
        }

        void Score() {
            if (runner.flag != null && scoringArea.Contains(runner.position)) {
                scoreboard.AddScore(MainGame.Tag.Runner, 1);
                state = State.EndRound;
            }
        }

        bool GameEnded() {
            for (int i = 0; i < scoreboard.Score.Length; i++) {
                if (scoreboard.Score[i] >= maxScore) {
                    return true;
                }
            }
            return false;
        }

        void CheckGameEnd(GameTime gameTime) {
            bool timerEnded;
            endGameTimer.TimerCounter(gameTime, 20f, out timerEnded);
            if (GameEnded()) {
                state = State.GameEnd;
            }
            else if (timerEnded) {
                state = State.EndRound;
            }
        }

        void RestartRound() {
            game.sceneControl.gameplay = new Scene_Gameplay(game, roundNumber + 1, scoreboard);
            game.sceneControl.EnterScene(SceneType.Gameplay);
        }

        void RestartGame() {
            game.sceneControl.gameplay = new Scene_Gameplay(game);
            game.sceneControl.EnterScene(SceneType.Gameplay);
        }

        void DrawClock() {
            Vector2 pos = new Vector2(game.graphics.PreferredBackBufferWidth * 0.5f, game.graphics.PreferredBackBufferHeight * 0.1f);
            Vector2 size = arial20.MeasureString(endGameTimer.GetTimeDecreasing().ToString("N2"));
            Vector2 origin = size * 0.5f;
            game.spriteBatch.DrawString(arial20, endGameTimer.GetTimeDecreasing().ToString("N2"), pos, Color.Cyan, 0.0f, origin, 1.0f, SpriteEffects.None, 1.0f);
        }

        void DrawBackground() {
            game.graphics.GraphicsDevice.Clear(Color.Black);
            /*
            Vector2 bgPos = new Vector2(game.graphics.PreferredBackBufferWidth * 0.5f, game.graphics.PreferredBackBufferHeight * 0.5f);
            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            game.spriteBatch.Draw(bg, bgPos, null, null, new Vector2(bg.Width * 0.5f, bg.Height * 0.5f), 0f, Vector2.One * 0.75f, Color.White, SpriteEffects.None, 0f);
            game.spriteBatch.End();
             * */
        }

        void UpdateBombs(GameTime gameTime) {
            if (bombList.Count > 0) {
                foreach (Bomb bomb in bombList) {
                    bomb.Update(gameTime);
                }
                IList<Bomb> newBombList = new List<Bomb>();
                foreach (Bomb bomb in bombList) {
                    if (!bomb.Dead) {
                        newBombList.Add(bomb);
                    }
                }
                bombList = newBombList;
            }
        }

        void DrawBombs() {
            if (bombList.Count > 0) {
                foreach (Bomb bomb in bombList) {
                    bomb.Draw();
                }
            }
        }

        public virtual void Init() {
            //bg = game.Content.Load<Texture2D>("Sprites/Background/Bg");
            characterSprite = game.Content.Load<Texture2D>("Sprite/Characters/Runner");
            arial20 = game.Content.Load<SpriteFont>("Arial20");
            state = State.BuildGameObjects;
        }

        void BuildGameObjects() {

            scoringArea = new Rectangle(0, 0, 30, game.graphics.PreferredBackBufferHeight);
            flag = new Flag(game);

            IPlayerInput input = new KeyboardInput();
            Vector2 runnerStartingPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.5f, game.graphics.PreferredBackBufferHeight * 0.5f);
            runner = new Runner(characterSprite, MainGame.Tag.Runner, runnerStartingPosition, "runner", game, input);
            runner.velocity = Vector2.Zero;

            input = new GamePadInput(PlayerIndex.One);
            input.DebugPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.333f, 0.0f);
            Vector2 bomberStartingPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.2f, game.graphics.PreferredBackBufferHeight * 0.5f);
            bomber = new Bomber(characterSprite, MainGame.Tag.Bomber, bomberStartingPosition, "bomber", game, input);
            bomber.velocity = Vector2.Zero;

            Vector2 testDummyStartingPosition = new Vector2(1300f, 450f);
            input = new GamePadInput(PlayerIndex.Two);
            input.DebugPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.666f, 0.0f);
            testDummy = new Runner(characterSprite, MainGame.Tag.Runner, testDummyStartingPosition, "dummy", game, input);
            testDummy.velocity = Vector2.Zero;

            state = State.PreGame;
        }
    }
}
