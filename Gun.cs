using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool; // Gắn ObjectPool chứa viên đạn
    [SerializeField] private Transform firePoint; // Vị trí bắn đạn
    [SerializeField] private Camera mainCamera; // Camera chính trong scene
    public AudioSource ban;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Bắn khi nhấn Space
        {
            StartCoroutine(Fire());
            ban.Play();
        }
    }

    IEnumerator Fire()
    {
        // Lấy một viên đạn từ Object Pool
        GameObject bullet = bulletPool.GetObject();

        // Đặt vị trí của viên đạn tại vị trí bắn (firePoint)
        bullet.transform.position = firePoint.position;

        // Raycast từ vị trí của camera tới vị trí chuột
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Tính hướng từ firePoint tới vị trí trúng (hit point)
            Vector3 direction = (hit.point - firePoint.position).normalized;

            // Đặt hướng của viên đạn
            bullet.transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // Nếu không trúng vật thể, bắn theo hướng thẳng về phía camera
            Vector3 direction = (ray.GetPoint(100f) - firePoint.position).normalized;
            bullet.transform.rotation = Quaternion.LookRotation(direction);
        }
        yield return new WaitForSeconds(1f);
    }

    
}