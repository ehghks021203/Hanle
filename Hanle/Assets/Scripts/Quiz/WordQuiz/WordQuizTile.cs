using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace Quiz.WordQuiz
{
    public class WordQuizTile : MonoBehaviour
    {

        private InputField inputField;
        private ProceduralImage image;

        public bool isCorrectAnswer = false;
        private string answer;
        private Action endEdit;

        public Sprite closed;

        void OnEnable()
        {
            inputField = GetComponent<InputField>();
            image = GetComponent<ProceduralImage>();
        }


        void SetDisable()
        {
            if(inputField == null) inputField = GetComponent<InputField>();
            if(image == null) image = GetComponent<ProceduralImage>();

            inputField.interactable = false;
            image.sprite = closed;
            isCorrectAnswer = true;
        }

        public void SetAnswer(string answer, Action endEdit)
        {
            if(string.IsNullOrEmpty(answer))
            {
                SetDisable();
                return;
            }

            this.answer = answer;
            this.endEdit = endEdit;
        }

        public void OnEndEdit()
        {
            isCorrectAnswer = inputField.text == answer;
            endEdit?.Invoke();
        }
    }
}