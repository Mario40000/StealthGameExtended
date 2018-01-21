using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    //Metodo para empezar
	public void StartGame()
    {
        SceneManager.LoadScene("Stealth");
    }
    //Metodo para quitar el juego
    public void ExitGame()
    {
        Application.Quit();
    }
    //Metodo para volver al menu
    public void ReturnTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
