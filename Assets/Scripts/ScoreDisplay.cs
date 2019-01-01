using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Image HungerBar, DirtinessBar;
    public Image AngerDisplay;

    [Tooltip("Lower indices represent lower frustration")]
    public Sprite[] AngerIcons;

    void Update ()
    {
        HungerBar.fillAmount = ScoreManager.Instance.Hunger / 100;
        DirtinessBar.fillAmount = ScoreManager.Instance.Dirtiness / 100;

        var angerIndex = Mathf.FloorToInt(ScoreManager.Instance.Frustration / 100 * (AngerIcons.Length - 1));
        AngerDisplay.sprite = AngerIcons[angerIndex];
    }
}
