using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private float boundry = 22;
    public GameObject rotationObject;
    private SpawnManager spawnManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        rotationObject = GameObject.FindGameObjectWithTag("Player");
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        transform.LookAt(rotationObject.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
        if (transform.position.z > boundry || transform.position.x > boundry || transform.position.z < -boundry || transform.position.x < -boundry)
        {
            spawnManagerScript.score += 1;
            Destroy(gameObject);
        }    

        if (spawnManagerScript.timeToWait == 1)
        {
            speed = 3;
        }

    }
}
