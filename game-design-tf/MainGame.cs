using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace game_design_tf {
    public class MainGame : Game {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SceneControl sceneControl;

        public MainGame()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sceneControl = new SceneControl(this);
            sceneControl.EnterScene(SceneType.Gameplay);
            Debug.LoadContent(Content);
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            sceneControl.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            sceneControl.Draw(spriteBatch, Content, graphics);
            base.Draw(gameTime);
        }

        public enum Tag{
            Runner,
            Bomber,
            Flag,
            Wall
        }
    }
}
