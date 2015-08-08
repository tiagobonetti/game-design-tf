using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    class Animator {
        public int FrameIndex { get; private set; }
        public bool PlayedToFrame { get; private set; }
        public Animation currentAnimation;
        public State state = State.Stop;
        public float elapsedTime = 9999999;
        GameObject currentGameObject;
        GameTime gameTime;
        int startFrame;

        public Animator() {
            startFrame = 0;
            FrameIndex = 0;
        }

        public enum State {
            Play,
            PlayLoop,
            PlayLoopBackwards,
            Stop,
            RollBack
        }

        public void EnterState(State newState) {

            if (newState != state) {
                state = newState;

                switch (state) {
                    case State.Play:
                        FrameIndex = startFrame;
                        elapsedTime = 9999999;
                        break;
                    case State.PlayLoop:
                        FrameIndex = startFrame;
                        elapsedTime = 9999999;
                        break;
                    case State.PlayLoopBackwards:
                        FrameIndex = startFrame;
                        elapsedTime = 9999999;
                        break;
                    case State.Stop:
                        elapsedTime = 9999999;
                        break;
                    case State.RollBack:
                        elapsedTime = 9999999;
                        break;
                }
            }
        }

        void StateMachine() {
            if (currentGameObject != null)
                if (currentGameObject.name == "p1") {
            //        System.Diagnostics.Debug.WriteLine(state + " Index " + FrameIndex + " Size " + currentAnimation.size);
                }
            switch (state) {
                case State.Play:
                    PlayAnimation();
                    break;
                case State.PlayLoop:
                    PlayLoopAnimation();
                    break;
                case State.PlayLoopBackwards:
                    PlayLoopAnimationBackwards();
                    break;
                case State.Stop:
                    break;
                case State.RollBack:
                    RollBackAnimation();
                    break;
            }
        }

        public void Update() {
            StateMachine();
        }

        Animation CurrentAnimation {
            get {
                return currentAnimation;
            }
            set {
                FrameIndex = startFrame;
                elapsedTime = 9999999;
                currentAnimation = value;
            }
        }

        public void Play(Animation animation, GameObject gameObject, GameTime gameTime) {
            currentGameObject = gameObject;
            CurrentAnimation = animation;
            this.gameTime = gameTime;
            startFrame = animation.startIndex;
            EnterState(State.Play);
        }

        public void PlayLoop(Animation animation, GameObject gameObject, GameTime gameTime) {
            currentGameObject = gameObject;
            CurrentAnimation = animation;
            this.gameTime = gameTime;
            startFrame = animation.startIndex;
            EnterState(State.PlayLoop);
        }

        public void PlayLoopBackwards(Animation animation, GameObject gameObject, GameTime gameTime) {
            currentGameObject = gameObject;
            currentAnimation = animation;
            this.gameTime = gameTime;
            startFrame = animation.startIndex;
            EnterState(State.PlayLoopBackwards);
        }

        public void Stop() {
            EnterState(State.Stop);
        }

        public void RollBack() {
            EnterState(State.RollBack);
        }

        void PlayAnimation() {

            if (elapsedTime < currentAnimation.animationLength) {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else {
                elapsedTime = 0;
                currentGameObject.baseRectangle = new Rectangle(currentAnimation.spriteRectPosition.X * FrameIndex,
                                                         currentAnimation.spriteRectPosition.Y,
                                                         currentGameObject.baseRectangle.Width,
                                                         currentGameObject.baseRectangle.Height);
                FrameIndex++;
            }
            if (FrameIndex > currentAnimation.size - 1) {
                EnterState(State.Stop);
            }

            // System.Diagnostics.Debug.WriteLine("PlayAnimation Index " + FrameIndex);
        }

        void PlayLoopAnimation() {

            if (elapsedTime < currentAnimation.animationLength) {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else {
                elapsedTime = 0;
                currentGameObject.baseRectangle = new Rectangle(currentAnimation.spriteRectPosition.X * FrameIndex,
                                                         currentAnimation.spriteRectPosition.Y,
                                                         currentGameObject.baseRectangle.Width,
                                                         currentGameObject.baseRectangle.Height);
                FrameIndex++;
                // System.Diagnostics.Debug.WriteLine("PlayLoopAnimation Index " + FrameIndex);
            }
            if (FrameIndex > currentAnimation.size - 1) {
                FrameIndex = startFrame;
            }
        }

        void PlayLoopAnimationBackwards() {

            if (elapsedTime < currentAnimation.animationLength) {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else {
                elapsedTime = 0;
                // System.Diagnostics.Debug.WriteLine("PlayLoopAnimationBackwards Index " + FrameIndex);
                currentGameObject.baseRectangle = new Rectangle(currentAnimation.spriteRectPosition.X * FrameIndex,
                                                         currentAnimation.spriteRectPosition.Y,
                                                         currentGameObject.baseRectangle.Width,
                                                         currentGameObject.baseRectangle.Height);
                FrameIndex--;
            }
            if (FrameIndex <= currentAnimation.size) {
                FrameIndex = startFrame;
            }
        }

        void RollBackAnimation() {

            if (elapsedTime < currentAnimation.animationLength) {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else {
                elapsedTime = 0;
                FrameIndex--;
                if (FrameIndex >= 0)
                    currentGameObject.baseRectangle = new Rectangle(currentAnimation.spriteRectPosition.X * FrameIndex,
                                                             currentAnimation.spriteRectPosition.Y,
                                                             currentGameObject.baseRectangle.Width,
                                                             currentGameObject.baseRectangle.Height);
            }
            if (FrameIndex <= 0) {
                EnterState(State.Stop);
            }
            // System.Diagnostics.Debug.WriteLine("Rollback Index " + FrameIndex);
        }
    }
}