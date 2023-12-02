using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneUIManager : SingletonBehaviourDontDestroy<LoadSceneUIManager>
{
    private const string NAMESCENE_MAINMENU = "MainMenuScene";
    private const string NAMESCENE_INGAME = "PlayGameScene";
    [SerializeField] private LoadSceneUI loadSceneUI;
    [SerializeField] private Camera camera;

    public enum LoadingState
    {
        Loading,
        Done
    }
    
    public LoadingState State { get; private set; }

    private void Start()
    {
        camera.gameObject.SetActive(false);
    }

    public async void LoadMainMenu()
    {
        LoadScene(NAMESCENE_MAINMENU);
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    public async void LoadPlayGame()
    {
        LoadScene(NAMESCENE_INGAME);
        GameManager.Instance.ChangeState(GameState.InMode);
    }

    private AsyncOperation scene;

    private async void LoadScene(string nameScene)
    {
        GameManager.Instance.ChangeState(GameState.Loading);
        State = LoadingState.Loading;
        if (camera.gameObject.activeSelf) return;
        scene = SceneManager.LoadSceneAsync(nameScene);
        camera.gameObject.SetActive(true);
        do
        {
            await Task.Delay(100);
            if(loadSceneUI && scene != null) loadSceneUI.SetFill(scene.progress);
        } while (scene != null && scene.progress < 0.9f);

        await Task.Delay(1000);
        State = LoadingState.Done;
    }
    
    public void SetOffLoading(){
        if (scene != null)
        {
            scene.allowSceneActivation = true;
            camera.gameObject.SetActive(false);
        }

        scene = null;
    }
}
