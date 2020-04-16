using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonAPITest : MonoBehaviour
{
    public void TestGet()
    {
        PokemonAPIHelper.GetPokemon("3");
    }
}
