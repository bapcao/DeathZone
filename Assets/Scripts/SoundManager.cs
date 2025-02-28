using System.Collections;
using System.Collections.Generic;
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
    public AudioClip UziShot;
    public AudioClip Pistol_FShot;
    public AudioClip M107Shot;
    public AudioClip AK47Shot;

    [Header("Reload Sounds")]
    public AudioSource reloadingSoundUzi;
    public AudioSource reloadingSoundPistol_F;
    public AudioSource reloadingSoundM107;
    public AudioSource reloadingSoundAK47;

    [Header("Other Sounds")]
    public AudioSource emptyManagazineSoundUzi;
    public AudioSource throwablesChannel;
    public AudioClip grenadeSound;

    [Header("Zombie Sounds")]
    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;
    public AudioSource zombieChannel;
    public AudioSource zombieChannel2;

    [Header("Player Sounds")]
    public AudioSource playerChannel;
    public AudioSource gameChanel;
    public AudioClip playerHurt;
    public AudioClip playerDie;

    [Header("Game Music")]
    public AudioClip gameOverMusic;
    public AudioClip gameMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ âm thanh khi chuyển scene
        }
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
                reloadingSoundM107.Play();
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
        gameChanel.clip = gameMusic;
        gameChanel.Play();
    }

    public void SetMusicVolume(float volume)
    {
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        float SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        myMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);
        myMixer.SetFloat("SFX", Mathf.Log10(SFXVolume) * 20);
    }
}