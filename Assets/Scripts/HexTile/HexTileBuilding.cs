using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class HexTileBuilding : MonoBehaviour
{
    Camera cam;
    private HexagonalRuleTile selectTile;
    private GameObject selectPrefab;
    private bool selectIsBelt;

    public Grid grid;
    public GameObject buildMenu;
    public GameObject buildButton;

    [Header("Build Menu")]

    public Tilemap tilemap;
    public HexagonalRuleTile hexagonalRuleTile0;
    public GameObject prefab0;
    public bool isBelt0;
    public HexagonalRuleTile hexagonalRuleTile1;
    public GameObject prefab1;
    public bool isBelt1;
    public HexagonalRuleTile hexagonalRuleTile2;
    public GameObject prefab2;
    public bool isBelt2;
    public HexagonalRuleTile hexagonalRuleTile3;
    public GameObject prefab3;
    public bool isBelt3;
    public HexagonalRuleTile hexagonalRuleTile4;
    public GameObject prefab4;
    public bool isBelt4;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        buildMenu.SetActive(false);
        buildButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && !buildMenu.activeInHierarchy)
        {
            buildMenu.SetActive(true);
            buildButton.SetActive(false);
        }
        else if (Input.GetKeyDown("e") && buildMenu.activeInHierarchy)
        {
            buildMenu.SetActive(false);
            buildButton.SetActive(true);
            selectTile = null;
            selectPrefab = null;
        }

        if (buildMenu.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!tilemap.HasTile(CellCoordinate()))
                {
                    tilemap.SetTile(CellCoordinate(), selectTile);

                    if (selectPrefab != null)
                        PlaceObject(selectPrefab);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (tilemap.HasTile(CellCoordinate()))
                {
                    tilemap.SetTile(CellCoordinate(), null);
                }
            }
        }
    }

    public void ButtonOnClick(int numberButton)
    {
        switch (numberButton)
        {
            case 0:
                selectTile = hexagonalRuleTile0;
                selectPrefab = prefab0;
                selectIsBelt = isBelt0;
                break;
            case 1:
                selectTile = hexagonalRuleTile1;
                selectPrefab = prefab1;
                selectIsBelt = isBelt1;
                break;
            case 2:
                selectTile = hexagonalRuleTile2;
                selectPrefab = prefab2;
                selectIsBelt = isBelt2;
                break;
            case 3:
                selectTile = hexagonalRuleTile3;
                selectPrefab = prefab3;
                selectIsBelt = isBelt3;
                break;
            case 4:
                selectTile = hexagonalRuleTile4;
                selectPrefab = prefab4;
                selectIsBelt = isBelt4;
                break;
            default:
                break;
        }
    }

    public Vector3Int CellCoordinate()
    {
        Vector3 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3Int position = grid.WorldToCell(mousePos);
        return position;
    }

    public void PlaceObject(GameObject prefab)
    {
        Vector3 position = grid.GetCellCenterWorld(CellCoordinate());
        Instantiate(prefab, position, Quaternion.identity);
    }

    
}
