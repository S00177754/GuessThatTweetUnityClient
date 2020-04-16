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

    public PokemonQuestion()
    {
    }

    public PokemonQuestion(PokemonSpeciesDataObject pokemonObj, QuestionType type)
    {
        QuestionType = type;
        QuestionSetup(pokemonObj, type);
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
                Choices = GetRandomChoices(Answer, PokemonAPIQuestionGenerator.EggGroups);
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
                Question = "Fill in the blanks.";
                Answer = pokeObj.FlavorTexts.Where(ft => ft.Language.Name == "en").Select(ft => ft.FlavorText).Single();
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

}
