using UnityEngine;

public class ToggleCursorOnEsc : MonoBehaviour
{
    private bool isCursorVisible = false; // Ban đầu ẩn con trỏ

    void Start()
    {
        // Ẩn và khóa con trỏ khi bắt đầu game
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Khi nhấn phím ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorVisible = !isCursorVisible; // Đảo trạng thái con trỏ

            Cursor.visible = isCursorVisible; // Hiển thị hoặc ẩn con trỏ
            Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
