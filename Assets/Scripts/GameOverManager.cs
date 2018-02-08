using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public float timeToRestart;
    public Text countdown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        float remainingTime = timeToRestart - Time.timeSinceLevelLoad;
        if (remainingTime < 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    void LateUpdate ()
	{
	    float remainingTime = timeToRestart - Time.timeSinceLevelLoad;
	    countdown.text = Mathf.RoundToInt(remainingTime).ToString();

	}
}
