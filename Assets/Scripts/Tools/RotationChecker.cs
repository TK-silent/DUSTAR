using UnityEngine;

public class RotationChecker : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject activateObject1;
    public GameObject activateObject2;
    public GameObject activateObject3;
    public GameObject activateObject4;

    // 设定的角度条件
    public float requiredAngleX1 = 90f;
    public float requiredAngleY1 = 180f;
    public float requiredAngleZ1 = 270f;

    public float requiredAngleX2 = 45f;
    public float requiredAngleY2 = 90f;
    public float requiredAngleZ2 = 180f;

    public float requiredAngleX3 = 30f;
    public float requiredAngleY3 = 270f;
    public float requiredAngleZ3 = 360f; // 注意：360度是0度的等价，可以考虑使用0度

    public float requiredAngleX4 = 60f;
    public float requiredAngleY4 = 360f; // 同样，360度是0度的等价，可以考虑使用0度
    public float requiredAngleZ4 = 90f;

    void Update()
    {
        CheckRotation();
    }

    void CheckRotation()
    {
        CheckAndActivate(
            object1.transform.rotation,
            Quaternion.Euler(requiredAngleX1, 0, 0),
            object2.transform.rotation,
            Quaternion.Euler(0, requiredAngleY1, 0),
            object3.transform.rotation,
            Quaternion.Euler(0, 0, requiredAngleZ1),
            activateObject1);

        CheckAndActivate(
            object1.transform.rotation,
            Quaternion.Euler(requiredAngleX2, 0, 0),
            object2.transform.rotation,
            Quaternion.Euler(0, requiredAngleY2, 0),
            object3.transform.rotation,
            Quaternion.Euler(0, 0, requiredAngleZ2),
            activateObject2);

        CheckAndActivate(
            object1.transform.rotation,
            Quaternion.Euler(requiredAngleX3, 0, 0),
            object2.transform.rotation,
            Quaternion.Euler(0, requiredAngleY3, 0),
            object3.transform.rotation,
            Quaternion.Euler(0, 0, requiredAngleZ3),
            activateObject3);

        CheckAndActivate(
            object1.transform.rotation,
            Quaternion.Euler(requiredAngleX4, 0, 0),
            object2.transform.rotation,
            Quaternion.Euler(0, requiredAngleY4, 0),
            object3.transform.rotation,
            Quaternion.Euler(0, 0, requiredAngleZ4),
            activateObject4);
    }

    void CheckAndActivate(Quaternion currentRotX, Quaternion targetRotX, Quaternion currentRotY, Quaternion targetRotY, Quaternion currentRotZ, Quaternion targetRotZ, GameObject activateObject)
    {
        if (Quaternion.Angle(currentRotX, targetRotX) < 5f &&
            Quaternion.Angle(currentRotY, targetRotY) < 5f &&
            Quaternion.Angle(currentRotZ, targetRotZ) < 5f)
        {
            activateObject.SetActive(true);
        }
        else
        {
            activateObject.SetActive(false);
        }
    }
}