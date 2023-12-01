using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int level = 1;
    public float currentXP = 0f;
    public float xpToNextLevel = 100f; 
    public UIManager uiManager;
    public float health = 100f;
    DiscreteMovement discreteMovement;
    PlayerInput playerInput;


    private void Start() {
        uiManager = FindObjectOfType<UIManager>();
        discreteMovement = GetComponent<DiscreteMovement>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void GainXP(float xpAmount)
    {
        currentXP += xpAmount;
        CheckForLevelUp();
        uiManager.UpdateExperienceBar(currentXP, xpToNextLevel);
    }

    void CheckForLevelUp()
    {
        if (currentXP >= xpToNextLevel)
        {
            level++;
            currentXP -= xpToNextLevel; 
            xpToNextLevel *= 1.5f; 
            LevelUpUIManager levelUpUIManager = FindObjectOfType<LevelUpUIManager>();
            levelUpUIManager.OpenLevelUpOptions();
        }
    }

    public void subtractHealth(float damage) {
        health = health - damage;
        uiManager.UpdateHP(health);
        if(health <= 0) {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void addHealth(float healing) {
        health = health + healing;
        uiManager.UpdateHP(health);
    }



    public void IncreaseAttackSpeed(float percent)
    {
        playerInput.changeCooldown(percent);
    }

    public void IncreaseSpeed(float percent)
    {
        discreteMovement.increaseSpeed(percent);
    }

    public void IncreaseHealth(float percent)
    {
        addHealth(percent);
    }
}
