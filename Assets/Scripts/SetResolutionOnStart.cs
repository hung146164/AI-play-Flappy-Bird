using UnityEngine;

public class SetResolutionOnStart : MonoBehaviour
{
    void Start()
    {
        // Đặt kích thước thành 800x600, ở chế độ cửa sổ
        Screen.SetResolution(960, 540, false);
    }
}