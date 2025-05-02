using UnityEngine;

public class FloatingPlant : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Rotate
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // Float up and down
        float y = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, startPos.y + y, startPos.z);
    }
}
