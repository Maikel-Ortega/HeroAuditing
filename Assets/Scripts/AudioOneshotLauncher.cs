using UnityEngine;

public class AudioOneshotLauncher : MonoBehaviour
{
    float volume = 0.5f;
    public void PlayOneShot(string s)
    {
        AudioManager.Play(s, false,volume);
    }
}
