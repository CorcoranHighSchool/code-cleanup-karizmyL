
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = player.transform.position - transform.position;
        lookDirection.Normalize();
        enemyRb.AddForce(lookDirection * speed);
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        
    }
}
