using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.WordQuiz {
    public class WordQuizTable : MonoBehaviour
    {
        public string[] answers = new string[25];

        public string[] horizontalsHint;
        public string[] verticalsHint;

        private WordQuizTile[] tiles = new WordQuizTile[25];
        public WordQuizHint hint;

        //yh
        public GameObject Button;

        void Start()
        {
            for(int i = 0; i < 25; i++)
            {
                tiles[i] = transform.GetChild(i).GetComponent<WordQuizTile>();
                tiles[i].SetAnswer(answers[i], EndEdit);
            }

            hint.SetHint(horizontalsHint, verticalsHint);
        }

        void EndEdit()
        {
            for (int i = 0; i < 25; i++)
            {
                if (!tiles[i].isCorrectAnswer) return;
            }

            Debug.Log("모든 답안이 정답입니다!");
            
            //yh
            Button.SetActive(true);
        }
    }
}

