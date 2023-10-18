using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{

    DiscreteMovement discreteMovement;
    ProjectileThrower projectileThrower;
    public float cooldown = 0.5f;
    public float shotTime = 0f;
    public float health = 100f;
    public UIManager uiManager;
    void Awake() {
        discreteMovement = GetComponent<DiscreteMovement>();
        projectileThrower = GetComponent<ProjectileThrower>();
    }


    void FixedUpdate(){
        Vector3 vel = Vector3.zero;
        if(Input.GetKey(KeyCode.W)){
            vel.y = 1;
        }if(Input.GetKey(KeyCode.A)){
            vel.x = -1;
        }if(Input.GetKey(KeyCode.S)){
            vel.y = -1;
        }if(Input.GetKey(KeyCode.D)){
            vel.x = 1;
        }

        discreteMovement.MoveTransform(vel);


        
    }




    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > shotTime) {
            projectileThrower.Throw();
            shotTime = Time.time + cooldown;
            GetComponent<AudioSource>().Play();
        }  
    }

    public void damageTaken(float damage) {
        health = health - damage;
        uiManager.UpdateHP(health);
        if(health <= 0) {
            SceneManager.LoadScene("MainMenu");
        }
    }




}