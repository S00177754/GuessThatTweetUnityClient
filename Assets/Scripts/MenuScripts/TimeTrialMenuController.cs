using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrialMenuController : MonoBehaviour
{
    public TMP_InputField TwitterUsernameInput;
    public TMP_Dropdown RoundSelectionDropdown;

    public void StartGame()
    {
        //Check if username is valid

        switch (RoundSelectionDropdown.value)
        {
            case 0: //5 Rounds
                break;

            case 1: //10 Rounds
                break;

            case 2: //15 Rounds
                break;
        }
    }
}
