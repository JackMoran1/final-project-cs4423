using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUIManager : MonoBehaviour
{
    public GameObject levelUpMenu;
    public Button option1;
    public Button option2;
    public Button option3;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text; 
    public TextMeshProUGUI option3Text; 

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        levelUpMenu.SetActive(false);
    }

    public void OpenLevelUpOptions()
    {
        BuffOption[] options = GetBuffOptions(); 

        UpdateButton(option1, option1Text, options[0]);
        UpdateButton(option2, option2Text, options[1]);
        UpdateButton(option3, option3Text, options[2]);

        levelUpMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void UpdateButton(Button button, TextMeshProUGUI buttonText, BuffOption buffOption)
    {
        buttonText.text = buffOption.description;
        button.onClick.RemoveAllListeners(); 
        button.onClick.AddListener(() => ApplyBuffAndClose(buffOption.applyBuff));
    }

    private void ApplyBuffAndClose(Action<Player> buffAction)
    {
        buffAction(player);

        levelUpMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private BuffOption[] GetBuffOptions()
    {
        return new BuffOption[]
        {
            new BuffOption("5% Faster Attack Speed", p => p.IncreaseAttackSpeed(0.05f)),
            new BuffOption("+10% Movement Speed", p => p.IncreaseSpeed(0.1f)),
            new BuffOption("+10 HP", p => p.IncreaseHealth(10f))
        };
    }
}