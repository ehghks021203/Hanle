using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.WordQuiz
{
    public class WordQuizHint : MonoBehaviour
    {

        public Text text;
        public RectTransform verticalLayoutGroup;

        public void SetHint(string[] horizontal, string[] vertical)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("<size=60>가로</size>\r\n");
            for(int i = 0; i < horizontal.Length; i++)
            {
                stringBuilder.Append((i + 1) + ". " + horizontal[i] + "\r\n");
            }

            stringBuilder.Append("\r\n");
            stringBuilder.Append("<size=60>세로</size>\r\n");
            for (int i = 0; i < vertical.Length; i++)
            {
                stringBuilder.Append((i + 1) + ". " + vertical[i] + "\r\n");
            }

            text.text = stringBuilder.ToString();

            LayoutRebuilder.ForceRebuildLayoutImmediate(verticalLayoutGroup);
        }
    }

}
