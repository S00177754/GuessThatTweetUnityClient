using Microsoft.SqlServer.Server;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestionType { EggGroup, Generation, PokedexNumber, Name, FlavorText }

public class PokemonQuestion
{
    public QuestionType QuestionType;
    public string Question;
    public string Answer;
    public List<string> Choices;
    public bool IsMultipleChoice;
    public int Score;
    public string PokemonName;

    public PokemonQuestion(PokemonSpeciesDataObject pokemonObj, QuestionType type)
    {
        QuestionType = type;
        QuestionSetup(pokemonObj, type);
        PokemonName = pokemonObj.Name;
    }

    public void QuestionSetup(PokemonSpeciesDataObject pokeObj, QuestionType type)
    {
        switch (type)
        {
            case QuestionType.EggGroup:
                Question = "What egg group does " + pokeObj.Name + " belong to?";
                Answer = PokemonEggGroup.FormatName(pokeObj.EggGroups[0].Name);
                Choices = GetRandomChoices(Answer, PokemonAPIQuestionGenerator.EggGroups);
                IsMultipleChoice = true;
                break;

            case QuestionType.Generation:
                Question = "What generation of games is " + pokeObj.Name + " from?";
                Answer = PokemonGeneration.FormatName(pokeObj.Generation.Name);
                Choices = GetRandomChoices(Answer, PokemonAPIQuestionGenerator.Generations);
                IsMultipleChoice = true;
                break;

            case QuestionType.PokedexNumber:
                Question = "What number of the national pokedex is " + pokeObj.Name + "?";
                Answer = pokeObj.PokedexNumber.ToString();
                IsMultipleChoice = false;
                break;

            case QuestionType.Name:
                Question = "Who is that Pokemon?";
                Answer = pokeObj.Name;
                Answer = char.ToUpper(Answer[0]) + Answer.Substring(1);
                IsMultipleChoice = false;
                break;

            case QuestionType.FlavorText:
                string[] fixedUp = RemoveWord(pokeObj.FlavorTexts.Where(ft => ft.Language.Name == "en").Take(1).Select(ft => ft.FlavorText).SingleOrDefault());
                Answer = fixedUp[0];
                Question = "Fill in the blanks.\n" + fixedUp[1];
                IsMultipleChoice = false;
                break;
        }
    }

    public List<string> GetRandomChoices(string answer, List<string> list)
    {
        List<string> choices = list.Where(eg => eg != answer).OrderBy(x => Random.Range(0, x.Length)).Take(3).ToList();
        choices.Add(Answer);
        return choices;
    }

    public void CalculateScore(float timeElapsed)
    {
        Score = (1000 - (int)timeElapsed);
    }

    public string FormatFlavorText(string text)
    {
        text.Replace("\n", " ");
        return text;
    }

    public string[] RemoveWord(string flavorText)
    {
        flavorText = FormatFlavorText(flavorText);
        string[] words = flavorText.Split(' ');
        int num = Random.Range(0, words.Length);

        string answer = words[num];
        words[num] = " ______ ";

        string newQuestion = "";
        foreach (var wrd in words)
        {
            newQuestion += (wrd + " ");
        }

        return new string[]{ answer,newQuestion};
    }
}
