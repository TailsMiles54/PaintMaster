using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class Paint : MonoBehaviour
{

    private LineRenderer LineRenderer;
    private Vector3 mousePos;
    private int a = 0;
    private int i = 1;
    private Vector3 bpos;
    private GameObject[] allline;

    public GameObject Canvas;
    public Material[] material;
    public Slider Slider;
    void LateUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            createLine();
            LineRenderer.SetPosition(0, mousePos);
            LineRenderer.SetPosition(1, bpos);
        }
        bpos = mousePos;
    }

    void createLine()
    {
        LineRenderer = new GameObject("Line"+i).AddComponent<LineRenderer>();
        LineRenderer.tag = "Line";
        LineRenderer.positionCount = 2;
        LineRenderer.sortingOrder = i;
        LineRenderer.material = material[a];
        LineRenderer.endWidth = Slider.value;
        LineRenderer.startWidth = Slider.value;
        LineRenderer.useWorldSpace = true;
        LineRenderer.numCapVertices = 50;
        i++;
    }

    public void OnButtonClick1()
    {
        a = 0;
    }
    public void OnButtonClick2()
    {
        a = 1;
    }
    public void OnButtonClick3()
    {
        a = 2;
    }
    public void OnButtonClick4()
    {
        allline = null;
        allline = GameObject.FindGameObjectsWithTag("Line");
        
        if (allline.Length > 0)
        {
            for (int j = 0;j < allline.Length; j++)
            {
                Destroy(allline[j]);
                i = 1;
            }
        }
    }
    public void OnButtonClick5()
    {
        StartCoroutine(ScreenShot());
    }
    

    public IEnumerator ScreenShot()
    {
        Canvas.SetActive(false);
        ScreenCapture.CaptureScreenshot("SomeLevel.png");
        yield return new WaitForSeconds(2);
        Canvas.SetActive(true);
    }
}
