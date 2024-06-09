using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using System.Linq;

public class RandomMazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell Cell;
    [SerializeField] private int width, height;
    private MazeCell[,] maze;
    private List<int[]> edges;
    private int[,] sets;
    
    void Awake(){
        this.InitializeMaze();
        GenerateMaze();
    }
    
    void InitializeMaze(){
        this.maze = new MazeCell[this.width, this.height];
        for(int x = 0; x < this.width; x++){
            for(int y = 0; y < this.height; y++){
                this.maze[x,y] = Instantiate(this.Cell, new Vector3(y * 2, -x * 2, 0), Quaternion.identity);
            }
        }
    }

    void MakeEdges(){
        this.edges = new List<int[]>();
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

    int PositionToNumber(int x, int y){
        return (x + y + (width - 1) * x);
    }

    int[] NumberToPosition(int n){
        int[] position = {n / this.width, n % this.width};
        return position; 
    }

    void Shuffle(List<int[]> list){
        if(list != null){
            for(int i = 0; i < list.Count; i++){
                int k = Random.Range(0, list.Count - 1);
                int[] v = list[k];
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
            }

            this.edges.RemoveAt(0);
        }
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
}
