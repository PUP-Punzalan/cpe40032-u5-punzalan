using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Component variables
    private Rigidbody targetRigidbody;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;

    // Basic data type variables
    private float minSpeed = 11;
    private float maxSpeed = 15;
    private float maxTorque = 10;
    private float xRange = 4;
    private float yRangePos = 2;
    public int pointValue;
    public int liveValue;

    // Start is called before the first frame update
    void Start()
    {
        // store object and other object's components
        targetRigidbody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // add force and torque to the objects when spawned
        targetRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        targetRigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        // after spawned, they will be spawned at a random x-axis location
        transform.position = RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This will run if the user clicked the objects
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            // destroy object when they got clicked
            Destroy(gameObject);

            // spawn particle in object position after they got deleted
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            // update values
            gameManager.UpdateScore(pointValue);
            gameManager.UpdateLive(liveValue);
            gameManager.CheckStatus();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // destroy object when they touched the sensor
        Destroy(gameObject);

        // update live
        if (gameObject.CompareTag("Good"))
        {
            gameManager.UpdateLive(-1);
        }
        
        // check status
        gameManager.CheckStatus();
    }

    // Returns random force (Vector3)
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // Returns random torque—rotation speed (float)
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // Returns random position (Vector3)
    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), -yRangePos);
    }
}
