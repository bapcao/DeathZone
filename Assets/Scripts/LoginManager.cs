using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI errorMessage;
    public string nextScene = "MainMenu"; // Tên scene sau khi đăng nhập thành công

    // Danh sách tài khoản tĩnh (có thể mở rộng sau này)
    private Dictionary<string, string> staticAccounts = new Dictionary<string, string>()
    {
        { "thang@gmail.com", "1234" },
        { "user1@gmail.com", "password" },
        { "player@game.com", "game123" }
    };

    private void Start()
    {
        errorMessage.text = ""; // Xóa thông báo lỗi ban đầu

        // Bắt sự kiện nhấn Enter để đăng nhập
        emailInput.onSubmit.AddListener(delegate { Login(); });
        passwordInput.onSubmit.AddListener(delegate { Login(); });
    }

    public void Login()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();

        // Kiểm tra tài khoản trong danh sách tĩnh
        if (staticAccounts.ContainsKey(email) && staticAccounts[email] == password)
        {
            Debug.Log("✅ Đăng nhập thành công!");
            SceneManager.LoadScene(nextScene); // Chuyển sang MainMenu hoặc scene khác
        }
        else
        {
            errorMessage.text = "❌ Sai tài khoản hoặc mật khẩu!";
        }
    }
}
