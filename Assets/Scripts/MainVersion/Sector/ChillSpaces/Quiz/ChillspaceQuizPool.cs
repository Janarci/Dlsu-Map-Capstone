using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillspaceQuizPool : MonoBehaviour
{
    [Serializable]
    public class Quizzes
    {
        [Serializable]
        public class Quiz
        {
            [TextArea] public string quiz;
            public string answer;
        }

        public ChillSpace.Area area;
        public Quiz[] quizzes;
    }

    public List<Quizzes> quizPool;

    Dictionary<ChillSpace.Area, Quizzes> quizMap;


    private void Start()
    {
        quizMap = new Dictionary<ChillSpace.Area, Quizzes>();
        foreach(Quizzes _q in quizPool)
        {
            if(!quizMap.ContainsKey(_q.area))
            {
                quizMap[_q.area] = _q;
            }
        }
    }

    public Quizzes.Quiz GetQuestion(ChillSpace.Area _area)
    {
        int rndIndex = -1;

        if(quizMap[_area].quizzes.Length != 0)
        {
            rndIndex = UnityEngine.Random.Range(0, quizMap[_area].quizzes.Length);
        }

        if (rndIndex != -1)
        {
            return quizMap[_area].quizzes[rndIndex];
        }

        else
            return null;
    }

}
