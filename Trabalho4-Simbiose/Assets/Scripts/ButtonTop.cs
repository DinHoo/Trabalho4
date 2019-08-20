using UnityEngine;

public class ButtonTop : MonoBehaviour
{
    [SerializeField]
    private Platform[] platform;

    [SerializeField]
    private bool isLeftTopButton;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                if (isLeftTopButton == true)
                {
                    foreach(Platform p in platform)
                    p.transform.Rotate(0, 0, 3);
                    
                }
                else
                {
                    foreach (Platform p in platform)
                        p.transform.Rotate(0, 0, -3);
                }
            }
        }
#endif

#if UNITY_ANDROID
        foreach (Touch toque in Input.touches)
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(toque.position)))
            {
                if (toque.phase == TouchPhase.Began || toque.phase == TouchPhase.Stationary)
                {
                    if (isLeftTopButton == true)
                    {
                        foreach (Platform p in platform)
                            p.transform.Rotate(0, 0, 3);
                    }
                    else
                    {
                        foreach (Platform p in platform)
                            p.transform.Rotate(0, 0, -3);
                    }
                }
            }
        }

#endif
    }
}