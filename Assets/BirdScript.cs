using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D MyRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public AudioSource birdAudio;
    public AudioClip gameOverSound;
    public AudioClip flapSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive == true) 
        {

            MyRigidbody.linearVelocity = Vector2.up * flapStrength;
            birdAudio.PlayOneShot(flapSound);
        }   
        
        if (transform.position.y > 10 || transform.position.y < -10)
        {
            if (birdIsAlive == true)
            {

                logic.gameOver();
                birdIsAlive = false;
                birdAudio.PlayOneShot(gameOverSound);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdIsAlive == true)
        {

            logic.gameOver();
            birdIsAlive = false;
            birdAudio.PlayOneShot(gameOverSound);
        }
    }
}
