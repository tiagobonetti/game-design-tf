﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public class SceneGameplay : Scene {

        public const int maxScore = 1;
        public Rectangle scoringArea;
        public int roundNumber = 0;
        public float gameLength = 60;
        Scoreboard scoreboard = new Scoreboard();
        Timer timer = new Timer();
        SpriteFont arial20;
        Timer endGameTimer = new Timer();
        State state;

        public List<BaseCharacter> characterList = new List<BaseCharacter>();
        Runner runner;
        Bomber bomberA;
        Bomber bomberB;
        Bomber bomberC;

        Flag flag;
        Texture2D bg;

        public SceneGameplay(MainGame game) {
            this.game = game;
            Load();
        }

        public SceneGameplay(MainGame game, int roundNumber, Scoreboard scoreBoard) {
            this.game = game;
            this.roundNumber = roundNumber;
            this.scoreboard = scoreBoard;
            System.Diagnostics.Debug.WriteLine("Round " + roundNumber + " Score " + scoreboard.Score[0]);
            Load();
        }

        enum State {
            BuildGameObjects,
            PreGame,
            Play,
            EndRound,
            GameEnd
        }

        void ChangeState(State newState) {
            switch (newState) {
                case State.BuildGameObjects:
                    break;
                case State.PreGame:
                    foreach (BaseCharacter character in characterList) {
                        character.Reset();
                    }
                    break;
                case State.Play:
                    break;
                case State.EndRound:
                    break;
                case State.GameEnd:
                    break;
                default:
                    break;
            }
            state = newState;
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
                    foreach (BaseCharacter character in characterList) {
                        character.canControl = true;
                    }
                    Score();
                    break;
                case State.EndRound:
                    foreach (BaseCharacter character in characterList) {
                        character.canControl = false;
                    }
                    RestartRound();
                    break;
                case State.GameEnd:
                    RestartGame();
                    break;
            }
            Debug.Update();
            runner.Update(gameTime);
            bomberC.Update(gameTime);
            bomberB.Update(gameTime);
            bomberA.Update(gameTime);
            flag.Update(gameTime);
        }

        public void Draw() {

            game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            switch (state) {
                case State.BuildGameObjects:
                    DrawBackground();
                    DrawClock();
                    break;
                case State.PreGame:
                case State.Play:
                case State.EndRound:
                    DrawBackground();
                    DrawClock();
                    DrawScoreBoard();
                    flag.Draw(game.spriteBatch);
                    foreach (BaseCharacter character in characterList) {
                        character.Draw(game.spriteBatch);
                    }
                    break;
                case State.GameEnd:
                    break;
            }
            game.spriteBatch.End();
        }

        void PreFight(GameTime gameTime) {
            bool timerEnded;
            timer.TimerCounter(gameTime, 0.01f, out timerEnded);
            if (timerEnded) {
                ChangeState(State.Play);
            }
        }

        void Score() {
            if (runner.flag != null && scoringArea.Contains(runner.position)) {
                scoreboard.AddScore(MainGame.Tag.Runner, 1);
                ChangeState(State.EndRound);
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
            endGameTimer.TimerCounter(gameTime, gameLength, out timerEnded);
            if (GameEnded()) {
                ChangeState(State.GameEnd);
            }
            else if (timerEnded) {
                ChangeState(State.EndRound);
            }
        }

        void RestartRound() {
            game.sceneControl.gameplay = new SceneGameplay(game, roundNumber + 1, scoreboard);
            game.sceneControl.EnterScene(SceneType.Gameplay);
        }

        void RestartGame() {
            game.sceneControl.gameplay = new SceneGameplay(game);
            game.sceneControl.EnterScene(SceneType.Gameplay);
        }

        void DrawClock() {
            Vector2 pos = new Vector2(game.graphics.PreferredBackBufferWidth * 0.5f, game.graphics.PreferredBackBufferHeight * 0.1f);
            Vector2 size = arial20.MeasureString(endGameTimer.GetTimeDecreasing().ToString("N2"));
            Vector2 origin = size * 0.5f;
            game.spriteBatch.DrawString(arial20, endGameTimer.GetTimeDecreasing().ToString("N2"), pos, Color.Cyan, 0.0f, origin, 1.0f, SpriteEffects.None, 1.0f);
        }

        void DrawScoreBoard() {
           /* Vector2 pos = new Vector2(game.graphics.PreferredBackBufferWidth * 0.5f, game.graphics.PreferredBackBufferHeight * 0.9f);
            Vector2 size = arial20.MeasureString(endGameTimer.GetTimeDecreasing().ToString("N2"));
            Vector2 origin = size * 0.5f;
            game.spriteBatch.DrawString(arial20, scoreboard.Score[0].ToString(), pos, Color.Cyan, 0.0f, origin, 1.0f, SpriteEffects.None, 1.0f);*/
        }


        void DrawBackground() {
            game.graphics.GraphicsDevice.Clear(Color.Black);
        }

        public virtual void Load() {
            Runner.Load(game.Content);
            Bomber.Load(game.Content);
            arial20 = game.Content.Load<SpriteFont>("Arial20");
            ChangeState(State.BuildGameObjects);
        }

        void BuildGameObjects() {
            scoringArea = new Rectangle(0, 0, 30, game.graphics.PreferredBackBufferHeight);
            flag = new Flag(game);

            float screenWidth = game.graphics.PreferredBackBufferWidth;
            float screenHeight = game.graphics.PreferredBackBufferHeight;

            //runner
            Vector2 startingPosition = new Vector2(screenWidth * 0.1f, screenHeight * 0.5f);
            IPlayerInput input = new KeyboardInput();
            runner = new Runner(startingPosition, "Run", game, input);
            runner.DebugPosition = new Vector2(0.0f, screenHeight * 0.8f);
            characterList.Add(runner);

            // bomber A
            startingPosition = new Vector2(screenWidth * 0.35f, screenHeight * 0.15f);
            input = new GamePadInput(PlayerIndex.One);
            input.DebugPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.25f, 0.0f);
            bomberA = new Bomber(startingPosition, "A", game, input);
            bomberA.DebugPosition = new Vector2(screenWidth * 0.25f, screenHeight * 0.8f);
            characterList.Add(bomberA);

            // bomber B
            startingPosition = new Vector2(screenWidth * 0.55f, screenHeight * 0.15f);
            input = new GamePadInput(PlayerIndex.Two);
            input.DebugPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.50f, 0.0f);
            bomberB = new Bomber(startingPosition, "B", game, input);
            bomberB.DebugPosition = new Vector2(screenWidth * 0.50f, screenHeight * 0.8f);
            bomberB.velocity = Vector2.Zero;
            characterList.Add(bomberB);

            // bomber C
            startingPosition = new Vector2(screenWidth * 0.75f, screenHeight * 0.85f);
            input = new GamePadInput(PlayerIndex.Three);
            input.DebugPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.75f, 0.0f);
            bomberC = new Bomber(startingPosition, "C", game, input);
            bomberC.DebugPosition = new Vector2(screenWidth * 0.75f, screenHeight * 0.8f);
            bomberC.velocity = Vector2.Zero;
            characterList.Add(bomberC);

            ChangeState(State.PreGame);
        }
    }
}
