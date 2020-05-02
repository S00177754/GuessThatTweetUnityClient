using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using UnityEngine;

public static class PokemonAPIHelper
{
    static public string BaseWebAddress = "https://pokeapi.co/api/v2/";
    static public int MaxRange = 807;

    static public PokemonSpeciesDataObject GetPokemon(string nameOrId)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(BaseWebAddress + "pokemon-species/" + nameOrId.ToLower()).Result;
            var resultContent = response.Content.ReadAsAsync<PokemonSpeciesDataObject>(new[] { new JsonMediaTypeFormatter() }).Result;

            return resultContent;
        }
    }

    static public void GetMaxRange()
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(BaseWebAddress + "pokemon-species/").Result;
            var resultContent = response.Content.ReadAsAsync<PokemonSpecies>(new[] { new JsonMediaTypeFormatter() }).Result;

            MaxRange = resultContent.Count;
        }
    }

    static public List<string> GetEggGroups()
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(BaseWebAddress + "egg-group/").Result;
            var resultContent = response.Content.ReadAsAsync<PokemonAttributeSearch<PokemonEggGroup>>(new[] { new JsonMediaTypeFormatter() }).Result;

            return resultContent.FormatEggResults();
        }
    }

    static public List<string> GetGenerations()
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(BaseWebAddress + "generation/").Result;
            var resultContent = response.Content.ReadAsAsync<PokemonAttributeSearch<PokemonGeneration>>(new[] { new JsonMediaTypeFormatter() }).Result;

            return resultContent.FormatGenerationResults();
        }
    }

}
