using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    [SerializeField]
    float LinearDrag = 0;

    float OriginalLinearDrag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb =collision.gameObject.GetComponent<Rigidbody2D>();
            OriginalLinearDrag = rb.drag;
            rb.drag = LinearDrag;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().drag = OriginalLinearDrag;
        }
    }
}
