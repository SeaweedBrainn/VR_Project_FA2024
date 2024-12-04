using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadScene()
    {
        //Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}