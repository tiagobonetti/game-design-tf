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
        public const float speed_Walk = 500;
        public const float acceleration_Walk = 0.1f;
        public const float deceleration_Walk = 0.05f;
        public const float speed_Run = 1000;
        public const float acceleration_Run = 0.1f;
        public const float deceleration_Run = 0.05f;
        public bool canControl = true;
        public bool canMove = true;
        public CharacterState state = CharacterState.Idle;
        public IPlayerInput input;
        CharacterState previousState;
        Animator animator = new Animator();

        public BaseCharacter(Texture2D sprite, MainGame.Tag tag, Vector2 position, string name, MainGame game, IPlayerInput input)
            : base(sprite, tag, position, name, game) {

            baseRectangle = new Rectangle(0, 0, 50, 50);
            velocity = Vector2.Zero;
            this.input = input;

        }

        override public void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            if (input != null) {
                input.DrawDebug(spriteBatch);
            }
        }

        void EvalInput(GameTime gameTime, CharacterState newState) {
            //System.Diagnostics.Debug.WriteLine("State: " + state.ToString() + " Input: " + input.ToString());
            if (newState == state) {
            }

            switch (state) {
            }
        }

        void ChangeState(CharacterState newState, GameTime gameTime) {
            //System.Diagnostics.Debug.WriteLine("OnEntry: State: " + newState.ToString() + " Name: " + name);
            previousState = state;
            state = newState;
            // On exit
            switch (previousState) {
            }
            // On Entry
            switch (state) {

            }
        }

        protected void StateMachine(GameTime gameTime, CharacterState newState) {
            EvalInput(gameTime, newState);
            switch (state) {
            }
        }

        protected void Movement(GameTime gameTime) {

            Vector2 direction = input.GetDirection();
            if (direction == Vector2.Zero) {
                velocity.X = MathHelper.Lerp(velocity.X, 0, deceleration_Walk);
                if (MathHelper.Distance(velocity.X, 0) < 0.01f)
                    velocity.X = 0;
                velocity.Y = MathHelper.Lerp(velocity.Y, 0, deceleration_Walk);
                if (MathHelper.Distance(velocity.Y, 0) < 0.01f)
                    velocity.Y = 0;
            }
            else {
                velocity += direction * acceleration_Walk;
            }
            velocity = new Vector2(MathHelper.Clamp(velocity.X, -1f, 1f), MathHelper.Clamp(velocity.Y, -1f, 1f));

            position += (velocity * speed_Walk) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Clamp position to scene borders
            position = new Vector2(MathHelper.Clamp(position.X, 0 + baseRectangle.Width * 0.5f, game.graphics.PreferredBackBufferWidth - baseRectangle.Width * 0.5f),
                                   MathHelper.Clamp(position.Y, 0 + baseRectangle.Height * 0.5f, game.graphics.PreferredBackBufferHeight - baseRectangle.Height * 0.5f));
        }

        protected void Action(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                //Run modifier.
            }
        }

        public bool TakeHit(GameTime gameTime) {
            System.Diagnostics.Debug.WriteLine("Take Hit!");
            return true;
        }
    }
}