using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        StartCoroutine(DelayedLoadVolume()); // Trì hoãn load volume để đảm bảo SoundManager đã khởi tạo

        masterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
        musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        SFXSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    }

    private IEnumerator DelayedLoadVolume()
    {
        yield return new WaitForSeconds(0.1f); // Chờ một chút để đảm bảo SoundManager sẵn sàng
        LoadVolume();
    }

    public void SetMasterVolume()
    {
        float masterVolume = masterSlider.value;
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        AudioListener.volume = masterVolume;

        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        if (SoundManager.Instance == null) return; // Kiểm tra tránh lỗi NullReferenceException

        float volume = musicSlider.value * masterSlider.value;
        SoundManager.Instance.SetMusicVolume(volume);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume()
    {
        if (SoundManager.Instance == null) return; // Kiểm tra tránh lỗi NullReferenceException

        float volume = SFXSlider.value * masterSlider.value;
        SoundManager.Instance.SetSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        SetMasterVolume(); // Gọi để cập nhật âm lượng ngay lập tức
    }
}
