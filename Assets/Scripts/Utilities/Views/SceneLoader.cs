using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference sceneToLoad;

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
}
