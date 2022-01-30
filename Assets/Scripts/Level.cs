using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Grid mapGrid;
    public Tile TileBlank;
    public Tile TileWhite;
    public Tile TileBlack;
    public Tile TilePink;
    public Tile TileWhiteTouched;
    public Tile TileBlackTouched;
    // Start is called before the first frame update
    public int MapSizeX=22, MapSizeY = 12;
    private short[,] AutoMap;
    private bool[,] WalkableMap;
    private int[] w = {6,10};
    private int[] b = {6,11};
    private GridLayout gridW;
    private Tilemap tmapW;
    private GridLayout gridB;
    private Tilemap tmapB;
    public Text Remain;
    private int BlockNum;

    public GameObject PlayerW;
    public GameObject PlayerB;
    
    void shuffle(int[] r){
        for (int i = -1; ++i < r.Length;) {
            int temp = r[i];
            int randomIndex = Random.Range(i, r.Length);
            r[i] = r[randomIndex];
            r[randomIndex] = temp;
        }
    }

    bool CheckSurrounding(int[] w, int[] b){            //Check if have been position, return true if have not been
        if(w[0]<MapSizeY-1 && AutoMap[w[0]+1,w[1]] == 0){
            return true;
        }if(w[0]>0 && AutoMap[w[0]-1,w[1]] == 0){
            return true;
        }if(w[1]<MapSizeX-1 && AutoMap[w[0],w[1]+1] == 0){
            return true;
        }if(w[1]>0 && AutoMap[w[0],w[1]-1] == 0){
            return true;
        }if(b[0]<MapSizeY-1 && AutoMap[b[0]+1,b[1]] == 0){
            return true;
        }if(b[0]>0 && AutoMap[b[0]-1,b[1]] == 0){
            return true;
        }if(b[1]<MapSizeX-1 && AutoMap[b[0],b[1]+1] == 0){
            return true;
        }if(b[1]>0 && AutoMap[b[0],b[1]-1] == 0){
            return true;
        }
        return false;
    }
    
    void MapGeneration(){
        int[] RandomArray1 = {0,1,2,3,4};       //0=dont move, 1,2,3,4 = r,d,l,u
        int[] RandomArray2 = {4,3,2,1,0};
        AutoMap = new short[MapSizeY,MapSizeX];
        WalkableMap = new bool[MapSizeY,MapSizeX];
        AutoMap[6,10] = 2;
        AutoMap[6,11] = 1;
        WalkableMap[6,10] = true;
        WalkableMap[6,11] = true;
        int limit = 1000;
        while(limit>0){
            limit--;
            shuffle(RandomArray1);
            shuffle(RandomArray2);
            bool finished = false;
            foreach(int i in RandomArray1){
                if(i==0){
                    foreach(int j in RandomArray2){
                        if(i==0){
                            continue;
                        }else if(i==1){
                            if(b[1]<MapSizeX-1 && (AutoMap[b[0],b[1]+1]==0 || AutoMap[b[0],b[1]+1]==1)){
                                if(w[1]==MapSizeX-1 || AutoMap[w[0],w[1]+1]==0 || AutoMap[w[0],w[1]+1]==1){
                                    b[1]++;
                                    if(CheckSurrounding(w,b)){
                                        if(w[1]!=MapSizeX-1){
                                            AutoMap[w[0],w[1]+1] = 1;
                                        }
                                        WalkableMap[b[0],b[1]] = true;
                                        AutoMap[b[0],b[1]] = 1;
                                        finished = true;
                                        break;
                                    }
                                    b[1]--;
                                }
                            }
                        }else if(i==2){
                            if(b[0]>0 && (AutoMap[b[0]-1,b[1]]==0 || AutoMap[b[0]-1,b[1]]==1)){
                                if(w[0]==0 || AutoMap[w[0]-1,w[1]]==0 || AutoMap[w[0]-1,w[1]]==1){
                                    b[0]--;
                                    if(CheckSurrounding(w,b)){
                                        if(w[0]!=0){
                                            AutoMap[w[0]-1,w[1]] = 1;
                                        }
                                        WalkableMap[b[0],b[1]] = true;
                                        AutoMap[b[0],b[1]] = 1;
                                        finished = true;
                                        break;
                                    }
                                    b[0]++;
                                }
                            }
                        }else if(i==3){
                            if(b[1]>0 && (AutoMap[b[0],b[1]-1]==0 || AutoMap[b[0],b[1]-1]==1)){
                                if(w[1]==0 || AutoMap[w[0],w[1]-1]==0 || AutoMap[w[0],w[1]-1]==1){
                                    b[1]--;
                                    if(CheckSurrounding(w,b)){
                                        if(w[1]!=0){
                                            AutoMap[w[0],w[1]-1] = 1;
                                        }
                                        WalkableMap[b[0],b[1]] = true;
                                        AutoMap[b[0],b[1]] = 1;
                                        finished = true;
                                        break;
                                    }
                                    b[1]++;
                                }
                            }
                        }else if(i==4){
                            if(b[0]<MapSizeY-1 && (AutoMap[b[0]+1,b[1]]==0 || AutoMap[b[0]+1,b[1]]==1)){
                                if(w[0]==MapSizeY-1 || AutoMap[w[0]+1,w[1]]==0 || AutoMap[w[0]+1,w[1]]==1){
                                    b[0]++;
                                    if(CheckSurrounding(w,b)){
                                        if(w[0]!=MapSizeY-1){
                                            AutoMap[w[0]+1,w[1]] = 1;
                                        }
                                        WalkableMap[b[0],b[1]] = true;
                                        AutoMap[b[0],b[1]] = 1;
                                        finished = true;
                                        break;
                                    }
                                    b[0]--;
                                }
                            }
                        }
                    }
                }else if(i==1){
                    if(w[1]<MapSizeX-1 && (AutoMap[w[0],w[1]+1]==0 || AutoMap[w[0],w[1]+1]==2)){
                        foreach(int j in RandomArray2){
                            if(j==0){
                                if(b[1]==MapSizeX-1 || AutoMap[b[0],b[1]+1]==0 || AutoMap[b[0],b[1]+1]==2){
                                    w[1]++;
                                    if(CheckSurrounding(w,b)){
                                        if(b[1]!=MapSizeX-1)
                                            AutoMap[b[0],b[1]+1] = 2;
                                        WalkableMap[w[0],w[1]] = true;
                                        AutoMap[w[0],w[1]] = 2;
                                        finished = true;
                                        break;
                                    }
                                    w[1]--;
                                }
                            }else if(j==3){
                                if(b[1]>0 && (AutoMap[b[0],b[1]-1]==0 || AutoMap[b[0],b[1]-1]==1)){
                                    w[1]++;
                                    b[1]--;
                                    if(CheckSurrounding(w,b)){
                                        WalkableMap[w[0],w[1]] = true;
                                        WalkableMap[b[0],b[1]] = true;
                                        AutoMap[w[0],w[1]] = 2;
                                        AutoMap[b[0],b[1]] = 1;
                                        finished = true;
                                        break;
                                    }
                                    w[1]--;
                                    b[1]++;
                                }
                            }
                        }
                    }
                }else if(i==2){
                    if(w[0]>0 && (AutoMap[w[0]-1,w[1]]==0 || AutoMap[w[0]-1,w[1]]==2)){
                        foreach(int j in RandomArray2){
                            if(j==0){
                                if(b[0]==0 || AutoMap[b[0]-1,b[1]]==0 || AutoMap[b[0]-1,b[1]]==2){
                                    w[0]--;
                                    if(CheckSurrounding(w,b)){
                                        if(b[0]!=0)
                                            AutoMap[b[0]-1,b[1]] = 2;
                                        WalkableMap[w[0],w[1]] = true;
                                        AutoMap[w[0],w[1]] = 2;
                                        finished = true;
                                        break;
                                    }
                                    w[0]++;
                                }
                            }else if(j==4){
                                if(b[0]<MapSizeY-1 && (AutoMap[b[0]+1,b[1]]==0 || AutoMap[b[0]+1,b[1]]==1)){
                                    w[0]--;
                                    b[0]++;
                                    if(CheckSurrounding(w,b)){
                                        WalkableMap[w[0],w[1]] = true;
                                        WalkableMap[b[0],b[1]] = true;
                                        AutoMap[w[0],w[1]] = 2;
                                        AutoMap[b[0],b[1]] = 1;
                                        finished = true;
                                        break;
                                    }
                                    w[0]++;
                                    b[0]--;
                                }
                            }
                        }
                    }
                }else if(i==3){
                    if(w[1]>0 && (AutoMap[w[0],w[1]-1]==0 || AutoMap[w[0],w[1]-1]==2)){
                        foreach(int j in RandomArray2){
                            if(j==0){
                                if(b[1]==0 || AutoMap[b[0],b[1]-1]==0 || AutoMap[b[0],b[1]-1]==2){
                                    w[1]--;
                                    if(CheckSurrounding(w,b)){
                                        if(b[1]!=0)
                                            AutoMap[b[0],b[1]-1] = 2;
                                        WalkableMap[w[0],w[1]] = true;
                                        AutoMap[w[0],w[1]] = 2;
                                        finished = true;
                                        break;
                                    }
                                    w[1]++;
                                }
                            }else if(j==1){
                                if(b[1]<MapSizeX-1 && (AutoMap[b[0],b[1]+1]==0 || AutoMap[b[0],b[1]+1]==1)){
                                    w[1]--;
                                    b[1]++;
                                    if(CheckSurrounding(w,b)){
                                        WalkableMap[w[0],w[1]] = true;
                                        WalkableMap[b[0],b[1]] = true;
                                        AutoMap[w[0],w[1]] = 2;
                                        AutoMap[b[0],b[1]] = 1;
                                        finished = true;
                                        break;
                                    }
                                    w[1]++;
                                    b[1]--;
                                }
                            }
                        }
                    }
                }else if(i==4){
                    if(w[0]<MapSizeY-1 && (AutoMap[w[0]+1,w[1]]==0 || AutoMap[w[0]+1,w[1]]==2)){
                        foreach(int j in RandomArray2){
                            if(j==0){
                                if(b[0]==MapSizeY-1 || AutoMap[b[0]+1,b[1]]==0 || AutoMap[b[0]+1,b[1]]==2){
                                    w[0]++;
                                    if(CheckSurrounding(w,b)){
                                        if(b[0]!=MapSizeY-1)
                                            AutoMap[b[0]+1,b[1]] = 2;
                                        WalkableMap[w[0],w[1]] = true;
                                        AutoMap[w[0],w[1]] = 2;
                                        finished = true;
                                        break;
                                    }
                                    w[0]--;
                                }
                            }else if(j==2){
                                if(b[0]>0 && (AutoMap[b[0]-1,b[1]]==0 || AutoMap[b[0]-1,b[1]]==1)){
                                    w[0]++;
                                    b[0]--;
                                    if(CheckSurrounding(w,b)){
                                        WalkableMap[w[0],w[1]] = true;
                                        WalkableMap[b[0],b[1]] = true;
                                        AutoMap[w[0],w[1]] = 2;
                                        AutoMap[b[0],b[1]] = 1;
                                        finished = true;
                                        break;
                                    }
                                    w[0]--;
                                    b[0]++;
                                }
                            }
                        }
                    }
                }
                if(finished){
                    break;
                }
            }
            if(!finished){
                break;
            }
        }
        // print(limit);
        for(int i = -1; ++i < MapSizeY;){
            for(int j = -1; ++j < MapSizeX;){
                if(WalkableMap[i,j])
                    BlockNum ++;
                if(AutoMap[i,j] == 0){
                    AutoMap[i,j] = 3;
                }
            }
        }
        
    }
    void Start()
    {
        MapGeneration();

        // PlayerW = GameObject.Find("YangPlayer");
        // PlayerB = GameObject.Find("YinPlayer");
        PlayerW.transform.position = new Vector3(w[1]-10.5f,w[0]-5.5f,0);
        PlayerB.transform.position = new Vector3(b[1]-10.5f,b[0]-5.5f,0);
        var TileTypeNames = new Dictionary<short, string>
        {
            {0, "TileBlank"}, {1, "TileWhite"}, {2, "TileBlack"}, {3, "TilePink"}
        };
        var TileTypes = new Dictionary<short, Tile>
        {
            {0, TileBlank}, {1, TileWhite}, {2, TileBlack}, {3, TilePink}
        };
        // var TileScripts = new Dictionary<short, type>
        // {
        //     {0, ""}, {1, ""}, {2, ""}, {3, Spikes}
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

        // for(int y = -1; ++y<MapSizeY;){
        //     for(int x=-1; ++x<MapSizeX;){
        //         TilesTypeLis[LevelMap[y,x]].Add(TileTypes[LevelMap[y,x]]);
        //         TilesPosLis[LevelMap[y,x]].Add(new Vector3Int(x,y,0));
        //     }
        // }

        for(int y = -1; ++y<MapSizeY;){
            for(int x=-1; ++x<MapSizeX;){
                TilesTypeLis[AutoMap[y,x]].Add(TileTypes[AutoMap[y,x]]);
                TilesPosLis[AutoMap[y,x]].Add(new Vector3Int(x,y,0));
            }
        }

        // BlockNum += TilesTypeLis[1].Count;      //Count total blocks
        // BlockNum += TilesTypeLis[2].Count;


        for(short TileType=(short)TileTypes.Count; TileType-->0;){

            // Add one type of tiles at a time
            var tw = new GameObject(TileTypeNames[TileType]);
            var tw_tm = tw.AddComponent<Tilemap>();
            var tw_tr = tw.AddComponent<TilemapRenderer>();

            tw_tm.tileAnchor = new Vector3(.5f,.5f,0);
            tw.transform.SetParent(mapGrid.transform);
            tw.transform.position = new Vector3(-11f,-6f,TileType);
            // tw_tm.SetTile(new Vector3Int(0,0,0),TileWhite);
            tw_tm.SetTiles(TilesPosLis[TileType].ToArray(), TilesTypeLis[TileType].ToArray());
            var tw_tc = tw.AddComponent<TilemapCollider2D>();
            var tw_rb = tw.AddComponent<Rigidbody2D>();
            tw_rb.bodyType=RigidbodyType2D.Static;
            var tw_cc = tw.AddComponent<CompositeCollider2D>();
            tw_tc.usedByComposite = true;

            if(TileTypeNames[TileType] == "TilePink"){
                tw.AddComponent<Spikes>();
                tw_cc.isTrigger = true;
            }
            if(TileTypeNames[TileType] == "TileWhite"){
                gridW = tw_tm.layoutGrid;
                tmapW = tw_tm;
            }
            if(TileTypeNames[TileType] == "TileBlack"){
                gridB = tw_tm.layoutGrid;
                tmapB = tw_tm;
            }
            // Add Script Here?
        }

        for(int i=-1;++i<MapSizeY;){
            for(int j=-1;++j<MapSizeY;){
                if(AutoMap[i,j]!=3 && !WalkableMap[i,j]){
                    if(AutoMap[i,j]==1){
                        tmapW.SetTile(new Vector3Int(i,j,0), TileWhiteTouched);
                    }
                    if(AutoMap[i,j]==2){
                        tmapB.SetTile(new Vector3Int(i,j,0), TileBlackTouched);
                    }
                }
            }
        }





        
    }

    // Update is called once per frame
    void Update()
    {
        var currentCellPos = gridB.WorldToCell(PlayerW.transform.position);
        currentCellPos[0]+=11;
        currentCellPos[1]+=6;
        if(AutoMap[currentCellPos[1],currentCellPos[0]]==1 || AutoMap[currentCellPos[1],currentCellPos[0]]==2){
            AutoMap[currentCellPos[1],currentCellPos[0]] = -1;
            // var currentCell = tmapW.GetTile(currentCellPos);
            tmapB.SetTile(currentCellPos, TileBlackTouched);
            BlockNum --;
        }
        currentCellPos = gridW.WorldToCell(PlayerB.transform.position);
        currentCellPos[0]+=11;
        currentCellPos[1]+=6;
        if(AutoMap[currentCellPos[1],currentCellPos[0]]==1 || AutoMap[currentCellPos[1],currentCellPos[0]]==2){
            AutoMap[currentCellPos[1],currentCellPos[0]] = -1;
            var currentCell = tmapB.GetTile(currentCellPos);
            tmapW.SetTile(currentCellPos, TileWhiteTouched);
            BlockNum --;
        }
        Remain.text = "Remaining : " + BlockNum;
    }
}
