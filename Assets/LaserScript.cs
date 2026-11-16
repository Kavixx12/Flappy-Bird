using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float speed = 15f; 

    void Update()
    {
        
        transform.position += Vector3.right * speed * Time.deltaTime;

        
        if (transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Pipe") || other.gameObject.CompareTag("Pipe"))
        {
            
            Destroy(other.gameObject);

            
            Destroy(gameObject);
        }
    }
}