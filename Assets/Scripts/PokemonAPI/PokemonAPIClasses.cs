using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokemonSpeciesDataObject
{
    [JsonProperty("base_happiness")]
    public int BaseHappines;

    [JsonProperty("capture_rate")]
    public int CaptureRate;

    [JsonProperty("color")]
    public PokemonColor Color;

    [JsonProperty("egg_groups")]
    public List<PokemonEggGroup> EggGroups;

    [JsonProperty("evolution_chain")]
    public PokemonEvolutionChain EvolutionChain;

    [JsonProperty("evolves_from_species")]
    public PokemonEvolutionChain EvolvesFrom;

    [JsonProperty("flavor_text_entries")]
    public List<PokemonFlavorText> FlavorTexts;

    [JsonProperty("forms_switchable")]
    public bool FormSwitchable;

    [JsonProperty("gender_rate")]
    public int GenderRate;

    [JsonProperty("generation")]
    public PokemonGeneration Generation;

    [JsonProperty("habitat")]
    public PokemonHabitat Habitat;

    [JsonProperty("has_gender_differences")]
    public bool HasGenderDifference;

    [JsonProperty("hatch_counter")]
    public int HatchCounter;

    [JsonProperty("id")]
    public int PokedexNumber;

    [JsonProperty("name")]
    public string Name;

    public PokemonFlavorText GetFlavorText(string language)
    {
        switch (language.ToLower())
        {
            case "english":
                language = "en";
                break;

            case "japanese":
                language = "ja";
                break;

            case "italian":
                language = "it";
                break;
        }

        return FlavorTexts.Where(f => f.Language.Name == language).SingleOrDefault();
    }
}

public class PokemonColor
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("url")]
    public string URL;
}

public class PokemonEggGroup
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("url")]
    public string URL;
}

public class PokemonEvolutionChain
{
    [JsonProperty("url")]
    public string URL;
}

public class PokemonEvolutionHistory
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("url")]
    public string URL;
}

public class PokemonFlavorText
{
    [JsonProperty("flavor_text")]
    public string FlavorText;

    [JsonProperty("language")]
    public PokemonLanguageGrouping Language;

    [JsonProperty("version")]
    public PokemonVersionGroup Version;
}

public class PokemonLanguageGrouping
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("url")]
    public string URL;
}

public class PokemonVersionGroup
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("url")]
    public string URL;
}

public class PokemonGeneration
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("url")]
    public string URL;
}

public class PokemonGrowthRate
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("url")]
    public string URL;
}

public class PokemonHabitat
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("url")]
    public string URL;
}

public class PokemonSpecies
{
    [JsonProperty("count")]
    public int Count;

    [JsonProperty("next")]
    public string NextResultsPage;

    [JsonProperty("previous")]
    public string PreviousResultsPage;

    [JsonProperty("results")]
    public List<PokemonSpeciesResults> Results;
}

public class PokemonSpeciesResults
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("url")]
    public string URL;
}