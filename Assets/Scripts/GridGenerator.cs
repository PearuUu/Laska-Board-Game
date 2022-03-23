using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private static int width, height;
    public int[] grid;
    public int w, h, offset;

    [SerializeField] Transform tile1,tile2,tile3,pawn1,pawn2;

    [SerializeField] Color lightColor, darkColor;

    // Start is called before the first frame update
    void Start()
    {
        width = w;
        height = h;
        grid = new int[width * height];
        //GenerateGridIndex();
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateColor();
    }

    void GenerateGrid()
    {
        for(int i = 0; i<height; i++)
        {

            Instantiate(tile3, new Vector3(height, 0.5f, i), Quaternion.identity, gameObject.transform);
            Instantiate(tile3, new Vector3(-1, 0.5f, i), Quaternion.identity, gameObject.transform);
            Instantiate(tile3, new Vector3(i, 0.5f, -1), Quaternion.identity, gameObject.transform);
            Instantiate(tile3, new Vector3(i, 0.5f, height), Quaternion.identity, gameObject.transform);

            for (int j = 0; j < width; j++)
           {
                bool isLightTile = (i +j) % 2 != 0;
               
                
               
                Renderer tile1Renderer = tile1.GetComponent<Renderer>();
                Renderer tile2Renderer = tile2.GetComponent<Renderer>();

                if (isLightTile)
                {
                    tile1Renderer.sharedMaterial.SetColor("_BaseColor", lightColor);
                    Instantiate(tile1, new Vector3(j, 0, i), Quaternion.identity, gameObject.transform);
                }
                else
                {
                    tile2Renderer.sharedMaterial.SetColor("_BaseColor", darkColor);
                    Instantiate(tile2, new Vector3(j, 0, i), Quaternion.identity, gameObject.transform);
                    SpawnPawns(i, j);
                }
           }

            Instantiate(tile3, new Vector3(-1, 0.5f, height), Quaternion.identity, gameObject.transform);
            Instantiate(tile3, new Vector3(width, 0.5f, height), Quaternion.identity, gameObject.transform);
            Instantiate(tile3, new Vector3(-1, 0.5f, -1), Quaternion.identity, gameObject.transform);
            Instantiate(tile3, new Vector3(width, 0.5f, -1), Quaternion.identity, gameObject.transform);

        }

    }

    void SpawnPawns(int i, float j)
    {
        if (i < 3)
        {
            Instantiate(pawn1, new Vector3(j, 0.55f, i), Quaternion.identity, gameObject.transform);
        }
        else if (i >= height - 3)
        {
            Instantiate(pawn2, new Vector3(j, 0.55f, i), Quaternion.identity, gameObject.transform);
        }
        
    }

    void UpdateColor()
    {
        GameObject[] lightTiles = GameObject.FindGameObjectsWithTag("LightTIle");
        GameObject[] darkTiles = GameObject.FindGameObjectsWithTag("DarkTile");

        foreach(GameObject lightTile in lightTiles)
        {
            lightTile.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor", lightColor);
        }

        foreach (GameObject darkTile in darkTiles)
        {
            darkTile.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor", darkColor);
        }

    }

   void GenerateGridIndex()
    {
        for(int i = 0; i < height * width; i++)
        {
            grid[i] = i + 1;
        }
    }

    
}
