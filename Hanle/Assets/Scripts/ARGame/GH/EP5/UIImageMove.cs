using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EP5
{
    public class UIImageMove : MonoBehaviour
    {
        //public Vector2 endSize = new Vector2(100, 100);
        // Start is called before the first frame update
        public void SetData(int type)
        {
            (transform as RectTransform).DOAnchorPos(transform.parent.Find("CollectView").localPosition, .5f).OnComplete(delegate
            {
                GetComponent<Image>().DOFade(0, .5f).OnComplete(delegate
                {
                    GameObject.Find("SliceManager").GetComponent<SliceManager>().ClearPart(type);
                    Destroy(gameObject);
                    //인벤토리에 들어갔음
                });
            });
        }


    }

}
