using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDTO
{
    public string Username;

    public List<GameRecordDTO> PastGames;

    public List<CustomGameDTO> CustomGames;
}

[Serializable]
public class GameRecordDTO
{
    public int ID;

    public string PlayerUsername;

    public string PokemonName;

    public float CompletionTime;

    public int Score;

    public DateTime Date;

    public string CustomGameCode;
}

[Serializable]
public class CustomGameDTO
{
    public float TimeLimit;

    public string PokemonURL1;

    public string PokemonURL2;

    public string PokemonURL3;

    public string PokemonURL4;

    public string PokemonURL5;

    public string CreatedBy;

    public Guid GameCode;
}

[Serializable]
public class UserDTO
{
    public string Email;

    public string PlayerUsername;

    public string Password;

    public string ConfirmPassword;
}

public class Token
{
    public string access_token { get; set; }

    public string token_type { get; set; }

    public int expires_in { get; set; }

    public string refresh_token { get; set; }
}


