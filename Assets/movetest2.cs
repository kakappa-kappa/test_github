using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetest2 : MonoBehaviour {
    private Animator animator;
    public float speed = 1.0f;
    public int myColor = 0;
    public const int MAX = 6;

    private GameObject[] panels;
    private bool flag = true;

    //6*6の36マス
    private int[] mass = new int[MAX*MAX];
    private string[] color = new string[MAX*MAX];

    //スタートポジション,スタートエリア
    private int pos = MAX+1;
    private int startArea = 3;
    private int[] myArea;

    //テクスチャー変更したい
    private Texture[] textures = new Texture[5];

    //プレイヤー情報
    private string player(int playerNumber) {
        switch (playerNumber){
            case 0: return "green";
            case 1: return "pink";
            case 2: return "blue";
            case 3: return "yellow";
            default: return "gray";
        }
    }

    //塗り替えメソッド(まとめて変える用)
    private void fillArea(int x,int z,int w,int h,int textureId) {//座標(x,z)を起点にw,hマスを塗り替える
        
        for (int i=0;i<w;i++) {
            for (int j=0;j<h;j++){
                panels[(i+x) * MAX + (j+z)].GetComponent<Renderer>().material.mainTexture = textures[textureId];
            }
        }
    }

    //塗り替えメソッド(指定マス１マスだけ変える用)
    private void changeArea(int pos, int dir,int textureId){
        if(pos > MAX && dir == 1) panels[pos-MAX].GetComponent<Renderer>().material.mainTexture = textures[textureId];
        if(pos % MAX != 0 && dir == 2) panels[pos-1].GetComponent<Renderer>().material.mainTexture = textures[textureId];
        if(pos % MAX != MAX-1 && dir == 3) panels[pos + 1].GetComponent<Renderer>().material.mainTexture = textures[textureId];
        if(pos < MAX*MAX-MAX && dir == 4) panels[pos + MAX].GetComponent<Renderer>().material.mainTexture = textures[textureId];
    }

    private string Colouring(int textureId) {
        switch (textureId) {
            case 0 : return "green";
            case 1 : return "pink";
            case 2 : return "blue";
            case 3 : return "yellow";
            case 4 : return "gray";
            default: return null;
        }
    }

    //自エリア用メソッド
    private void collorArea(int x, int z, int w, int h, int textureId){

        for (int i = 0; i < w; i++){
            for (int j = 0; j < h; j++){
                color[(i + x) * MAX + (j + z)] = Colouring(textureId);
            }
        }
    }

    private void changeColor(int pos, int dir, int textureId){
        if (pos > MAX && dir == 1) color[pos - MAX] = player(textureId);
        if (pos % MAX != 0 && dir == 2) color[pos-1] = player(textureId);
        if (pos % MAX != MAX - 1 && dir == 3) color[pos+1] = player(textureId);
        if (pos < MAX * MAX - MAX && dir == 4) color[pos+MAX] = player(textureId);
    }
    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        panels = new GameObject[MAX*MAX];

        for (int i = 0; i < mass.Length; i++)
        {
            panels[i] = GameObject.Find("stage/panels/Plane ("+(i+1)+")");
            color[i] = "gray";
        }

        //テクスチャー読み込む
        for (int i = 0; i < textures.Length; i++) {
            textures[i] = (Texture)Resources.Load("masu"+(i+1));
        }

        //テクスチャーを貼る ID -> 緑,ピンク,青,黄色,灰
        fillArea(0,0,6,6,4); //全部灰色に

        //自エリアの色変え
        fillArea(0, 0, startArea, startArea, myColor);
        collorArea(0,0,startArea,startArea,myColor);

	}
	
	// Update is called once per frame
	void Update () {
     
        if (Input.GetKeyDown("w")){
            if (pos - MAX >= 0 && color[pos-MAX] == player(myColor)) {
                transform.Translate(0, 0, 10);
                pos -= MAX;
            }
        }
        
        if (Input.GetKeyDown("d")){
            if (pos % MAX != MAX- 1 && color[pos + 1] == player(myColor)) {
                transform.Translate(10, 0, 0);
                pos += 1; 
            }
        }

        if (Input.GetKeyDown("a"))
        {
            if (pos % MAX != 0 && color[pos - 1] == player(myColor)) {
                transform.Translate(-10, 0, 0);
                pos -= 1;
            }
        }
        if (Input.GetKeyDown("s"))
        {
            if (pos + MAX < MAX* MAX && color[pos + MAX] == player(myColor)) {
                transform.Translate(0, 0, -10);
                pos += MAX;
            }
        }
        if (Input.GetKeyDown("space")) {
            if (pos-MAX > 0 && color[pos - MAX] != player(myColor))
            {//上が自分の色と異なる
                changeArea(pos,1, myColor);
                changeColor(pos,1, myColor);
            }

            if (pos % MAX != 0 && color[pos - 1] != player(myColor))
            {//左が自分の色と異なる
                changeArea(pos, 2, myColor);
                changeColor(pos, 2, myColor);
            }

            if (pos % MAX != MAX-1 && color[pos + 1] != player(myColor))
            {//右が自分の色と異なる
                changeArea(pos, 3, myColor);
                changeColor(pos, 3, myColor);
            }

            if (pos < MAX*MAX-MAX && color[pos + MAX] != player(myColor))
            {//下が自分の色と異なる
                changeArea(pos, 4, myColor);
                changeColor(pos, 4, myColor);
            }
        }
        else
        {
            animator.SetBool("is_running", false);
        }
    }
}
