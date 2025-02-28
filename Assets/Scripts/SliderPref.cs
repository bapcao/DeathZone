using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderPrefs : MonoBehaviour
{
    public Slider sound_nen;
    public Slider sound_attack;
    public Button saveButton;
    public TMP_Text saveMessage;
    private string sliderKey1 = "SliderValue1";
    private string sliderKey2 = "SliderValue2";

    void Start()
    {
        // Load giá trị từ PlayerPrefs (mặc định là 0.5 nếu chưa có)
        sound_nen.value = PlayerPrefs.GetFloat(sliderKey1, 0.5f);
        sound_attack.value = PlayerPrefs.GetFloat(sliderKey2, 0.5f);

        // Gán sự kiện lưu khi bấm nút
        saveButton.onClick.AddListener(SaveSliders);
    }

    void SaveSliders()
    {
        // Lưu giá trị vào PlayerPrefs
        PlayerPrefs.SetFloat(sliderKey1, sound_nen.value);
        PlayerPrefs.SetFloat(sliderKey2, sound_attack.value);
        PlayerPrefs.Save(); // Đảm bảo giá trị được lưu ngay lập tức

        // Hiển thị thông báo đã lưu
        if (saveMessage != null)
        {
            saveMessage.text = "Đã lưu thành công!";
            Invoke("ClearMessage", 2f); // Xóa thông báo sau 2 giây
        }
    }

    void ClearMessage()
    {
        if (saveMessage != null)
        {
            saveMessage.text = "";
        }
    }
}