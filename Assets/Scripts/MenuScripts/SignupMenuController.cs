using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static MainMenuController;

public class SignupMenuController : MonoBehaviour
{
    public MainMenuController MenusController;

    public TMP_InputField EmailField;
    public TMP_InputField UsernameField;
    public TMP_InputField PasswordField;
    public TMP_Text ErrorMessage;

    public void SignUp()
    {
        UserDTO user = new UserDTO()
        {
            Email = EmailField.text,
            PlayerUsername = UsernameField.text,
            Password = PasswordField.text,
            ConfirmPassword = PasswordField.text
        };

        if (ASPNetAPIHelper.RegisterUser(user))
        {
            ErrorMessage.text = "Success Register";
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SetUserDetails(true, UsernameField.text);
            MenusController.SetMenuState(MenuState.Game);
            ASPNetAPIHelper.Login(user.Email, user.Password);
        }
        else
        {
            ErrorMessage.text = ASPNetAPIHelper.ErrorMessage;
        }
    }
}
