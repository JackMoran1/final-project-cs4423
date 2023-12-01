using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 1f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void Start () {
        damage = damage * DifficultyManager.DamageFactor;
    }


        void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
                Player player = collision.gameObject.GetComponent<Player>();
                player.subtractHealth(damage);
                Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Boundary")) {
            Destroy(gameObject);
        }
    }

}