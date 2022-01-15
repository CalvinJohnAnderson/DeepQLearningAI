using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public GameObject ReplaceSelf(GameObject replacement, Transform parent)
    {
        GameObject newTile = Instantiate(replacement, parent);
        newTile.transform.position = transform.position;
        return newTile;
    }
}
