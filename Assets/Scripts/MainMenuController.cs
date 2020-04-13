using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public enum MenuState { StartUp,LogIn,SignUp}

    public GameObject StartUpMenu;
    public GameObject LogInMenu;
    public GameObject SignUpMenu;

    private MenuState menuState;

    void Start()
    {
        SetMenuState(MenuState.StartUp);
    }

    void Update()
    {
        
    }

    public void SetMenuState(MenuState state)
    {
        switch (state)
        {
            default:
            case MenuState.StartUp:
                StartUpMenu.SetActive(true);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(false);
                break;

            case MenuState.LogIn:
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(true);
                SignUpMenu.SetActive(false);
                break;

            case MenuState.SignUp:
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(true);
                break;
        }
    }

    public void SetMenuState(string state)
    {
        switch (state.ToLower())
        {
            default:
            case "startup":
                SetMenuState(MenuState.StartUp);
                break;

            case "login":
                SetMenuState(MenuState.LogIn);
                break;

            case "signup":
                SetMenuState(MenuState.SignUp);
                break;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
