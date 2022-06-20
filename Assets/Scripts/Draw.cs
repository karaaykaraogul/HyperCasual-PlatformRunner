using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public GameObject brush;

    LineRenderer currentLineRenderer;

    Vector3 lastPos;
    
    private Texture3D texture;

    GameObject[] brushes;
    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();
    private Texture2D destinationTexture;
    Renderer paintableWallRenderer;
    float snapshotRate = 1f;
    float nextSnapshotTime = 0f;
    private bool isPerformingScreenGrab;
    int redCount;
    int whiteCount = 0;
    bool startedPainting = false;
    float paintPercentage = 0;

    void Start()
    {
        // Create a new Texture2D with the width and height of the screen, and cache it for reuse
        destinationTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        paintableWallRenderer = GameObject.FindGameObjectWithTag("Paintable").GetComponent<Renderer>();
    }

    void Update()
    {
        if(GameManager.Instance.State == GameState.Victory)
        {
            Drawing();
            
            if (Time.time >= nextSnapshotTime)
            {
                StartCoroutine(TakeSnapshot());
                nextSnapshotTime = Time.time + 1f / snapshotRate;
            }
        }
    }

    public int GetCurrentPercent()
    {
        if(paintPercentage != ((float) redCount/(float) whiteCount) * 100)
        {
            paintPercentage = ((float) redCount/(float) whiteCount) * 100;
        }
        
        if(paintPercentage >= 90)
        {
            paintPercentage = 100;
        }

        return (int) paintPercentage;
    }

    public IEnumerator TakeSnapshot()
    {
        //wait until frame end to take snapshot
        yield return frameEnd;
        
        redCount = 0;
        if(!startedPainting)
        {
            whiteCount = 0;
        }
        // Check whether the Camera that has just finished rendering is the one you want to take a screen grab from
        
        // Define the parameters for the ReadPixels operation
        Rect regionToReadFrom = new Rect(0, 0, Screen.width, Screen.height);
        int xPosToWriteTo = 0;
        int yPosToWriteTo = 0;
        bool updateMipMapsAutomatically = false;

        // Copy the pixels from the Camera's render target to the texture
        destinationTexture.ReadPixels(regionToReadFrom, xPosToWriteTo, yPosToWriteTo, updateMipMapsAutomatically);

        // Upload texture data to the GPU, so the GPU renders the updated texture
        // Note: This method is costly, and you should call it only when you need to
        // If you do not intend to render the updated texture, there is no need to call this method at this point
        destinationTexture.Apply();

        Color[] pixels = destinationTexture.GetPixels();
        foreach(var pixel in pixels)
        {
            if(ColorEqual(pixel, Color.red))
            {
                redCount++;
            }
            if(!startedPainting)
            {
                if(ColorEqual(pixel, paintableWallRenderer.material.color))
                {
                    whiteCount++;
                }
            }
        }
        // Reset the isPerformingScreenGrab state
        isPerformingScreenGrab = false;
    }

    bool ColorEqual(Color color1, Color color2)
    {
        float threshold = 0.1f; //Exact value should be found by trying different. Possibly different values for      different colors.
        return (Mathf.Abs(color1.r - color2.r) < threshold
        && Mathf.Abs(color1.g - color2.g) < threshold
        && Mathf.Abs(color1.b - color2.b) < threshold);
    }

    void Drawing()
    {
        Ray rayCheck = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(rayCheck, out RaycastHit rayCheckHit))
        {
            if(rayCheckHit.transform.tag == "Paintable")
            {
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    CreateBrush();
                    isPerformingScreenGrab = true;
                    startedPainting = true;
                }
                if(Input.GetKey(KeyCode.Mouse0))
                {
                    
                    Vector3 mousePos = rayCheckHit.point;
                    if(mousePos != lastPos)
                    {
                        AddAPoint(mousePos);
                        lastPos = mousePos;
                    }
                    isPerformingScreenGrab = true;
                    startedPainting = true;
                }
            }
        }
        else
        {
            currentLineRenderer = null;
        }
    }

    void CreateBrush()
    {
        Vector3 mousePos;
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mousePos = raycastHit.point;
            currentLineRenderer.SetPosition(0, mousePos);
            currentLineRenderer.SetPosition(1, mousePos);
        }
    }

    void AddAPoint(Vector3 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }
}
