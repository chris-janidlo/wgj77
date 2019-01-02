using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [Tooltip("Lower indices are for lower frustration")]
    public string[] FrustrationMessages;
    public Text VictoryText;

    bool falseForCongratsTrueForInfo, keyHasBeenReleased;

    void Start ()
    {
        VictoryText.text = "Congratulations, you died!\n\n";
    }

    void Update ()
    {
        if (!Input.anyKey) keyHasBeenReleased = true;
        
        if (keyHasBeenReleased && Input.anyKeyDown)
        {
            if (!falseForCongratsTrueForInfo)
            {
                VictoryText.text = $"It took you {Mathf.Floor(ScoreManager.Instance.LifeTime)} seconds.";
                
                int index = Mathf.FloorToInt(ScoreManager.Instance.Frustration / 100 * (FrustrationMessages.Length - 1));

                VictoryText.text += " " + FrustrationMessages[index] + "\n\n";
                VictoryText.text += "[press any key to go to start]";

                falseForCongratsTrueForInfo = true;
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
