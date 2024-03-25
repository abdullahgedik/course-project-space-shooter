using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float powerupFallSpeed;

    [SerializeField]
    private int powerupID;


    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(new Vector3(0, -powerupFallSpeed, 0) * Time.deltaTime);

        PowerupEnds();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;

                    default:
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }

    void PowerupEnds()
    {
        if (transform.position.y <= -6.6)
        {
            Destroy(gameObject);
        }
    }
}
