using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Loader 
{
    public enum Scene
    {
        MainMenu,
        GameScene,
        LoadingScreen
        
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScreen.ToString());

    }

    public static void LoadercallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());

    }
}
