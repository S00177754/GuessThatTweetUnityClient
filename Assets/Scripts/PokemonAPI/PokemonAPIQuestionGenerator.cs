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
    public QuestionsController QC;


    private void Start()
    {
        EggGroups = PokemonAPIHelper.GetEggGroups();
        Generations = PokemonAPIHelper.GetGenerations();
        Questions = new Queue<PokemonQuestion>();
    }

    public void GeneratePokemonQuestions(string pokemonName)
    {
        PokemonSpeciesDataObject pokeDTO = PokemonAPIHelper.GetPokemon(pokemonName);
        if(pokeDTO != null)
        {
            QueueQuestion(pokeDTO, QuestionType.Name);
            QueueQuestion(pokeDTO, QuestionType.EggGroup);
            QueueQuestion(pokeDTO, QuestionType.FlavorText);
            QueueQuestion(pokeDTO, QuestionType.Generation);
            QueueQuestion(pokeDTO, QuestionType.PokedexNumber);
            GetComponent<QuestionsController>().SetSprite(pokeDTO.PokedexNumber);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SetGameState(GameState.Playing);
            GetComponent<QuestionsController>().NextQuestion();
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

    
}


