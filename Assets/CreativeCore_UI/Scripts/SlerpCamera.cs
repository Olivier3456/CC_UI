using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlerpCamera : MonoBehaviour
{

    public Transform cam;
    public Transform menuPosition;
    public Transform mainPosition;

    public Transform settingsMenu;

    public float speed = 1.0f;

    float startTime;

    float fracMovement;

    public void MoveCameraToMenuView()
    {
        startTime = Time.time;

        StartCoroutine(LerpCoroutineToMenuView(mainPosition, menuPosition));
    }

    public void MoveCameraToMainView()
    {
        startTime = Time.time;

        StartCoroutine(LerpCoroutineToMenuView(menuPosition, mainPosition));
    }

    private IEnumerator LerpCoroutineToMenuView(Transform from, Transform to)
    {
        fracMovement = (Time.time - startTime) * speed;
        transform.position = Vector3.Lerp(from.position, to.position, fracMovement);
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, fracMovement);

        if (to == menuPosition)
        {
            settingsMenu.localScale = new Vector3(fracMovement, fracMovement, fracMovement);
            Debug.Log("LocalScale va de 0 à 1.");
        }
        else
        {
            settingsMenu.localScale = new Vector3(1 - fracMovement, 1 - fracMovement, 1 - fracMovement);
            Debug.Log("LocalScale revient de 1 à 0.");
        }

        yield return null;

        if (fracMovement < 0.99f) StartCoroutine(LerpCoroutineToMenuView(from, to));
    }
}
