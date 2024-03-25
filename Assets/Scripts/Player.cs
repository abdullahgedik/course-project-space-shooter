using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private float speedMultiplier = 2f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleLaserPrefab;
    [SerializeField]
    private GameObject shieldPrefab;
    [SerializeField]
    private float fireRate = .15f;
    [SerializeField]
    private float Lives = 3f;


    [SerializeField]
    private bool isTripleShotActive = false;
    [SerializeField]
    private bool isSpeedBoostActive = false;
    [SerializeField]
    private bool isShieldActive = false;

    private SpawnManager spawnManager;

    private float canFire = -1;

    void Start()
    {
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL!");
        }
    }

    void Update()
    {
        Movement();

        if (Input.GetKey(KeyCode.Space) && Time.time > canFire)
        {
            FireLaser();
        }
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * Time.deltaTime * speed);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-4.8f,0), 0);

        if (transform.position.x >= 11.25)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.25)
        {
            transform.position = new Vector3(11.25f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        canFire = Time.time + fireRate;

        if (isTripleShotActive)
        {
            Instantiate(tripleLaserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        Lives--;

        if(Lives < 1)
        {
            spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ShieldActive()
    {
        isShieldActive = true;
        GameObject shield =  Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        shield.transform.parent = transform;
        StartCoroutine(ShieldPowerDownRoutine(shield));
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        while (isTripleShotActive)
        {
            yield return new WaitForSeconds(4f);
            isTripleShotActive = false;
        }
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        while (isSpeedBoostActive)
        {
            yield return new WaitForSeconds(4f);
            speed /= speedMultiplier;
            isSpeedBoostActive = false;
        }
    }

    public IEnumerator ShieldPowerDownRoutine(GameObject shield)
    {
        while(isShieldActive)
        {
            yield return new WaitForSeconds(8f);

            if(shield != null)
            {
                Destroy(shield.gameObject);
            }
            isShieldActive = false;
        }
    }
}
