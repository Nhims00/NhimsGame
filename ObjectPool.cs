using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Prefab đối tượng cần pooling
    [SerializeField] private int initialPoolSize = 10; // Số lượng đối tượng ban đầu

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        // Khởi tạo pool với số lượng đối tượng ban đầu
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObject();
        }
    }

    // Tạo một đối tượng mới và thêm vào pool
    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false); // Vô hiệu hóa đối tượng ngay sau khi tạo
        pool.Add(obj);
        return obj;
    }

    // Lấy một đối tượng từ pool
    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy) // Tìm đối tượng chưa được kích hoạt
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Nếu không có đối tượng nào khả dụng, tạo mới
        return CreateNewObject();
    }

    // Trả đối tượng lại vào pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // Vô hiệu hóa đối tượng
    }
}