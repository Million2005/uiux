using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TileType
{
    Empty,
    Arrow,
    Blocker,
    Revealed
}
public  enum ArrowDir
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}

public class Tile : MonoBehaviour
{
    public TileType tileType = TileType.Arrow;
    public ArrowDir arrowDir = ArrowDir.Up;
    public int x;
    public int y;
    Image img;
    SpriteRenderer spriteRenderer;
    // chi dinh 4 sprite cho 4 huong
    public Sprite[] arrowSprites;
    public Sprite blockerSprite;
    public Sprite emptySprite;

    public Action<Tile> OnTapped;

    private void Awake()
    {
        img = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    public void Init(int x, int y ,TileType type,ArrowDir dir)
    {
        this.x = x;
        this.y = y;
        tileType = type;
        arrowDir = dir;
        RefreshVisual();
    }
    public void RefreshVisual()
    {
        switch (tileType)
        {
            case TileType.Empty:
                if (img != null) { img.sprite = emptySprite; img.color = new Color(1,1,1,0.0f); }
                if (spriteRenderer != null) { spriteRenderer.sprite = emptySprite; spriteRenderer.color = new Color(1,1,1,0.0f); }
                break;
            case TileType.Revealed:
                if (img != null) { img.sprite = emptySprite; img.color = new Color(1, 1, 1, 0.0f); }
                if (spriteRenderer != null) { spriteRenderer.sprite = emptySprite; spriteRenderer.color = new Color(1,1,1,0.0f); }
                break;
            case TileType.Blocker:
                if (img != null) { img.sprite = blockerSprite; img.color = Color.white; }
                if (spriteRenderer != null) { spriteRenderer.sprite = blockerSprite; spriteRenderer.color = Color.white; }
                break;
            case TileType.Arrow:
                if(arrowSprites != null && arrowSprites.Length >= 4 && arrowSprites[(int)arrowDir]!= null)
                {
                    if (img != null) { img.sprite = arrowSprites[(int)arrowDir]; img.color= Color.white; }
                    if (spriteRenderer != null) { spriteRenderer.sprite = arrowSprites[(int)arrowDir]; spriteRenderer.color = Color.white; }
                    transform.localEulerAngles = Vector3.zero;
                }
                else
                {
                    // du phong: 
                    if (img != null) { img.sprite = emptySprite; img.color = Color.white; }
                    if (spriteRenderer != null) { spriteRenderer.sprite = emptySprite; spriteRenderer.color = Color.white; }
                    transform.localEulerAngles = new Vector3(0, 0, -((int)arrowDir * 90));
                }
                break;

        }
    }
    public void Ontap()
    {
        OnTapped?.Invoke(this);
    }
    private void OnMouseDown()
    {
        // world-space click support (requires a Collider/Collider2D on the tile prefab)
        OnTapped?.Invoke(this);
    }
}
