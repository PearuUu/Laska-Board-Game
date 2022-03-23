using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pawn : MonoBehaviour
{
    #region Variables

    [SerializeField] GridGenerator gridGenerator;

    private Vector3 mOffset;

    private float mZCoord;

    Camera cam;

    [SerializeField] Vector3 rayDirection;

    [SerializeField] int pieceIndex;

    public bool raycast;

    

    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        gridGenerator = GameObject.FindObjectOfType<GridGenerator>();
        GetPieceIndex();
        AddToGrid();

        cam = GameObject.Find("Camera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        
         if (raycast)
        {
            DiagonalLength();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    void OnMouseDown()
    {

        mZCoord = cam.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        RemoveFromGrid();
        Cursor.visible = false;

    }

    private void OnMouseUp()
    {

        SnapToGrid();
        AddToGrid();
        Cursor.visible = true;

    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

    #endregion

    #region Grid

    void AddToGrid()
    {
        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedZ = Mathf.RoundToInt(transform.position.z);

        gridGenerator.grid[roundedZ * gridGenerator.h + roundedX] = pieceIndex;
    }

    void RemoveFromGrid()
    {
        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedZ = Mathf.RoundToInt(transform.position.z);

        gridGenerator.grid[roundedZ * gridGenerator.h + roundedX] = 0;
    }

    void SnapToGrid()
    {
        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedZ = Mathf.RoundToInt(transform.position.z);
        transform.position = new Vector3(roundedX, transform.position.y, roundedZ);
    }

    #endregion

    #region other

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return cam.ScreenToWorldPoint(mousePoint);

    }

    void GetPieceIndex()
    {
        switch (transform.tag)
        {
            case "LightPawn":
                pieceIndex = 1;
                break;

            case "LightQueen":
                pieceIndex = 2;
                break;

            case "DarkPawn":
                pieceIndex = 3;
                break;

            case "DarkQueen":
                pieceIndex = 4;
                break;
        }
    }

    #endregion

    #region Moves

    void DiagonalLength()
    {
        RaycastHit hit1;
        RaycastHit hit2;

        LayerMask mask = LayerMask.GetMask("Border");

        if (Physics.Raycast(transform.position, transform.TransformDirection(rayDirection), out hit1, Mathf.Infinity, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(rayDirection) * hit1.distance, Color.red);
            Debug.Log("Hit name: " +  hit1.transform.name);
            Debug.Log("Hit Point: " + hit1.transform.position);
            Debug.Log("Hit distance: " + Mathf.RoundToInt(hit1.distance) /*hit1.distance*/);

        }

    }

    #endregion


}
