using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit : MonoBehaviour
{

    public GameObject slicedFruit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blade"))
        {
            Instantiate(slicedFruit , transform.position , transform.rotation);
            Destroy(gameObject);
        }
    }
}
