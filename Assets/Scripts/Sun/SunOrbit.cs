using UnityEngine;

public class SunOrbit : MonoBehaviour
{
    public Transform terrainCenter; // Tâm của Terrain
    public float radius = 50f; // Bán kính quỹ đạo
    public float orbitSpeed = 10f; // Tốc độ quay
    public float fixedZ = 0f; // Giá trị cố định của trục Z

    private float angle = 0f; // Góc quay

    void Start()
    {
        if (terrainCenter == null)
        {
            
            return;
        }

        // Đặt Z của mặt trời bằng Z của Terrain từ đầu
        fixedZ = terrainCenter.position.z;
    }

    void Update()
    {
        if (terrainCenter == null) return;

        // Tăng góc theo thời gian
        angle += orbitSpeed * Time.deltaTime;
        if (angle >= 360f) angle -= 360f;

        // Tính toán vị trí theo quỹ đạo tròn
        float x = terrainCenter.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = terrainCenter.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        // Cập nhật vị trí Mặt Trời (Z giữ nguyên)
        transform.position = new Vector3(x, y, fixedZ);

        // Luôn hướng ánh sáng về trung tâm Terrain
        transform.LookAt(terrainCenter);
    }
}
