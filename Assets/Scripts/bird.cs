using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    public Transform Hook;

    public GameObject pointPrefab;
    public GameObject[] points;
    public int numberOfPoints = 30;

    public SpringJoint2D sj;
    public Rigidbody2D rb;
    public float releaseTime = .15f;
    private bool isPressed = false;
    private Vector2 Direction;
    float force;

    private void Start()
    {
        points = new GameObject[numberOfPoints];
        for(int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }
    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;

        StartCoroutine(Release());
        //for (int i = 0; i < numberOfPoints; i++)
        //{
          //  Destroy(points[i]);
        //}
    }

    private void Update()
    {
        if (isPressed)
        {
            rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Direction = Hook.position - transform.position;

            force = Vector2.Distance((new Vector2(0.186028f + transform.position.x, -0.1860743f + transform.position.y)), Hook.position) * 4.65f * sj.frequency;

            for(int i = 0; i < points.Length; i++)
            {
                points[i].transform.position = PointPosition(i * 0.1f);
            }
        }
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        sj.enabled = false;
    }

    private Vector2 PointPosition(float t)
    {
        Vector2 currentPosition = (Vector2)transform.position + (Direction.normalized * force * t) + .5f * Physics2D.gravity * Mathf.Pow(t,2);
        return currentPosition;
    } 
}
