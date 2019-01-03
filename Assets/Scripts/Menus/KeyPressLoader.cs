using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyPressLoader : MonoBehaviour
{
    public string Target;
    public float Cooldown;

    IEnumerator Start ()
    {
        yield return new WaitForSeconds(Cooldown);
        while (true)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(Target);
            }
            yield return null;
        }
    }
}
