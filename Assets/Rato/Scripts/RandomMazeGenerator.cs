using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using System.Linq;

public class RandomMazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell Cell;
    [SerializeField] private int width, height;
    [SerializeField] private Vector3 RatoPositionNextScene;
    [SerializeField] private string NewScene;
    private MazeCell[,] maze;
    private List<int[]> edges;
    private List<MazeCell[]> DFSedges;
    private int[,] sets;
    public MazeCell Entrance {get; set;}
    public MazeCell Exit {get; set;}
    
    void Awake(){
        InitializeMaze();
        GenerateMaze();
    }
    
    void InitializeMaze(){
        this.width *= (GameController.Instance.difficulty + 1);
        this.height *= (GameController.Instance.difficulty + 1);

        this.maze = new MazeCell[this.width, this.height];
        for(int x = 0; x < this.width; x++){
            for(int y = 0; y < this.height; y++){
                this.maze[x,y] = Instantiate(this.Cell, new Vector3(y * 3, -x * 3, 0), Quaternion.identity);
                this.maze[x,y].color = "white";
                this.maze[x,y].parent = null;
            }
        }
    }

    void MakeEdges(){
        this.edges = new List<int[]>();
        this.DFSedges = new List<MazeCell[]>();
        for(int x = 0; x < this.width; x++){
            for(int y = 0; y < this.height; y++){
                if(x - 1 >= 0 && !ExistEdge(PositionToNumber(x, y), PositionToNumber(x - 1, y))){
                    int[] e = {PositionToNumber(x, y), PositionToNumber(x - 1, y)};
                    edges.Add(e);
                }

                if(y - 1 >= 0 && !ExistEdge(PositionToNumber(x, y), PositionToNumber(x, y - 1))){
                    int[] e = {PositionToNumber(x, y), PositionToNumber(x, y - 1)};
                    edges.Add(e);
                }

                if(x + 1 < this.width && !ExistEdge(PositionToNumber(x, y), PositionToNumber(x + 1, y))){
                    int[] e = {PositionToNumber(x, y), PositionToNumber(x + 1, y)};
                    edges.Add(e);
                }
                
                if(y + 1 < this.width && !ExistEdge(PositionToNumber(x, y), PositionToNumber(x, y + 1))){
                    int[] e = {PositionToNumber(x, y), PositionToNumber(x, y + 1)};
                    edges.Add(e);
                }
            }
        }
    }

    bool ExistEdge(int v1, int v2){
        if(this.edges != null){
            if(this.edges.Any(x => x[0] == v1 && x[1] == v2)){
                return true;
            }else if(this.edges.Any(x => x[0] == v2 && x[1] == v1)){
                return true;
            }else{
                return false;
            }
        }else{
            return false;
        }
        
    }

    bool ExistDFSedge(MazeCell v1, MazeCell v2){
        if(this.DFSedges != null){
            if(this.DFSedges.Any(x => x[0] == v1 && x[1] == v2)){
                return true;
            }else if(this.DFSedges.Any(x => x[0] == v2 && x[1] == v1)){
                return true;
            }else{
                return false;
            }
        }else{
            return false;
        }
        
    }

    int PositionToNumber(int x, int y){
        return (x + y + (width - 1) * x);
    }

    int[] NumberToPosition(int n){
        int[] position = {n / this.width, n % this.width};
        return position; 
    }

    void Shuffle(List<int[]> list){
        if(list != null){
            for(var i = 0; i < list.Count; i++){
                int k = Random.Range(0, list.Count - 1);
                int[] v = list[k];
                list[k] = list[i];
                list[i] = v;
            }
        }
    }

    void Shuffle(List<MazeCell> list){
        if(list != null){
            for(var i = 0; i < list.Count; i++){
                int k = Random.Range(0, list.Count - 1);
                MazeCell v = list[k];
                list[k] = list[i];
                list[i] = v;
            }
        }
    }

    void GenerateMaze(){
        this.sets = new int[width, height]; 
        for(int x = 0; x < this.width; x++){
            for(int y = 0; y < this.height; y++){
                this.sets[x, y] = PositionToNumber(x, y);
            }
        }

        MakeEdges();
        Shuffle(this.edges);

        while(!Finished()){
            if(this.sets[NumberToPosition(this.edges[0][0])[0],NumberToPosition(this.edges[0][0])[1]] != this.sets[NumberToPosition(this.edges[0][1])[0],NumberToPosition(this.edges[0][1])[1]]){
                RemoveWalls(this.edges[0][0], this.edges[0][1]);
                int set = this.sets[NumberToPosition(this.edges[0][1])[0],NumberToPosition(this.edges[0][1])[1]];
                for(int x = 0; x < this.width; x++){
                    for(int y = 0; y < this.height; y++){
                        if(this.sets[x,y] == set){
                            this.sets[x,y] = this.sets[NumberToPosition(this.edges[0][0])[0],NumberToPosition(this.edges[0][0])[1]];
                        }
                    }
                }

                MazeCell[] e = {this.maze[NumberToPosition(this.edges[0][0])[0],NumberToPosition(this.edges[0][0])[1]], this.maze[NumberToPosition(this.edges[0][1])[0], NumberToPosition(this.edges[0][1])[1]]};
                this.DFSedges.Add(e);         
            }


            this.edges.RemoveAt(0);
        }

        SetIO();
        DFS();
    }

    bool Finished(){
        for(int x = 0; x < this.width; x++){
            for(int y = 0; y < this.height; y++){
                if(this.sets[x,y] != this.sets[0,0]){
                    return false;
                }
            }
        }

        return true;
    }

    void RemoveWalls(int w1, int w2){
        if(w1 < w2){
            if(w1 - w2 == -1){
                this.maze[NumberToPosition(w1)[0], NumberToPosition(w1)[1]].RemoveWall("right");
                this.maze[NumberToPosition(w2)[0], NumberToPosition(w2)[1]].RemoveWall("left");
            }else if(w1 - w2 == -(this.width)){
                this.maze[NumberToPosition(w1)[0], NumberToPosition(w1)[1]].RemoveWall("bottom");
                this.maze[NumberToPosition(w2)[0], NumberToPosition(w2)[1]].RemoveWall("top");
            }
        }else if(w1 > w2){
            if(w1 - w2 == 1){
                this.maze[NumberToPosition(w1)[0], NumberToPosition(w1)[1]].RemoveWall("left");
                this.maze[NumberToPosition(w2)[0], NumberToPosition(w2)[1]].RemoveWall("right");
            }else if(w1 - w2 == this.width){
                this.maze[NumberToPosition(w1)[0], NumberToPosition(w1)[1]].RemoveWall("top");
                this.maze[NumberToPosition(w2)[0], NumberToPosition(w2)[1]].RemoveWall("bottom");
            }
        }
    }

    void SetIO(){
        List<MazeCell> topBorders = new List<MazeCell>();
        for(int x = 0; x < this.width; x++){
            for(int y = 0; y < this.height; y++){
                if(x == 0){
                    topBorders.Add(this.maze[x,y]);
                }
            }
        }

        Shuffle(topBorders);
        
        this.Entrance = maze[this.width - 1, this.height - 1];
        this.Entrance.IsEntrance(true);

        this.Exit = topBorders[0];
        this.Exit.IsExit(true);
        this.Exit.SetExit(this.NewScene, this.RatoPositionNextScene);
    }

    void DFS(){
        for(int x = 0; x < this.width; x++){
            for(int y = 0; y < this.height; y++){
                if(ExistDFSedge(this.Entrance, this.maze[x,y]) && this.maze[x,y].color == "white"){
                    this.maze[x,y].parent = this.Entrance;
                    DFSVisita(this.maze[x,y]);
                }
            }
        }
    }

    void DFSVisita(MazeCell cell){
        cell.color = "gray";

        for(int x = 0; x < this.width; x++){
            for(int y = 0; y < this.height; y++){
                if(ExistDFSedge(cell, this.maze[x,y]) && this.maze[x,y].color == "white"){
                    this.maze[x,y].parent = cell;
                    if(this.maze[x,y] != this.Exit){
                        DFSVisita(this.maze[x,y]);
                    }else{
                        FinishDFS(this.maze[x,y]);
                    }
                    
                }
            }
        }

        cell.color = "black";
    }

    void FinishDFS(MazeCell cell){
        do{
            cell.Path();
            cell = cell.parent;
        }while(cell != this.Entrance);
        this.Entrance.Path();
    }
}
