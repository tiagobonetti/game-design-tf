using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_design_tf {
    public enum SceneType {
        MainMenu,
        Gameplay,
        Transition,
        None
    }

    public class SceneControl {

        public SceneType currentScene { get; private set; }
        public Scene_Gameplay gameplay { get; set; }
        public SceneTransition transition { get; set; }

        public bool transitioning;
        SceneType previousScene;
        MainGame game;

        public SceneControl(MainGame game) {
            this.game = game;
            gameplay = new Scene_Gameplay(game);
            currentScene = SceneType.None;
        }

        public void EnterScene(SceneType scene, SceneTransition.Type transitionType, float length) {
            transitioning = true;
            transition = new SceneTransition(game);
            transition.StartFade(transitionType, scene, length);
        }

        public void EnterScene(SceneType scene) {
            previousScene = currentScene;
            currentScene = scene;
            switch (currentScene) {
                default:
                    break;
                case SceneType.MainMenu:
                    break;
                case SceneType.Gameplay:
                    break;
            }
        }

        public void Update(GameTime gameTime) {
            switch (currentScene) {
                default:
                    break;
                case SceneType.Gameplay:
                    gameplay.Update(gameTime);
                    break;
            }
            if (transitioning)
                transition.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, ContentManager content, GraphicsDeviceManager graphics) {
            switch (currentScene) {
                default:
                    break;
                case SceneType.Gameplay:
                    gameplay.Draw();
                    break;
            }
            if (transitioning)
                transition.Draw();
        }

        public Scene GetScene() {
            switch (currentScene) {
                default: return null;
                case SceneType.Gameplay: return (Scene)gameplay;
            }
        }
    }
}
