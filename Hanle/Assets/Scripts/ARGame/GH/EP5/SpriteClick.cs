using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

namespace EP5
{
    public class SpriteClick : MonoBehaviour
    {

        public GameObject uiImagePrefab;
        private Transform rootCanvas;

        public int type;

        private void Awake()
        {
            rootCanvas = GameObject.Find("Canvas").transform;
        }

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
        public void Catched(Vector2 vector2)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            GameObject obj = Instantiate(uiImagePrefab, vector2, Quaternion.Euler(0, 0, 0), rootCanvas);
            obj.GetComponent<UIImageMove>().SetData(type);
            obj.GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
            //distance ���� ũ�� �����ϰ� �Ϸ������� �ð��� �н�
            GetComponent<SpriteRenderer>().DOFade(0, .5f).OnComplete(delegate
            {
                Destroy(gameObject);
            });
        }
    }
}

