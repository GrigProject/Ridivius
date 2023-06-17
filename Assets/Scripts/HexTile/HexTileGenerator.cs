using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexTileGenerator : MonoBehaviour
{
    public Vector2Int size;

    [Range(1f, 50f)]
    public float zoom = 20;
    [Range(1f, 10f)]
    public float intensivity = 1;

    [Header("Surface")]
    public Tilemap TilemapSurface;
    public HexagonalRuleTile TileOre;

    [Header("Terrain")]
    public Tilemap TilemapTerrain;
    public HexagonalRuleTile TerrainRock;
    public HexagonalRuleTile TerrainGrass;
    public HexagonalRuleTile TerrainRedSand;
    public HexagonalRuleTile TileFluid;

    [Header("Underground")]
    public Tilemap TilemapUnderground;
    public HexagonalRuleTile TileUnderground;

    public void generationTiles()
    {
        TilemapSurface.ClearAllTiles();
        TilemapTerrain.ClearAllTiles();
        TilemapUnderground.ClearAllTiles();

        var offsetTerrain = new Vector2(Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        var offsetTemperature = new Vector2(Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));

        for (int x = size.x / 2 - size.x; x < size.x / 2; x++)
        {
            for (int y = size.y / 2 - size.y; y < size.y / 2; y++)
            {
                TilemapUnderground.SetTile(new Vector3Int(x, y, 0), TileUnderground);

                var terrain = Mathf.PerlinNoise((x + offsetTerrain.x) / zoom, (y + offsetTerrain.y) / zoom) * intensivity;
                var temperature = Mathf.PerlinNoise((x + offsetTemperature.x) / zoom, (y + offsetTemperature.y) / zoom);

                if (terrain > Mathf.NegativeInfinity)
                {
                    TilemapTerrain.SetTile(new Vector3Int(x, y, 0), TileFluid);
                }

                if (terrain > 0.4f)
                {
                    if (temperature < 0.5f)
                        TilemapTerrain.SetTile(new Vector3Int(x, y, 0), TerrainGrass);
                    else
                        TilemapTerrain.SetTile(new Vector3Int(x, y, 0), TerrainRedSand);
                }

                if (terrain > 0.8f)
                {
                    TilemapTerrain.SetTile(new Vector3Int(x, y, 0), TerrainRock);

                    if (Random.Range(0, 10) > 5)
                        TilemapSurface.SetTile(new Vector3Int(x, y, 0), TileOre);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        generationTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
