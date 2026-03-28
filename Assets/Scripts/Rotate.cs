using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    // make this used in the trigger, so we can turn it on and of
    [SerializeField] public bool rotate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotate from the middle of the object, and we use transform so we don't need a rigidbody
        if (rotate)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    public void turnOfOrON()
    {
        rotate = !rotate;
    }
}
