using UnityEngine;

public class StarDisplay : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public int level = 1;

    // Hằng số cho mẫu khóa PlayerPrefs
    private const string StarKeyPattern = "Stars_Level_{0}";

    // Phương thức để lưu dữ liệu sao dựa trên phần trăm điểm số
    public void SaveStarData(float scorePercentage, int level)
    {
        Debug.Log(level);
        int newStars = CalculateStars(scorePercentage); // Tính số sao mới dựa trên phần trăm điểm số
        string key = GetStarKey(level); // Lấy khóa lưu trữ cho level

        int currentStars = PlayerPrefs.GetInt(key, 0); // Lấy số sao hiện tại đã lưu, mặc định là 0 nếu chưa có dữ liệu
        // Chỉ cập nhật số sao nếu số sao mới lớn hơn số sao hiện tại
        if (newStars > currentStars)
        {
            PlayerPrefs.SetInt(key, newStars); // Lưu số sao mới vào PlayerPrefs
            PlayerPrefs.Save(); // Lưu lại tất cả thay đổi vào PlayerPrefs

            // Nếu level hiện tại là level đang xét thì cập nhật hiển thị sao
            if (this.level == level)
            {
                UpdateStarDisplay(newStars); // Cập nhật hiển thị sao với số sao mới
            }
        }


    }

    // Phương thức để tính toán số sao dựa trên phần trăm điểm số
    private int CalculateStars(float scorePercentage)
    {
        if (scorePercentage >= 80)
            return 3; // 3 sao nếu điểm số >= 80%
        else if (scorePercentage >= 70)
            return 2; // 2 sao nếu điểm số >= 70%
        else if (scorePercentage >= 60)
            return 1; // 1 sao nếu điểm số >= 60%
        else
            return 0; // 0 sao nếu điểm số < 60%
    }

    // Phương thức để tạo khóa PlayerPrefs cho level
    private string GetStarKey(int level)
    {
        return string.Format(StarKeyPattern, level); // Tạo khóa theo mẫu "Stars_Level_{level}"
    }

    // Phương thức để tải dữ liệu sao cho một level cụ thể
    public void LoadStarData(int level)
    {
        string key = GetStarKey(level); // Lấy khóa lưu trữ cho level
        int stars = PlayerPrefs.GetInt(key, 0); // Lấy số sao đã lưu, mặc định là 0 nếu không tìm thấy
        UpdateStarDisplay(stars); // Cập nhật hiển thị sao với số sao đã lấy được
    }

    // Phương thức để cập nhật hiển thị sao dựa trên số sao
    public void UpdateStarDisplay(int stars)
    {
        if (star1 != null) star1.SetActive(stars >= 1); // Hiển thị sao thứ nhất nếu số sao >= 1
        if (star2 != null) star2.SetActive(stars >= 2); // Hiển thị sao thứ hai nếu số sao >= 2
        if (star3 != null) star3.SetActive(stars >= 3); // Hiển thị sao thứ ba nếu số sao >= 3
    }

    // Phương thức ví dụ để tải số sao cho level hiện tại khi bắt đầu
    private void Start()
    {
        LoadStarData(level); // Tải dữ liệu sao cho level hiện tại khi game bắt đầu
    }
}
