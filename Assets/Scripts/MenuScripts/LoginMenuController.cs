using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static MainMenuController;

public class LoginMenuController : MonoBehaviour
{
    public MainMenuController MenusController;

    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_Text errorMessage;

    public void Login()
    {
        if (ASPNetAPIHelper.Login(usernameField.text, passwordField.text))
        {
            errorMessage.text = "";
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SetUserDetails(true, usernameField.text);
            MenusController.SetMenuState(MenuState.Game);
        }
        else
        {
            errorMessage.text = ASPNetAPIHelper.ErrorMessage;
        }
    }
}
