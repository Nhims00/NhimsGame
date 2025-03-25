using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;

    public Slider _slider;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;
    public GameObject diecmr, maincmr;
    public GameObject panel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        maincmr.gameObject.SetActive(true);
        diecmr.gameObject.SetActive(false);
        panel.SetActive(false);
    }

    void Update()
    {
        Move();

        // Kiểm tra nếu nhấn Space và nhân vật đang chạm đất
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Chặn nhảy lần 2
    }

    IEnumerator ShowAndHidePanel()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Nếu nhân vật chạm đất, cho phép nhảy lại
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("adfasdfas");
            _slider.value--;
            StartCoroutine(ShowAndHidePanel());
            if (_slider.value == 0)
            {
                Destroy(gameObject);
                diecmr.SetActive(true);
                maincmr.SetActive(false);
                Time.timeScale = 0;
            }
        }
        if (collision.gameObject.CompareTag("c1"))
        {
            
            diecmr.SetActive(true);
            maincmr.SetActive(false);
        }
    }

}
