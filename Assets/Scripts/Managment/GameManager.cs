using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        if (Time.timeScale == 1)
            PauseGame();
        else
            UnPauseGame();
    }

    private void OnDestroy()
    {
        UnPauseGame();
    }
}
