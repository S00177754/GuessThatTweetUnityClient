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

    static public string FormatName(string eggGroup)
    {
        string eggGroupName;

        switch (eggGroup)
        {
            case "water1":
                eggGroupName = "Water 1";
                break;

            case "humanshape":
                eggGroupName = "Humanoid";
                break;

            case "water2":
                eggGroupName = "Water 2";
                break;

            case "water3":
                eggGroupName = "Water 3";
                break;

            case "no-eggs":
                eggGroupName = "No Egg Group";
                break;

            default:
                eggGroupName = eggGroup;
                break;
        }

        eggGroupName = char.ToUpper(eggGroupName[0]) + eggGroupName.Substring(1);

        return eggGroupName;
    }
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

    static public string FormatName(string generationName)
    {
        string formattedName = "Undefined";
        switch (generationName)
        {
            case "generation-i":
                formattedName = "Generation I";
                break;

            case "generation-ii":
                formattedName = "Generation II";
                break;

            case "generation-iii":
                formattedName = "Generation III";
                break;

            case "generation-iv":
                formattedName = "Generation IV";
                break;

            case "generation-v":
                formattedName = "Generation V";
                break;

            case "generation-vi":
                formattedName = "Generation VI";
                break;

            case "generation-vii":
                formattedName = "Generation VII";
                break;

            default:
                break;
        }

        return formattedName;
    }
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

public class PokemonAttributeSearch<T>
{
    [JsonProperty("count")]
    public int Count;

    [JsonProperty("next")]
    public string NextPageURL;

    [JsonProperty("previous")]
    public string PrevPageURL;

    [JsonProperty("results")]
    public List<T> Results;

    public List<string> FormatEggResults()
    {
        List<string> info = (Results as List<PokemonEggGroup>).Select(e => e.Name).ToList();

        for(int i = 0; i < info.Count; i++)
        {
            info[i] = PokemonEggGroup.FormatName(info[i]);
        }

        return info;
    }

    public List<string> FormatGenerationResults()
    {
        List<string> info = (Results as List<PokemonGeneration>).Select(e => e.Name).ToList();

        for (int i = 0; i < info.Count; i++)
        {
            info[i] = PokemonGeneration.FormatName(info[i]);
        }

        return info;
    }
}