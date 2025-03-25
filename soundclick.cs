using UnityEngine;
using UnityEngine.UI;

public class soundclick : MonoBehaviour
{
    public AudioSource clickSound;
    public Button[] buttons;

    void Start()
    {
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => PlaySound());
        }
    }

    void PlaySound()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }
}
