using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscreteMovement : MonoBehaviour
{
    [SerializeField] float speed = 1;   
    Rigidbody2D rb;
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveTransform(Vector3 vel){
        
        transform.position += vel * speed * Time.deltaTime;
    }


    public void MoveRB(Vector3 vel) {
        rb.velocity = vel;
    }

    public void increaseSpeed(float updatedSpeed) {
        speed = (speed * updatedSpeed) + speed;
    }
}
