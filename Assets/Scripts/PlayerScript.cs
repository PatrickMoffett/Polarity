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

    Vector3 InitialPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InitialPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        AddMovementFromInput();
    }

    private void AddMovementFromInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (bIsYang)
        {
            rb.AddForce(new Vector2(-horizontalInput * speed, -verticalInput * speed));
        }
        else
        {
            rb.AddForce(new Vector2(horizontalInput * speed, verticalInput * speed));
        }
    }

    public void Die()
    {
        //TODO Show a died screen first? Play a sound?
        gameObject.transform.position = InitialPosition;
    }
}
