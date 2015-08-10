using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
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

    public abstract class BaseCharacter : GameObject {
        public const float speed_Walk = 500;
        public const float acceleration_Walk = 0.1f;
        public const float deceleration_Walk = 0.05f;
        public const float speed_Run = 1000;
        public const float acceleration_Run = 0.1f;
        public const float deceleration_Run = 0.05f;

        public bool canControl = true;
        public bool canMove = true;

        public BaseCharacter characterHit;
        public bool collided;
        public bool dead = false;

        public Timer respawnTimer;
        public float respawnTime = 1.0f;

        public static Random random = new Random();
        public Vector2 defaultSpawnPosition = new Vector2(0.0f, 0.0f);
        public Vector2 SpawnPosition {
            set {
                defaultSpawnPosition = value;
            }
            get {
                Vector2 spawnPosition = defaultSpawnPosition;
                Vector2 offset = Vector2.Zero;
                while ( GetCollisions(spawnPosition + offset) != null) {
                    float randomX = (float)random.NextDouble() - 0.5f;
                    float randomY = (float)random.NextDouble() - 0.5f;
                    offset = (150.0f * new Vector2(randomX, randomY));
                }
                return spawnPosition + offset;
            }
        }

        //Debug
        public Vector2 DebugPosition { get; set; }

        public IPlayerInput input;
        Animator animator = new Animator();

        public BaseCharacter(Texture2D sprite, MainGame.Tag tag, Vector2 spawnPosition, string name, MainGame game, IPlayerInput input)
            : base(sprite, tag, spawnPosition, name, game) {
            baseRectangle = new Rectangle(0, 0, 50, 50);
            velocity = Vector2.Zero;
            this.SpawnPosition = spawnPosition;
            this.input = input;
            Reset();
        }

        public virtual void Reset() {
            respawnTimer = new Timer();
            characterHit = null;
            collided = false;
            dead = false;
        }

        public void Respawn() {
            position = SpawnPosition;
            Reset();
        }

        override public void Draw(SpriteBatch spriteBatch) {
            if (!dead) {
                base.Draw(spriteBatch);
            }

            if (input != null) {
                input.DrawDebug(spriteBatch);
            }

            Vector2 stringSize = Debug.MeasureString(name);
            Debug.DrawText(game.spriteBatch, position - (stringSize * 0.5f), name);

            string msg = "Name: " + name;
            msg += "\nDead: " + dead.ToString();
            msg += "\nP: " + position.ToString();
            msg += "\nV: " + velocity.ToString();
            msg += "\nRespawn: " + respawnTimer.GetTimeDecreasing().ToString();
            Debug.DrawText(game.spriteBatch, DebugPosition, msg);
        }

        protected void UpdateRespawn(GameTime gameTime) {
            bool ended = false;
            respawnTimer.TimerCounter(gameTime, respawnTime, out ended);
            if (ended) {
                Respawn();
            }
        }

        protected void UpdateMovement(GameTime gameTime) {

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

            Vector2 newPosition = position + (velocity * speed_Walk) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Clamp position to scene borders
            newPosition = new Vector2(MathHelper.Clamp(newPosition.X, 0 + baseRectangle.Width * 0.5f,
                                                       game.graphics.PreferredBackBufferWidth - baseRectangle.Width * 0.5f),
                                      MathHelper.Clamp(newPosition.Y, 0 + baseRectangle.Height * 0.5f,
                                                       game.graphics.PreferredBackBufferHeight - baseRectangle.Height * 0.5f));

            characterHit = null;
            characterHit = GetCollisions(newPosition);

            if (characterHit == null) {
                position = newPosition;
            }
            else {
                collided = true;
                velocity = Vector2.Zero;
            }
        }


        public BaseCharacter GetCollisions(Vector2 position) {

            List<BaseCharacter> characterList = game.sceneControl.gameplay.characterList;
            characterList = characterList.Where(character => character != this).ToList();
            characterList = characterList.Where(character => !character.dead).ToList();

            Rectangle newCollisionRectangle = new Rectangle((int)position.X,
                                                         (int)position.Y,
                                                         (int)baseRectangle.Width,
                                                         (int)baseRectangle.Height);

            BaseCharacter characterHit = null;
            foreach (BaseCharacter character in characterList) {
                if (newCollisionRectangle.Intersects(character.CollisionRectangle)) {
                    characterHit = character;
                    break;
                }
            }
            return characterHit;
        }

        public void Die() {
            dead = true;
        }


    }
}