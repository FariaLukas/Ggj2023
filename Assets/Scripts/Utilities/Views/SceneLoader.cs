using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference sceneToLoad;
    public const string sceneSave = "LastActiveScene";

    public void LoadScene()
    {
        if (sceneToLoad == null) return;
        LoadScene(sceneToLoad);
    }

    public void LoadScene(SceneReference scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLastActiceScene()
    {
        if (ES3.KeyExists(sceneSave))
        {
            int lastScene = ES3.Load<int>(sceneSave);
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            LoadScene();
        }
    }
}
