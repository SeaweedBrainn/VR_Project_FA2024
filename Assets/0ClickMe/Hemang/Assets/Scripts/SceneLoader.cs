using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool additive = true;
    public string sceneToLoad;
    
    public void LoadScene()
    {
        if (additive)
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        else SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}