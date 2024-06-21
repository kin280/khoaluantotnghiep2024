using UnityEngine;
using UnityEngine.UI;

public class UIbutton : MonoBehaviour
{
    public Button[] Buttons;

    private void Start()
    {
        // Khởi tạo lại trạng thái ban đầu của các nút khi bắt đầu trò chơi
        ResetButtonState();
    }

    private void ResetButtonState()
    {
        // Khởi tạo lại trạng thái ban đầu cho tất cả các nút
        for (int i = 0; i < Buttons.Length; i++)
        {
            // Đặt trạng thái interactable của nút về false
            Buttons[i].interactable = false;
        }

        // Lấy giá trị mức đã mở khóa mặc định (trong trường hợp không tìm thấy trong PlayerPrefs)
        int unlockedlevel = PlayerPrefs.GetInt("Unlockdlevel", 1);

        // Kích hoạt các nút cho các cấp độ đã mở khóa
        for (int i = 0; i < unlockedlevel; i++)
        {
            // Đặt trạng thái interactable của nút về true
            Buttons[i].interactable = true;
        }
    }

    // Bạn có thể gọi phương thức này từ bất kỳ đâu trong game để reset trạng thái của nút
    public void ResetButtonStateFromOutside()
    {
        ResetButtonState();
    }
}
