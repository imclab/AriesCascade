// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class MouseOrbit : MonoBehaviour
{
    public static GameObject target;
    float distanceMin = 10.0f;
    float distanceMax = 15.0f;
    float distanceInitial = 12.5f;
    float scrollSpeed = 1.0f;

    float xSpeed = 250.0f;
    float ySpeed = 120.0f;

    int yMinLimit = -20;
    int yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;
    private float distanceCurrent;

    [AddComponentMenu("Camera-Control/Mouse Orbit")]
    //partial class MouseOrbit { }

    void Start()
    {
        distanceCurrent = distanceInitial;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation
        if (rigidbody)
            rigidbody.freezeRotation = true;
    }

    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            distanceCurrent -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

            distanceCurrent = Mathf.Clamp(distanceCurrent, distanceMin, distanceMax);
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 multiplier = new Vector3(0.0f, 0.0f, -distanceCurrent);
            Vector3 position = rotation * multiplier + target.transform.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}