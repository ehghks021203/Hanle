using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string name = "병사";
    public int health = 1;

    [SerializeField] GameObject deadEffect;
    [SerializeField] HP hp;

    public int DecreaseHealth() {
        health--;
        hp.DecreaseHealth();
        if (health <= 0) {
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            return -1;
        }
        return 0;
    }
}
