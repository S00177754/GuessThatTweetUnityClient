﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public enum MenuState { Disable,StartUp,LogIn,SignUp,Game,TimeTrial,Random}

    public GameObject TitleText;
    public GameObject StartUpMenu;
    public GameObject LogInMenu;
    public GameObject SignUpMenu;
    public GameObject GameMenu;
    public GameObject TimeTrialMenu;
    public QuestionsController QC;

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
                TitleText.SetActive(true);
                StartUpMenu.SetActive(true);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(false);
                GameMenu.SetActive(false);
                TimeTrialMenu.SetActive(false);
                QC.DeActivate();
                break;

            case MenuState.LogIn:
                TitleText.SetActive(true);
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(true);
                SignUpMenu.SetActive(false);
                GameMenu.SetActive(false);
                TimeTrialMenu.SetActive(false);
                QC.DeActivate();
                break;

            case MenuState.SignUp:
                TitleText.SetActive(true);
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(true);
                GameMenu.SetActive(false);
                TimeTrialMenu.SetActive(false);
                QC.DeActivate();
                break;

            case MenuState.Game:
                TitleText.SetActive(true);
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(false);
                GameMenu.SetActive(true);
                TimeTrialMenu.SetActive(false);
                QC.DeActivate();
                break;

            case MenuState.TimeTrial:
                TitleText.SetActive(true);
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(false);
                GameMenu.SetActive(false);
                TimeTrialMenu.SetActive(true);
                TimeTrialMenu.GetComponent<TimeTrialMenuController>().RandomGame = false;
                TimeTrialMenu.GetComponent<TimeTrialMenuController>().PokemonNameInput.gameObject.SetActive(true);
                QC.DeActivate(); 
                break;

            case MenuState.Random:
                TitleText.SetActive(true);
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(false);
                GameMenu.SetActive(false);
                TimeTrialMenu.SetActive(true);
                TimeTrialMenu.GetComponent<TimeTrialMenuController>().RandomGame = true;
                TimeTrialMenu.GetComponent<TimeTrialMenuController>().PokemonNameInput.gameObject.SetActive(false);
                QC.DeActivate(); 
                break;

            case MenuState.Disable:
                TitleText.SetActive(false);
                StartUpMenu.SetActive(false);
                LogInMenu.SetActive(false);
                SignUpMenu.SetActive(false);
                GameMenu.SetActive(false);
                TimeTrialMenu.SetActive(false);
                QC.DeActivate();
                break;
        }
        this.gameObject.SetActive(true);
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
                SetMenuState(MenuState.Game);
                break;

            case "timetrial":
                SetMenuState(MenuState.TimeTrial);
                break;

            case "random":
                SetMenuState(MenuState.Random);
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
