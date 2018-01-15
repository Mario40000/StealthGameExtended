using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

        StartCoroutine(Counter());
	
	}
	
	IEnumerator Counter()
    {
        for (int i = 0; i < 7;i++)
        {
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
