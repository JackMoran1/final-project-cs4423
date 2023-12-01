using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DifficultyManager : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }
    public static Difficulty CurrentDifficulty { get; private set; }

   
    public static float DamageFactor { get; private set; }
    public static float HealthFactor { get; private set; }


    public void SetEasyDifficulty() => SetDifficulty(Difficulty.Easy);
    public void SetMediumDifficulty() => SetDifficulty(Difficulty.Medium);
    public void SetHardDifficulty() => SetDifficulty(Difficulty.Hard);

    private void SetDifficulty(Difficulty difficulty)
    {
        CurrentDifficulty = difficulty;
        switch (difficulty)
        {
            case Difficulty.Easy:
                DamageFactor = 1f;
                HealthFactor = 1f;
                break;
            case Difficulty.Medium:
                DamageFactor = 2f; 
                HealthFactor = 2f; 
                break;
            case Difficulty.Hard:
                DamageFactor = 3f; 
                HealthFactor = 3f; 
                break;
        }


        SceneManager.LoadScene("Gameplay"); 
    }


}
