using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text HPText;
    public Text BossHPText;
    public Slider xpSlider;
    public Text Paused;

    public void UpdateHP(float health) {
        HPText.text = "HP: " + health.ToString();
    }

    public void bossTakeDamage(float health) {
        BossHPText.text = "Boss Health: " + health.ToString();
    }

    public void ShowBossHP() {
        BossHPText.gameObject.SetActive(true);
    }

    public void ShowPauseMenu() {
        Paused.gameObject.SetActive(true);
    }

    public void HidePauseMenu() {
        Paused.gameObject.SetActive(false);
    }


    public void UpdateExperienceBar(float currentXp, float xpNeededForLevelUp) {
        xpSlider.maxValue = xpNeededForLevelUp;
        xpSlider.value = currentXp;
    }
    
}
