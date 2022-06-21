using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MousePainter : MonoBehaviour{
    public Camera cam;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;

    private GameObject paintableObject;
    private Renderer objectRenderer;
    private Texture2D texture2D;
    private Texture2D destinationTexture;
    Texture mainTex;
    RenderTexture renderTexture;
    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();
    
    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;
    int textureArea;
    int redCount;
    float paintPercentage;

    void Start()
    {
        paintableObject = FindObjectOfType<Paintable>().gameObject;
        objectRenderer = paintableObject.GetComponent<Renderer>();
        mainTex = objectRenderer.material.mainTexture;
        destinationTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        textureArea = mainTex.width*mainTex.height;
    }

    void Update(){

        bool click;
        click = mouseSingleClick ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0);

        if (click){
            Vector3 position = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f)){
                Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
                transform.position = hit.point;
                Paintable p = hit.collider.GetComponent<Paintable>();
                if(p != null){
                    PaintManager.instance.paint(p, hit.point, radius, hardness, strength, paintColor);
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            StartCoroutine(CalculatePaintedPercent());
        }
    }

    public IEnumerator CalculatePaintedPercent()
    {
        yield return frameEnd;
        Vector3 screenPos = cam.WorldToScreenPoint(paintableObject.transform.position);
        Rect regionToReadFrom = new Rect(0, 0, Screen.width, Screen.height);
        int xPosToWriteTo = 0;
        int yPosToWriteTo = 0;
        bool updateMipMapsAutomatically = false;

        // Copy the pixels from the Camera's render target to the texture
        destinationTexture.ReadPixels(regionToReadFrom, xPosToWriteTo, yPosToWriteTo, updateMipMapsAutomatically);

        destinationTexture.Apply();

        Color[] pixels = destinationTexture.GetPixels();
        redCount = 0;
        foreach(var pixel in pixels)
        {
            if(ColorEqual(pixel, Color.red))
            {
                redCount++;
            }
        }
    }

    public int GetPaintPercent()
    {
        paintPercentage = (((float) redCount/(float) textureArea) * 100)*2;
        return (int) paintPercentage;
    }

    bool ColorEqual(Color color1, Color color2)
    {
        float threshold = 0.3f; //Exact value should be found by trying different. Possibly different values for different colors.
        return (Mathf.Abs(color1.r - color2.r) < threshold
        && Mathf.Abs(color1.g - color2.g) < threshold
        && Mathf.Abs(color1.b - color2.b) < threshold);
    }
}
