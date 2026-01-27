using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;

    public Slider volumeSlider;

    private void Start()
    {
        // load saved volume
        float volume = PlayerPrefs.GetFloat("Volume", 1f);

        // apply to audio mixer
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20f);

        // update ui
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
    }

    public void SetVolume(float level)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("Volume", level);
    }

}
