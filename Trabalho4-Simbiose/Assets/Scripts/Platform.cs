using UnityEngine;

public class Platform : MonoBehaviour
{
    bool isRight;
    [SerializeField]
    float speed;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        movimento();
    }

    private void movimento()
    {
        if (isRight)
        {
            transform.Translate(Vector2.right* speed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        }
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Parede"))
            isRight = !isRight;
    }
}