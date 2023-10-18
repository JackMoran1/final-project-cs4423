using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text HPText;
    public Text BossHPText;

    public void UpdateHP(float health) {
        HPText.text = "HP: " + health.ToString();
    }

    public void bossTakeDamage(float health) {
        BossHPText.text = "Boss Health: " + health.ToString();
    }

    public void ShowBossHP() {
        BossHPText.gameObject.SetActive(true);
    }

    
}
