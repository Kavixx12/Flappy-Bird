using UnityEngine;

public class PipeMiddleScript : MonoBehaviour


{

    public LogicScript logic;
    public AudioSource pipeAudio;
    public AudioClip scoreSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        pipeAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) 
        {


            logic.addScore(1);
            pipeAudio.PlayOneShot(scoreSound);
        }
       
    }
}
