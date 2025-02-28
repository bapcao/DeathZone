using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button playButton; // Khai báo biến public playButton, gán nút Play trong Inspector

    void Start() // Hàm Start() được gọi khi script bắt đầu
    {
        if (playButton == null) // Kiểm tra xem playButton đã được gán hay chưa
        {
            Debug.LogError("Play button not assigned in the Inspector!"); // In lỗi nếu chưa gán
            return; // Thoát khỏi hàm
        }
        playButton.onClick.AddListener(StartLoadScenes); // Thêm listener cho sự kiện click nút, gọi hàm StartLoadScenes
    }

    void StartLoadScenes() // Hàm StartLoadScenes()
    {
        StartCoroutine(LoadScenes()); // Bắt đầu coroutine LoadScenes
    }

    IEnumerator LoadScenes() // Coroutine LoadScenes()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene1"); // Tải scene có tên "Scene1"
        while (!asyncLoad.isDone) { yield return null; } // Chờ cho đến khi scene 1 được tải xong
    }
}