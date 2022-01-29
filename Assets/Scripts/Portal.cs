using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    GameObject ExitPortal;

    bool bIgnoreNextCollision = false;
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
        {
            if (bIgnoreNextCollision)
            {
                bIgnoreNextCollision = false;
                return;
            }
            else
            {

                ExitPortal.GetComponent<Portal>().bIgnoreNextCollision = true;
                collision.gameObject.transform.position = ExitPortal.transform.position;
            }
        }
    }

}
