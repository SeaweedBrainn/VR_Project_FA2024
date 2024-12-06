using UnityEngine;
using UnityEngine.SceneManagement;

public class unloadScene : MonoBehaviour
{
    public string sceneToUnLoad;

    public void UnLoadScene()
    {
        //Resources.UnloadUnusedAssets();
        SceneManager.UnloadSceneAsync(sceneToUnLoad);
    }
}