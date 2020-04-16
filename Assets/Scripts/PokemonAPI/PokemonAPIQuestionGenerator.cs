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

    public PokemonAPIQuestionGenerator()
    {
        EggGroups = PokemonAPIHelper.GetEggGroups();
        Generations = PokemonAPIHelper.GetGenerations();
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


