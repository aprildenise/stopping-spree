
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SplatMapController : MonoBehaviour
{

    private int totalTiles = 50;

    public TextMeshProUGUI exposureText;
    public Tile splat;
    private Tilemap tilemap;
    private int splats = 0;
    private float exposure;

    public static SplatMapController instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        tilemap = GetComponent<Tilemap>();
    }

    public void SetTile(Vector3 position)
    {
        Vector3Int currentCell = tilemap.WorldToCell(position);
        if (!tilemap.HasTile(currentCell)) {
            tilemap.SetTile(currentCell, splat);
            splats++;
            exposure = Mathf.Round(splats / totalTiles);
            //Debug.Log(exposure);
            exposureText.SetText(exposure + "%");
        }
    }
}
