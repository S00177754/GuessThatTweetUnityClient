using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokemonAPIQuestionGenerator : MonoBehaviour
{
    public Queue<PokemonQuestion> Questions;
    static public List<string> EggGroups;
    static public List<string> Generations;


    private void Start()
    {
        EggGroups = PokemonAPIHelper.GetEggGroups();
        Generations = PokemonAPIHelper.GetGenerations();
    }

    public void GeneratePokemonQuestions(string pokemonName)
    {
        ResetQuestions();

        PokemonSpeciesDataObject pokeDTO = PokemonAPIHelper.GetPokemon(pokemonName);
        if(pokeDTO != null)
        {
            QueueQuestion(pokeDTO, QuestionType.Name);
            QueueQuestion(pokeDTO, QuestionType.EggGroup);
            QueueQuestion(pokeDTO, QuestionType.FlavorText);
            QueueQuestion(pokeDTO, QuestionType.Generation);
            QueueQuestion(pokeDTO, QuestionType.PokedexNumber);
            GetComponent<QuestionsController>().SetSprite(pokeDTO.PokedexNumber);
            GetComponent<QuestionsController>().QuestionCount = 5;
        }
    }

    public PokemonQuestion GetNextQuestion()
    {
        PokemonQuestion question = Questions.Dequeue();
        Questions.Enqueue(question);
        return question;

    }

    public void QueueQuestion(PokemonSpeciesDataObject dataObj, QuestionType type)
    {
        Questions.Enqueue(new PokemonQuestion(dataObj, type));
    }

    public void ResetQuestions()
    {
        Questions.Clear();
    }
    
}


