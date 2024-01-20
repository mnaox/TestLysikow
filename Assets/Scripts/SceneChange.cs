using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangeScene(string sceneName) //Загрузка сцена по имени
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame() //Выход из игры
    {
        Application.Quit();
    }
}
