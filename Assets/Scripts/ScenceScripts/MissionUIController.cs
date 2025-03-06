using System.Collections;
using UnityEngine;

public class MissionUIController : MonoBehaviour
{
    public Canvas missionCanvas; // Kéo Canvas từ Inspector vào đây
    public float displayTime = 10f; // Hiển thị trong 10 giây

    private void Start()
    {
        StartCoroutine(ShowMission());
    }

    private IEnumerator ShowMission()
    {
        missionCanvas.gameObject.SetActive(true); // Hiện Canvas
        yield return new WaitForSeconds(displayTime); // Chờ 10 giây
        missionCanvas.gameObject.SetActive(false); // Ẩn Canvas
    }
}
