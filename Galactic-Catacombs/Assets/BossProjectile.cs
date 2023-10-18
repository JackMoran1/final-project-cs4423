using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }


        void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
                PlayerInput player = collision.gameObject.GetComponent<PlayerInput>();
                player.damageTaken(1f);
                Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Boundary")) {
            Destroy(gameObject);
        }
    }

}