using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    public static float MoveSpeed { get; set; } // Tạo 1 biến MoveSpeed thể hiện tốc độ di chuyển của đối tượng
    public float MoveRange;                    // Tạo 1 biến MoveRange thể hiện khoảng cách đối tượng di chuyển đến đâu
    private Vector3 oldPos;                     // Vị trí của đối tượng khi hết khoảng cách


    // Use this for initialization
    void Start()                                // Hàm Start khởi tạo khi đối tượng được thực thi
    {
        oldPos = transform.position;            // vị trí ban đầu của đối tượng
    }

    // Update is called once per frame
    void Update()                               // Hàm update sẻ chạy liên tục khi bắt đầu
    {

        transform.Translate(new Vector3(-1 * Time.deltaTime * MoveSpeed, 0, 0));        //Hàm Translate() tác dụng di chuyển đối tượng đến một điểm (Vecto) với tốc độ MoveSpeed
        if (Vector3.Distance(oldPos, transform.position) > MoveRange)                   // Nếu Vị trí(Vecto) mà đối tượng di chuyển đến vượt quá vị trí MoveRange thỳ...
        {
           transform.position = oldPos;                                                 // Hàm Tranforms() Tác dụng làm đối tượng nhãy đến 1 vị trí Vecto(OldPos) vị trí cũ của đối tượng

        }
    }
}


