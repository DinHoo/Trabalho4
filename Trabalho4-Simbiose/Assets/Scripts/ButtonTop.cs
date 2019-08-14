using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTop : MonoBehaviour
{
    [SerializeField]
    Platform[] platform;

    [SerializeField]
    bool isLeftTopButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                if (isLeftTopButton == true)
                {
                    platform[0].transform.Rotate(0, 0, 3);
                    platform[1].transform.Rotate(0, 0, 3);
                }
                else
                {
                    platform[0].transform.Rotate(0, 0, -3);
                    platform[1].transform.Rotate(0, 0, -3);
                }
            }
        }
    }
}
