using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{

    private float speed = 3f;
    public float boundryX;
    public float boundryZ;
    public Vector3 endposition;

    // Start is called before the first frame update
    void Start()
    {
        boundryX = Random.Range(-8f, 9f);
        boundryZ = Random.Range(-4f, 12f);
        endposition = new Vector3 (boundryX, 1, boundryZ);
         
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position != endposition)
        {
            transform.position = Vector3.MoveTowards(transform.position, endposition, speed * Time.deltaTime);
        }
        
    }
}
