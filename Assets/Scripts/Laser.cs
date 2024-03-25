using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float laserSpeed = 10;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(new Vector3(0, laserSpeed, 0) * Time.deltaTime);

        LaserEnds();
    }

    void LaserEnds()
    {
        if (transform.position.y >= 6)
        {
            Destroy(gameObject);
        }
    }
}
