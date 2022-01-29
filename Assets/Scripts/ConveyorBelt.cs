using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    Vector2 ConveyorDirection = new Vector2(0, 0);
    [SerializeField]
    float ConveyorSpeed = 3;

    List<Rigidbody2D> m_Rigidbodies;

    // Start is called before the first frame update
    void Start()
    {
        ConveyorDirection.Normalize();

        m_Rigidbodies = new List<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        foreach(Rigidbody2D rb in m_Rigidbodies)
        {
            rb.AddForce(ConveyorDirection * ConveyorSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            m_Rigidbodies.Add(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_Rigidbodies.Remove(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }
}
