using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void ButtonHandle()
    {
        var buttonName = EventSystem.current.currentSelectedGameObject.name;

        switch (buttonName)
        {
            case "Start":
                SceneManager.LoadScene("playGame");
                break;
            case "Credits":
                SceneManager.LoadScene("credits");
                break;
            case "EndGame":
                Application.Quit();
                break;
        }
        
    }
}
