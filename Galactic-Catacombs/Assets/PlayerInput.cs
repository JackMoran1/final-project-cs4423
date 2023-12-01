using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{

    private ICommand moveUpCommand;
    private ICommand moveLeftCommand;
    private ICommand moveDownCommand;
    private ICommand moveRightCommand;
    private ICommand shootCommand;

    DiscreteMovement discreteMovement;
    ProjectileThrower projectileThrower;
    public float cooldown = 0.5f;
    public float shotTime = 0f;
    public float health = 100f;
    public UIManager uiManager;
    private Player player;
    private bool isFrozen;
    private KeyCode forwardKey;
    private KeyCode leftKey;
    private KeyCode backwardKey;
    private KeyCode rightKey;
    private KeyCode shootKey;


    void Awake() {
        LoadKeyBindings();
        discreteMovement = GetComponent<DiscreteMovement>();
        projectileThrower = GetComponent<ProjectileThrower>();

        moveUpCommand = new MoveCommand(this, Vector3.up);
        moveLeftCommand = new MoveCommand(this, Vector3.left);
        moveDownCommand = new MoveCommand(this, Vector3.down);
        moveRightCommand = new MoveCommand(this, Vector3.right);
        shootCommand = new ShootCommand(this);
    }


    void FixedUpdate()
    {
        if(Input.GetKey(forwardKey))
        {
            moveUpCommand.Execute();
        }
        if(Input.GetKey(leftKey))
        {
            moveLeftCommand.Execute();
        }
        if(Input.GetKey(backwardKey))
        {
            moveDownCommand.Execute();
        }
        if(Input.GetKey(rightKey))
        {
            moveRightCommand.Execute();
        }
    }


    public void Move(Vector3 direction)
    {
        discreteMovement.MoveTransform(direction);
    }

    public void changeCooldown(float newCooldown) {
        cooldown = cooldown - (cooldown * newCooldown);
    }

    public void Shoot()
    {
        if (Time.time > shotTime)
        {
            projectileThrower.Throw();
            shotTime = Time.time + cooldown;
            GetComponent<AudioSource>().Play();
        }
    }

    public void Pause() {
        if(!isFrozen) {
            isFrozen = true;
            Time.timeScale = 0;
            uiManager = FindObjectOfType<UIManager>();
            uiManager.ShowPauseMenu();
        } else {
            isFrozen = false;
            Time.timeScale = 1;
            uiManager = FindObjectOfType<UIManager>();
            uiManager.HidePauseMenu();
        }
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(shootKey)) {
            Shoot();
        } 
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
    }


    public void LoadKeyBindings()
    {
        forwardKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", KeyCode.W.ToString()));
        leftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", KeyCode.A.ToString()));
        backwardKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", KeyCode.S.ToString()));
        rightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", KeyCode.D.ToString()));
        shootKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire", KeyCode.Space.ToString()));

    }




    





}