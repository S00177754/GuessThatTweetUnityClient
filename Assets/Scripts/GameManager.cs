using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, Paused, Playing}

public class GameManager : MonoBehaviour
{
    static public bool LoggedIn = false;
    public string Username = "";

    static public GameState GameState;
    public GameObject MenuGroup;
    public GameObject GamePlayGroup;


    private void Start()
    {
        GameState = GameState.MainMenu;
    }

    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                MenuGroup.SetActive(true);
                GamePlayGroup.SetActive(true);
                break;

            case GameState.Playing:
                MenuGroup.SetActive(true);
                GamePlayGroup.SetActive(true);
                break;

            default:
                break;
        }
    }

    public void SetGameState(string state)
    {
        switch (state)
        {
            case "mainmenu":
                SetGameState(GameState.MainMenu);
                break;

            case "playing":
                SetGameState(GameState.Playing);
                break;
        }
    }

    public void SetUserDetails(bool loggedIn, string username)
    {
        LoggedIn = loggedIn;
        Username = username;
    }

    static public bool CheckLoggedIn()
    {
        return LoggedIn;
    }
}
