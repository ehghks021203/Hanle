using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizGuide : MonoBehaviour
{
    
    public void Start(){
        if(GameObject.FindGameObjectsWithTag("QuizGuide").Length > 1) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
