using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public enum MenuState { Disable,StartUp,LogIn,SignUp,Game}

    public GameObject StartUpMenu;
    public GameObject LogInMenu;
    public GameObject SignUpMenu;
    public GameObject GameMenu;

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
                GameMenu.SetActive(false);
                break;

            case MenuState.LogIn:
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(true);
                SignUpMenu.SetActive(false);
                GameMenu.SetActive(false);
                break;

            case MenuState.SignUp:
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(true);
                GameMenu.SetActive(false);
                break;

            case MenuState.Game:
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(false);
                GameMenu.SetActive(true);
                break;

            case MenuState.Disable:
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(false);
                GameMenu.SetActive(false);
                GameMenu.SetActive(false);
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

            case "game":
                SetMenuState(MenuState.SignUp);
                break;

            case "disable":
                SetMenuState(MenuState.Disable);
                break;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Login()
    {

    }
}
