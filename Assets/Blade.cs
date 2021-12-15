using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject BladeTrailPrefab;
    float minCuttingVelocity = .001f;
    Vector2 previousPosition;

    GameObject currentBladeTrail;

    Rigidbody2D rb;
    Camera cam;

    CircleCollider2D circleCollider;
    bool isCutting = false;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }
    }
    
    void UpdateCut()
    {
        Vector2 newPossition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPossition;

        float velocity = (newPossition - previousPosition).magnitude;
        
        if(velocity > minCuttingVelocity)
        {
            circleCollider.enabled = true;
        }
        else
        {
            circleCollider.enabled = false;
        }

        previousPosition = newPossition;
    }
    void StartCutting()
    {
        isCutting = true;
        currentBladeTrail = Instantiate(BladeTrailPrefab, transform);
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        circleCollider.enabled = true;
    }
    void StopCutting()
    {
        isCutting = false;
        currentBladeTrail.transform.SetParent(null);
        circleCollider.enabled = false;
        Destroy(currentBladeTrail);
    }
}
