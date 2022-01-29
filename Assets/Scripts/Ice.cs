using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This behavior assumes only one Player tagged GameObject will be colliding with it
public class Ice : MonoBehaviour
{
    //The Drag to set while in the collisionbox
    [SerializeField]
    float LinearDrag = 0;

    //the original drag of the colliding gameobject
    float OriginalLinearDrag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //On Trigger Enter cache the players original drag and set it to our value
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb =collision.gameObject.GetComponent<Rigidbody2D>();
            OriginalLinearDrag = rb.drag;
            rb.drag = LinearDrag;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //On Trigger Exit, restore the players original drag
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().drag = OriginalLinearDrag;
        }
    }
}
