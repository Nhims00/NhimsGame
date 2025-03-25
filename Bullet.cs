

using System.Collections;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;
    public int damage = 20;
   
    public GameObject effect;
    
    private float timeAlive;
    private void OnEnable()
    {
       
        timeAlive = 0f; // Đặt lại thời gian sống khi kích hoạt
    }

    private void Update()
    {

        // Di chuyển viên đạn

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Tăng thời gian sống
        timeAlive += Time.deltaTime;

        // Nếu vượt quá thời gian sống, trả lại pool
        if (timeAlive >= lifeTime)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        // Trả đối tượng về pool
        FindObjectOfType<ObjectPool>().ReturnObject(gameObject);
    }
    IEnumerator Toelua()
    {
        
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(effect);
        yield return new WaitForSeconds(0.1f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            

            EnemyAI monster = other.gameObject.GetComponent<EnemyAI>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
                
                StartCoroutine(Toelua());
                //// Hủy đạn
                //Destroy(gameObject);
                
            }
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(Toelua());
        }
    }
    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        Instantiate(effect, transform.position, Quaternion.identity);
    //        Destroy(effect);
    //    }
    //}

}