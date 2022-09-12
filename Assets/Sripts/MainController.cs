using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MenuGameObject;
    public GameObject HelpGameObject;

    private void Start()
    {
        goMainMenu();
    }


    public void ReturnFromPlay()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }
    public void GoPlay()
    {
        Loader.Load(Loader.Scene.Imagetracking);
    }

    public void goHelp()
    {
        MenuGameObject.SetActive(false);
        HelpGameObject.SetActive(true);
    }

    public void goMainMenu()
    {
        HelpGameObject.SetActive(false);
        MenuGameObject.SetActive(true);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

}
