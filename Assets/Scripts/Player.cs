using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodyScreen;

    public TextMeshProUGUI playerHealthUI;
    public GameObject gameOverUI;

    public bool isDead;

    private void Start()
    {
        //playerHealthUI.text = $"health: {HP}";
    }
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            print("Player Dead");
            PlayDead();
            isDead = true;
        }
        else
        {
            print("Player Hit");
            StartCoroutine(BloodScreenEffect());
            playerHealthUI.text = $"health: {HP}";
            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);
        }
    }

    private void PlayDead()
    {
        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDie);

        SoundManager.Instance.playerChannel.clip = SoundManager.Instance.gameOverMusic;
        SoundManager.Instance.playerChannel.PlayDelayed(2f);

        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        // Dying Animatiion
        GetComponentInChildren<Animator>().enabled = true;
        playerHealthUI.gameObject.SetActive(false);

        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }

    private IEnumerator ShowGameOverUI()
    {
        Debug.Log("Hiển thị Game Over UI");

        yield return new WaitForSecondsRealtime(1f);

        if (gameOverUI == null)
        {
            Debug.LogError("gameOverUI bị null! Kiểm tra lại trong Inspector.");
            yield break; // Dừng coroutine nếu gameOverUI chưa được gán
        }

        gameOverUI.gameObject.SetActive(true);
        Debug.Log("Game Over UI đã được kích hoạt");

        StartCoroutine(ReturnToMainMenu());

    }

    private IEnumerator ReturnToMainMenu()
    {
        Debug.Log("Đang load scene Menu..."); // Kiểm tra
    yield return new WaitForSeconds(5f);
    Debug.Log("Đã xong thời gian chờ, load Menu"); // Kiểm tra
    SceneManager.LoadScene("Menu");
    }

    private IEnumerator BloodScreenEffect()
    {
        if (bloodyScreen.activeInHierarchy == false)
        {
            bloodyScreen.SetActive(true);
        }

        var image = bloodyScreen.GetComponentInChildren<Image>();

        //Set the initial alpha value to 1 (fully visible)
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            //Caculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the cokor with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; // wait for the next frame

        }


        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if (isDead == false)
            {
                TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
            }
        }
    }
}
