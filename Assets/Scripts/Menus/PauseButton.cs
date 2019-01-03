using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text), typeof(Button))]
public class PauseButton : MonoBehaviour
{
    public GameObject PauseMenu;
    public string DisplayText;
    public PauseButtonType Type;

    Text text;
    Button button;

    void Start()
    {
        text = GetComponent<Text>();
        button = GetComponent<Button>();

        button.onClick.AddListener(onClick);
    }

    void OnSelect ()
    {
        text.text = "> " + DisplayText;
    }

    void OnDeselect ()
    {
        text.text = DisplayText;
    }

    void onClick ()
    {
        switch (Type)
        {
            case PauseButtonType.Restart:
                ScoreManager.Instance.Initialize();
                SceneManager.LoadScene("LivingRoom");
                break;

            case PauseButtonType.Resume:
                Destroy(PauseMenu);
                break;
        }
    }
}

public enum PauseButtonType { Resume, Restart }
