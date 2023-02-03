using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLastActiveScene : MonoBehaviour
{
    [SerializeField] private int FirstLevelBuildIndex = 1;
    public const string sceneSave = "LastActiveScene";

    private void Start()
    {
        ES3.Save(sceneSave, SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OnComplete()
    {
        ES3.Save(sceneSave, FirstLevelBuildIndex);
    }
}
