using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapHandler : MonoBehaviour
{
    public Tilemap trapsTilemap;

    public void RemoveTile(Vector3 worldPosition)
    {
        Vector3Int cellPosition = trapsTilemap.WorldToCell(worldPosition);
        trapsTilemap.SetTile(cellPosition, null);
    }
}
