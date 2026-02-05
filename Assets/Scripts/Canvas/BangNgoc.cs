using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BangNgoc : MonoBehaviour {

    public Text SLNgocHoa;
    public Text SLNgocTho;
    public Text SLNgocPhong;

    void Update () {
        // Lấy thông tin 3 loại ngọc
        var ngocHoa = VatPhamController.GetItemByID(1);
        var ngocTho = VatPhamController.GetItemByID(2);
        var ngocPhong = VatPhamController.GetItemByID(3);

        // Kiểm tra xem CẢ 3 món có tồn tại (khác null) hay không trước khi xử lý
        if (ngocHoa != null && ngocTho != null && ngocPhong != null)
        {
            // Kiểm tra ID khác -1 (theo logic cũ của bạn)
            if (ngocHoa.ID != -1 && ngocTho.ID != -1 && ngocPhong.ID != -1)
            {
                SLNgocHoa.text = "x" + ngocHoa.Quality.ToString();
                SLNgocTho.text = "x" + ngocTho.Quality.ToString();
                SLNgocPhong.text = "x" + ngocPhong.Quality.ToString();
            }
        }
        else 
        {
            // Nếu bị null, có thể hiển thị số lượng là 0 thay vì báo lỗi đỏ
            if(SLNgocHoa != null) SLNgocHoa.text = "x0";
            if(SLNgocTho != null) SLNgocTho.text = "x0";
            if(SLNgocPhong != null) SLNgocPhong.text = "x0";
        }
    }
}