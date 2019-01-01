using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    void Update ()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("LivingRoom");
        }
    }
}
