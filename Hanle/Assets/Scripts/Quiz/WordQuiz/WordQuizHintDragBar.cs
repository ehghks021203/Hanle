using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quiz.WordQuiz
{
    public class WordQuizHintDragBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {

        public bool isPointerDown;
        private RectTransform rectTransform;

        public RectTransform wordQuizTable;
        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
            rectTransform.DOKill();
            wordQuizTable.DOKill();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y > 585 ? 1000 : 170, 0.5f);
            //wordQuizTable.DOAnchorPosY(transform.position.y > 585 ? -75 : -200, 0.5f);
        }


        // Start is called before the first frame update
        void Awake()
        {
            rectTransform = transform.parent.GetComponent<RectTransform>();  
        }

        // Update is called once per frame
        void Update()
        {
            /*if(isPointerDown)
            {
                float value = Mathf.Clamp(Input.mousePosition.y + 40, 170, 1000);
                //wordQuizTable.anchoredPosition = new Vector2(0, (-600 + ((value - 170) * 12.5f / 83)));
                rectTransform.anchoredPosition = new Vector2(0, value);
                return;
            }*/
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition = new Vector2(0, Mathf.Clamp(rectTransform.anchoredPosition.y + (eventData.delta.y * (1920f / Screen.height)), 170, 1000));
        }
    }
}

