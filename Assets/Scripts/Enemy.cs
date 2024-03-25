using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        float randomX = Random.Range(-9.5f,9.5f);

        if(transform.position.y < -6.4f)
        {
            transform.position = new Vector3(randomX, 6.4f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }

            Destroy(gameObject);
        }

        if(other.transform.tag == "Laser" || other.transform.tag == "Shield")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
