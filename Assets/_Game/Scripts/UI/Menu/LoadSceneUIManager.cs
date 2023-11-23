using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneUIManager : SingletonBehaviourDontDestroy<LoadSceneUIManager>
{
    private const string NAMESCENE_MAINMENU = "MainMenuScene";
    private const string NAMESCENE_INGAME = "PlayGameScene";
    [SerializeField] private LoadSceneUI loadSceneUI;
    [SerializeField] private Camera camera;

    private void Start()
    {
        camera.gameObject.SetActive(false);
    }

    public void LoadMainMenu()
    {
        LoadScene(NAMESCENE_MAINMENU);
    }

    public void LoadPlayGame()
    {
        LoadScene(NAMESCENE_INGAME);
    }

    private async void LoadScene(string nameScene)
    {
        if (camera.gameObject.activeSelf) return;
        var scene = SceneManager.LoadSceneAsync(nameScene);
        camera.gameObject.SetActive(true);
        do
        {
            await Task.Delay(100);
            loadSceneUI.SetFill(scene.progress);
        } while (scene.progress < 0.9f);

        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        camera.gameObject.SetActive(false);
    }
}
