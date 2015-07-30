using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {

    public enum CharacterState {
        Idle,
        Moving,
        Dead,
    }

    public class BaseCharacter : GameObject {
        public const float maxSpeed_Walk = 500;
        public const float maxSpeed_Run = 1000;
        public const float acceleration_Walk = 300;
        public const float acceleration_Run = 200;
        public bool canControl = true;
        public bool canMove = true;
        public CharacterState state = CharacterState.Idle;
        public Vector2 velocity = Vector2.Zero;
        float currentSpeed;
        CharacterState previousState;

        Animator animator = new Animator();

        public BaseCharacter(Texture2D spriteSheet, MainGame.Tag tag, Vector2 position, string name, MainGame game)
            : base(spriteSheet, tag, position, name, game) {

            uvRect = new Rectangle(0, 0, 50, 50);
            velocity = Vector2.Zero;
            uvRect.Location = new Point(uvRect.Width * 5, uvRect.Height * 0);
        }

        void EvalInput(GameTime gameTime, CharacterState newState) {
            //System.Diagnostics.Debug.WriteLine("State: " + state.ToString() + " Input: " + input.ToString());

            if (newState == state) {
            }

            switch (state) { 
            }
        }

        void ChangeState(CharacterState newState, GameTime gameTime) {
            System.Diagnostics.Debug.WriteLine("OnEntry: State: " + newState.ToString() + " Name: " + name);
            previousState = state;
            state = newState;
            // On exit
            switch (previousState) {
            }
            // On Entry
            switch (state) {
                
            }
        }

        void StateMachine(GameTime gameTime, CharacterState newState) {
            EvalInput(gameTime, newState);
            switch (state) {
            }
        }

        protected void BaseUpdate(GameTime gameTime, CharacterState newState) {
            StateMachine(gameTime, newState);
            if (canControl) {
                if (canMove) {
                    Movement(gameTime);
                }
                Action(gameTime);
            }
        }

        void Movement(GameTime gameTime) {

            if (Keyboard.GetState().IsKeyDown(Keys.W) &&
                Keyboard.GetState().IsKeyUp(Keys.S)) {
                velocity.Y = -1f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) &&
                Keyboard.GetState().IsKeyUp(Keys.W)) {
                velocity.Y = 1f;
            }
            else {
                velocity.Y = 0f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) &&
                Keyboard.GetState().IsKeyUp(Keys.D)) {
                velocity.X = -1f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) &&
                Keyboard.GetState().IsKeyUp(Keys.A)) {
                velocity.X = 1f;
            }
            else {
                velocity.X = 0;
            }
            
         //   currentSpeed += acceleration_Walk * (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentSpeed = maxSpeed_Walk;
            position += (velocity * currentSpeed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
       //     MathHelper.Clamp((int)currentSpeed, -(int)maxSpeed_Walk, (int)maxSpeed_Walk);
            System.Diagnostics.Debug.WriteLine(position);
        }

        void Action(GameTime gameTime) {
            
        }

        public bool TakeHit(GameTime gameTime) {
            System.Diagnostics.Debug.WriteLine("Take Hit!");
            return true;
        }
    }
}