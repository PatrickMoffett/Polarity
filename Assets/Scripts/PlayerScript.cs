using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    bool bIsYang = true;

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float WinRadius = 1.1f;
    
    [SerializeField]
    GameObject OtherPlayer;

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
        float distance = (OtherPlayer.transform.position - gameObject.transform.position).magnitude;
        if(distance < WinRadius)
        {
            //TODO Show a win screen? Go To Next Level?
            Debug.Log("You Won!");
        }
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
        //TODO Show a died screen first
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
