using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class KeybindManager : MonoBehaviour
{
    public Text forwardKeyText, leftKeyText, backwardKeyText, rightKeyText, fireKeyText;
    private GameObject currentKey;
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    

    void Start()
    {
        LoadKeyBindings();
        UpdateKeyTexts();
    }

    void Update()
    {
        if (currentKey != null)
        {
            currentKey.GetComponentInChildren<Text>().text = "Press a key...";
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    keys[currentKey.name] = keyCode;
                    currentKey.GetComponentInChildren<Text>().text = keyCode.ToString();
                    currentKey = null;
                    SaveKeyBindings();
                    break;
                }
            }
        }
    }

    private void UpdateKeyTexts()
    {
        forwardKeyText.text = keys["Forward"].ToString();
        leftKeyText.text = keys["Left"].ToString();
        backwardKeyText.text = keys["Backward"].ToString();
        rightKeyText.text = keys["Right"].ToString();
        fireKeyText.text = keys["Fire"].ToString();
    }

    public void StartRebindingForKey(GameObject clickedKey)
    {
        currentKey = clickedKey;
    }

    public KeyCode GetKeyForAction(string action)
    {
        return keys[action];
    }

     public void SaveKeyBindings()
    {
        foreach (var key in keys)
        {   
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
    }

public void LoadKeyBindings()
    {
        keys["Forward"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", KeyCode.W.ToString()));
        keys["Left"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", KeyCode.A.ToString()));
        keys["Backward"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", KeyCode.S.ToString()));
        keys["Right"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", KeyCode.D.ToString()));
        keys["Fire"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire", KeyCode.Space.ToString()));

        UpdateKeyTexts();
    }
}
