using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public int hp;

    void Start() {
        hp = transform.GetComponentInParent<Enemy>().health;
    }


    public void DecreaseHealth() {
        transform.GetChild(hp).GetComponent<SpriteRenderer>().color = Color.gray;
        hp = transform.GetComponentInParent<Enemy>().health;
    }
}