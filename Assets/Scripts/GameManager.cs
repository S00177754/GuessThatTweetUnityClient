﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool LoggedIn = false;
    public string Username = "";

    public void SetUserDetails(bool loggedIn, string username)
    {
        LoggedIn = loggedIn;
        Username = username;
    }
}