using UnityEngine;

public class Unmute : MonoBehaviour
{
    private void Awake()
    {
        AudioListener.pause = false;
    }
}
