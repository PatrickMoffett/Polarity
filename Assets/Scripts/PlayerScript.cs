using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    bool bIsYang = true;

    [SerializeField]
    float speed = 10f;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (bIsYang) {
            rb.AddForce(new Vector2(-horizontalInput*speed,-verticalInput * speed));
        }
        else
        {
            rb.AddForce(new Vector2(horizontalInput * speed, verticalInput * speed));
        }
    }
}
