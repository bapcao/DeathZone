using UnityEngine;
using TMPro;
using System.Collections;

public class NPCMission : MonoBehaviour
{
    public GameObject missionPanel; // UI hiển thị nhiệm vụ ban đầu
    public TextMeshProUGUI missionText; // Văn bản hiển thị nhiệm vụ bằng TextMeshPro
    public GameObject missionCornerPanel; // UI nhiệm vụ ở góc trên trái
    public TextMeshProUGUI missionCornerText; // Văn bản nhiệm vụ ở góc trên trái
    public float detectionRadius = 3f; // Bán kính phát hiện nhân vật
    public float textSpeed = 0.05f; // Tốc độ hiển thị từng chữ

    private Transform player;
    private string currentMission = "Nhiệm vụ: Bạn hãy sống sót qua 2 wave của zombie!";
    private bool wave1Completed = false;
    private bool isDisplayingText = false;

    void Start()
    {
        missionPanel.SetActive(false); // Ẩn nhiệm vụ khi bắt đầu
        missionCornerPanel.SetActive(false); // Ẩn nhiệm vụ góc trên trái khi bắt đầu
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRadius && !missionPanel.activeSelf && !isDisplayingText)
        {
            missionPanel.SetActive(true); // Hiện nhiệm vụ khi nhân vật đến gần
            StartCoroutine(ShowText()); // Chạy hiệu ứng hiển thị từng chữ
        }
        else if (distance > detectionRadius && missionPanel.activeSelf)
        {
            missionPanel.SetActive(false); // Ẩn nhiệm vụ khi nhân vật đi xa
            missionCornerPanel.SetActive(true); // Hiển thị nhiệm vụ ở góc trên trái
            missionCornerText.text = currentMission;
        }
    }

    IEnumerator ShowText()
    {
        isDisplayingText = true;
        missionText.text = "";
        foreach (char letter in currentMission)
        {
            missionText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isDisplayingText = false;
    }

    public void CompleteWave1()
    {
        if (!wave1Completed)
        {
            wave1Completed = true;
            currentMission = "Nhiệm vụ: Hiện tại bạn chỉ cần vượt qua đợt tấn công cuối cùng!";
            missionCornerText.text = currentMission; // Cập nhật nhiệm vụ góc trên trái
        }
    }
}
