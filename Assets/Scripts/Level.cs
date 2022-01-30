using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    public Grid mapGrid;
    public Tile TileBlank;
    public Tile TileWhite;
    public Tile TileBlack;
    public Tile TilePink;
    public Tile TileWhiteRule;
    public Tile TileBlackRule;
    // Start is called before the first frame update
    void Start()
    {
        var TileTypeNames = new Dictionary<short, string>
        {
            {0, "TileBlank"}, {1, "TileWhite"}, {2, "TileBlack"}, {3, "TilePink"},{4, "TileWhiteRule"},{5, "TileBlackRule"}
        };
        var TileTypes = new Dictionary<short, Tile>
        {
            {0, TileBlank}, {1, TileWhite}, {2, TileBlack}, {3, TilePink},{4, TileWhiteRule},{5, TileBlackRule}
        };
        // var TileScripts = new Dictionary<short, string>
        // {
        //     {0, ""}, {1, ""}, {2, ""}, {3, "Spikes.cs"}
        // };
        int MapSizeX=22, MapSizeY = 12;
        short[,] LevelMap =    {{1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2},
                                {1,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2},
                                {1,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2},
                                {1,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2},
                                {1,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2},
                                {1,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2},
                                {1,2,2,2,2,2,2,2,2,2,2,1,1,1,3,1,1,1,1,1,1,2},
                                {1,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2},
                                {1,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2},
                                {1,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2},
                                {1,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2},
                                {1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2}};

        List<Vector3Int>[] TilesPosLis = new List<Vector3Int>[TileTypes.Count];
        List<Tile>[] TilesTypeLis = new List<Tile>[TileTypes.Count];

        for(short TileType=-1; ++TileType<TileTypes.Count;){
            TilesPosLis[TileType] = new List<Vector3Int>();
            TilesTypeLis[TileType] = new List<Tile>();
        }

        for(int y = -1; ++y<MapSizeY;){
            for(int x=-1; ++x<MapSizeX;){
                TilesTypeLis[LevelMap[y,x]].Add(TileTypes[LevelMap[y,x]]);
                TilesPosLis[LevelMap[y,x]].Add(new Vector3Int(x,y,0));
            }
        }


        for(short TileType=-1; ++TileType<TileTypes.Count;){

            // Add one type of tiles at a time
            var tw = new GameObject(TileTypeNames[TileType]);
            var tw_tm = tw.AddComponent<Tilemap>();
            var tw_tr = tw.AddComponent<TilemapRenderer>();

            tw_tm.tileAnchor = new Vector3(.5f,.5f,0);
            tw.transform.SetParent(mapGrid.transform);
            tw.transform.position = new Vector3(-11f,-6f,0f);
            // tw_tm.SetTile(new Vector3Int(0,0,0),TileWhite);
            tw_tm.SetTiles(TilesPosLis[TileType].ToArray(), TilesTypeLis[TileType].ToArray());
            var tw_tc = tw.AddComponent<TilemapCollider2D>();
            var tw_rb = tw.AddComponent<Rigidbody2D>();
            tw_rb.bodyType=RigidbodyType2D.Static;
            var tw_cc = tw.AddComponent<CompositeCollider2D>();
            tw_tc.usedByComposite = true;


            // Add Script Here?
        }


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
