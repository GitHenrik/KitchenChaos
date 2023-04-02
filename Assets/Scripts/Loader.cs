using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// cannot be attached to an object or have instances of itself
public static class Loader
{

  public enum Scene
  {
    MainMenuScene,
    GameScene,
    LoadingScene
  }
  private static Scene targetScene;

  public static void Load(Scene targetScene)
  {
    Loader.targetScene = targetScene;
    SceneManager.LoadScene(Scene.LoadingScene.ToString());
  }

  // called after loading scene is rendered
  public static void LoaderCallback()
  {
    SceneManager.LoadScene(targetScene.ToString());
  }
}
