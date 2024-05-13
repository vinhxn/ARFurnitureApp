using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleObject : MonoBehaviour
{
    // Assign in the inspector
    private GameObject objectToScale;
    public Slider slider;

    // Preserve the original and current orientation
    private float previousValue;
    private float startValue = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Awake()
    {
        // Assign a callback for when this slider changes
        slider.onValueChanged.AddListener(OnSliderChanged);

        // And current value
        previousValue = slider.value;
    }

    void OnSliderChanged(float value)
    {
        // How much we've changed
        float delta = value - previousValue;
        float scale = 0.02f;
        Vector3 scaleU = new Vector3(scale, scale, scale);
        Vector3 scaleD = new Vector3(-scale, -scale, -scale);
        if (delta > 0)
        {
            objectToScale.transform.localScale += scaleU;
        }
        else if(objectToScale.transform.localScale.x > 0 &&
            objectToScale.transform.localScale.y > 0 &&
            objectToScale.transform.localScale.z > 0)
        {
            objectToScale.transform.localScale += scaleD;
        }
        
        // Set our previous value for the next change
        previousValue = value;
    }
    // Update is called once per frame
    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.current.ScreenPointToRay(touch.position);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                objectToScale = hitObject.transform.parent.transform.parent.gameObject;
                objectToScale.GetComponent<Recolour>().SetSelected();
            }
        }
    }

    public void Deselect()
    {
        objectToScale.GetComponent<Recolour>().SetOriginalMaterial();
        objectToScale = null;
        slider.value = startValue;
    }
}
