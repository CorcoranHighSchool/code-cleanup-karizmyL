
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;         //The force body on the GameObject
    private float minSpeed = 12.0f;     //Minimum upwards force
    private float maxSpeed = 16.0f;     //Maximum upwards force
    private float maxTorque = 10.0f;    //Range for turing
    private float xRange = 4.0f;        //Range for x position on spawn
    private float ySpawn = -6.0f;       //y Position on spawn
    private GameManager gameManager;
    public int pointValue = 5;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        //Capture the rigidbody
        targetRb = GetComponent<Rigidbody>();
        //Throw it up in the air
        Vector3 upwardsForce = RandomForce();
        targetRb.AddForce(upwardsForce, ForceMode.Impulse);
        //Random Rotation
        float xRot = RandomTorque();
        float yRot = RandomTorque();
        float zRot = RandomTorque();
        targetRb.AddTorque(xRot, yRot, zRot);
        //Spawn Position
        Vector3 spawnPosition = RandomSpawnPos();
        transform.position = spawnPosition;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawn, 0.0f);
    }
    
    //player clicks
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }
    //when the target falls into the sensor
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            GameObject.FindObjectOfType<GameManager>().GameOver();
        }
    }
}
