using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public float moveSpeed;
    public float lifeTime;
    public GhostColor myColor;
    public Material[] mats;

    public void Init(GhostColor c)
    {
        myColor = c;
        GetComponent<Renderer>().material = mats[(int)c];
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    
}
