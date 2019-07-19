using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//public static string GameMode = "EASY";

public class StartGameButtonEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void onEasyModeButtonClick()
    {
       
        SceneManager.LoadScene(sceneName: "SampleScene");
        PlayerPrefs.SetString("mode", "easy"); 
    }
    public void onLegendModeButtonClick()
    {

        SceneManager.LoadScene(sceneName: "SampleScene");
        PlayerPrefs.SetString("mode", "legend");
    }

}
