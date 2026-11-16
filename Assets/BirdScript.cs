using UnityEngine;
using System.Collections;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D MyRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public AudioSource birdAudio;
    public AudioClip gameOverSound;
    public AudioClip flapSound;

   
    public AudioClip shrinkSound;
    public AudioClip ghostSound;
    public AudioClip laserSound;

    
    public Sprite normalSprite;
    public Sprite ghostSprite;
    public Sprite gunnerSprite;

    public GameObject laserPrefab;

    private Vector3 originalScale;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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

    void ShootLaser()
    {
       
        Vector3 spawnPosition = transform.position + new Vector3(0.5f, 0f, 0f);
        Instantiate(laserPrefab, spawnPosition, Quaternion.identity);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PowerUp") && birdIsAlive == true)
        {
            if (shrinkSound != null) birdAudio.PlayOneShot(shrinkSound);
            Destroy(other.gameObject);
            StartCoroutine(ShrinkRoutine());
        }
        else if (other.gameObject.CompareTag("GhostPower") && birdIsAlive == true)
        {
            if (ghostSound != null) birdAudio.PlayOneShot(ghostSound);
            Destroy(other.gameObject);
            StartCoroutine(GhostRoutine());
        }
        else if (other.gameObject.CompareTag("BreakerPower") && birdIsAlive == true)
        {
            Destroy(other.gameObject);
            StartCoroutine(BreakerRoutine());
        }
    }

    private IEnumerator ShrinkRoutine()
    {
        transform.localScale = originalScale * 0.5f;
        yield return new WaitForSeconds(5f);
        transform.localScale = originalScale;
    }

    private IEnumerator GhostRoutine()
    {
        int pipeLayer = LayerMask.NameToLayer("Pipe");
        Physics2D.IgnoreLayerCollision(gameObject.layer, pipeLayer, true);

        if (spriteRenderer != null && ghostSprite != null)
        {
            spriteRenderer.sprite = ghostSprite;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.7f);
        }

        yield return new WaitForSeconds(5f);

        Physics2D.IgnoreLayerCollision(gameObject.layer, pipeLayer, false);

        if (spriteRenderer != null && normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    
    private IEnumerator BreakerRoutine()
    {
        
        if (spriteRenderer != null && gunnerSprite != null)
        {
            spriteRenderer.sprite = gunnerSprite;
        }

        
        if (laserSound != null) birdAudio.PlayOneShot(laserSound);

       
        yield return new WaitForSeconds(2f);

       
        Coroutine machineGun = StartCoroutine(MachineGunRoutine());

        
        yield return new WaitForSeconds(3f);

       
        StopCoroutine(machineGun);

        
        if (spriteRenderer != null && normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    private IEnumerator MachineGunRoutine()
    {
        while (true)
        {
            ShootLaser();
           
            yield return new WaitForSeconds(0.2f);
        }
    }
}