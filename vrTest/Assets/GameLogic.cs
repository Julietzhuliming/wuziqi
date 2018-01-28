using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
    public Text tip;
    private Wuziqi wzq;
    public Canvas canvas;
    public Transform qiTrans;
    public GameObject whiteQ;
    public GameObject blackQ;

	// Use this for initialization
	void Start () {
        wzq = new Wuziqi();
        Debug.Log("why");
        int k = 4;
        int p = k++;
        Debug.Log(p);
	}

    public bool isTest = false;
	// Update is called once per frame
	void Update () {
        if (wflag)
            return;

        if (Input.GetMouseButtonUp(0))
        {
            ShowTip("");
            dPos = Vector2.one;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
            Input.mousePosition, canvas.worldCamera, out dPos);
            if (dPos.x < zero.x || dPos.x > final.x || dPos.y < zero.y || dPos.y > final.y)
            {
                ShowTip("请在棋牌范围内落子");return;
            }
            int x =(int)((dPos.x - zero.x) / 60);
            if ((int)(dPos.x - zero.x) % 60 > 30) x++;
            int y = (int)(dPos.y - zero.y) / 60;
            if ((int)(dPos.y - zero.y) % 60 > 30) y++;
            Debug.Log("luozi"+x+" "+y);
            if (XiaQi(new Pos(x, y), turn))
            {
                if (turn == Wuziqi.qiType.black)
                {
                    fang = "黑棋";
                    turn = Wuziqi.qiType.white;
                }
                else
                {
                    fang = "白棋";
                    turn = Wuziqi.qiType.black;
                }
                ShowTip(string.Format("{0}落子成功！", fang));
                wflag = wzq.isWin(new Pos(x, y));
                if (wflag)
                    ShowTip(string.Format("{0}赢了", fang));
            }
            else
            {
                ShowTip("落子无效，请重新落子");
            }
        }
	}
    bool wflag = false;
    string fang = "";
    Wuziqi.qiType turn = Wuziqi.qiType.black;
    void ShowTip(string str)
    {
        tip.text = str;
    }
    Vector2 final = new Vector2(237.5f,243);
    Vector2 zero = new Vector2(-237.5f, -231);
    Vector2 dPos;
    bool XiaQi(Pos p, Wuziqi.qiType qtype)
    {
        if (wzq.AddQi(p, qtype))
        {
            GameObject oo = null;
            if (qtype == Wuziqi.qiType.white)
                oo = GameObject.Instantiate(whiteQ) as GameObject;
            else oo = GameObject.Instantiate(blackQ) as GameObject;
            int idx = Wuziqi.indexOf(p.x, p.y);
            if (idx == -1) return false;
            oo.transform.SetParent(qiTrans);
            
            oo.transform.localPosition = zero + new Vector2((idx % 9) * 60, (idx / 9) * 60);
            return true;
        }
        return false;
    }
}
