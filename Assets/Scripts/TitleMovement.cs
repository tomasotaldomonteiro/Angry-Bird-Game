using UnityEngine;

public class TitleMovement : MonoBehaviour
{
   
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float wave = Mathf.Sin(Time.time * speed);
        transform.position = startPos + offset * wave;
        
    }
}
