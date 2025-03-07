using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Mixer")]
    public AudioMixer myMixer;

    [Header("Shooting Sounds")]
    public AudioSource ShootingChannel;
    public AudioClip UziShot, Pistol_FShot, M107Shot, AK47Shot;

    [Header("Reload Sounds")]
    public AudioSource reloadingSoundUzi, reloadingSoundPistol_F, reloadingSoundM107, reloadingSoundAK47;

    [Header("Other Sounds")]
    public AudioSource emptyManagazineSoundUzi, throwablesChannel;
    public AudioClip grenadeSound;

    [Header("Zombie Sounds")]
    public AudioSource zombieChannel, zombieChannel2;
    public AudioClip zombieWalking, zombieChase, zombieAttack, zombieHurt, zombieDeath;

    [Header("Player Sounds")]
    public AudioSource playerChannel, gameChannel;
    public AudioClip playerHurt, playerDie;

    [Header("Game Music")]
    public AudioClip gameOverMusic, gameMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Giữ âm thanh khi chuyển scene
    }

    private void Start()
    {
        LoadVolume(); // Load âm lượng ngay khi khởi động
    }

    public void PlayShootingSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.Pistol_F:
                ShootingChannel.PlayOneShot(Pistol_FShot);
                break;
            case Weapon.WeaponModel.Uzi:
                ShootingChannel.PlayOneShot(UziShot);
                break;
            case Weapon.WeaponModel.M107:
                ShootingChannel.PlayOneShot(M107Shot);
                break;
            case Weapon.WeaponModel.Ak47:
                ShootingChannel.PlayOneShot(AK47Shot);
                break;
        }
    }

    public void PlayReloadSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.Pistol_F:
                reloadingSoundPistol_F.Play();
                break;
            case Weapon.WeaponModel.Uzi:
                reloadingSoundUzi.Play();
                break;
            case Weapon.WeaponModel.M107:
                reloadingSoundM107.Play();
                break;
            case Weapon.WeaponModel.Ak47:
                reloadingSoundAK47.Play(); // Đã sửa lỗi gọi nhầm
                break;
        }
    }

    public void PlayGameOverMusic()
    {
        playerChannel.clip = gameOverMusic;
        playerChannel.Play();
    }

    public void PlayGameMusic()
    {
        gameChannel.clip = gameMusic;
        gameChannel.Play();
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f); // Tránh Log(0) gây lỗi
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f); // Tránh Log(0) gây lỗi
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        float SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        SetMusicVolume(musicVolume); // Gọi SetMusicVolume để đảm bảo tính toán đúng
        SetSFXVolume(SFXVolume);
    }
}
