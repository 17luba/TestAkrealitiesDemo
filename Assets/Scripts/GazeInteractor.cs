using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class GazeInteractor : MonoBehaviour
{
    public float gazeTime = 3f;
    public Image reticleFillImage; // cercle qui se remplit
    private float timer = 0f;
    private bool isGazing = false;
    private GameObject currentTarget;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("UIButton"))
            {
                if (currentTarget != hitObject)
                {
                    currentTarget = hitObject;
                    timer = 0f;
                }

                isGazing = true;
                timer += Time.deltaTime;
                reticleFillImage.fillAmount = timer / gazeTime;

                if (timer >= gazeTime)
                {
                    ExecuteEvents.Execute(currentTarget, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    timer = 0f; // reset pour éviter double clic
                }
            }
            else
            {
                ResetGaze();
            }
        }
        else
        {
            ResetGaze();
        }
    }

    void ResetGaze()
    {
        isGazing = false;
        timer = 0f;
        reticleFillImage.fillAmount = 0f;
    }
}
