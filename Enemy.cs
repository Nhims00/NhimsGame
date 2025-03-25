using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float rotationSpeed = 50f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 1f;

    private float lastShotTime = 0f;
    private enum EnemyState { Idle, Shooting }
    private EnemyState currentState = EnemyState.Idle;

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player chưa được gán vào Enemy!");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
            currentState = EnemyState.Shooting;
        else
            currentState = EnemyState.Idle;

        switch (currentState)
        {
            case EnemyState.Idle:
                RotateInPlace();
                break;
            case EnemyState.Shooting:
                LookAtPlayer();
                AttackPlayer();
                break;
        }
    }

    void RotateInPlace()
    {
        // Quái sẽ xoay tròn tại chỗ quanh trục Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void AttackPlayer()
    {
        if (Time.time - lastShotTime > shootCooldown)
        {
            lastShotTime = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = transform.forward * 10f;
        }
        else
        {
            Debug.LogWarning("Bullet prefab thiếu Rigidbody!");
        }
    }
}
