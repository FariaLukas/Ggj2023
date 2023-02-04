using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference sceneToLoad;
    [SerializeField] private AudioPlayer Audio;
    public const string sceneSave = "LastActiveScene";

    public void LoadScene()
    {
        if (sceneToLoad == null) return;
        LoadScene(sceneToLoad);
    }

    public void LoadScene(SceneReference scene)
    {
        SceneManager.LoadScene(scene);
        PlayAudio();
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayAudio();
    }

    public void LoadLastActiceScene()
    {
        if (ES3.KeyExists(sceneSave))
        {
            int lastScene = ES3.Load<int>(sceneSave);
            if (lastScene != 0)
            {
                SceneManager.LoadScene(lastScene);
                PlayAudio();
            }
            else LoadScene();
        }
        else
        {
            LoadScene();
        }
    }

    public void DeleteSave()
    {
        ES3.DeleteKey(AudioMute.muteSave);
        ES3.DeleteKey(SetLastActiveScene.sceneSave);
        ES3.DeleteKey(Inventory.storageSave);
        ES3.DeleteDirectory(ES3Settings.defaultSettings);
    }

    private void PlayAudio()
    {
        if (Audio)
            Audio.PlaySfx();
    }
}
