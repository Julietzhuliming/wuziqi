using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 点
/// </summary>
public struct Pos
{
    public int x;
    public int y;
    public Pos(int x, int y)
    {
        this.x = x; this.y = y;
    }
    public bool Equals(Pos p)
    {
        return p.x == x && p.y == y;
    }
}
/// <summary>
/// 棋盘点
/// </summary>
public struct qPos
{
    public qPos(int x,int y)
    {
        pos = new Pos(x, y); r = 0;
    }
    public Pos pos;   //位置     
    public byte r;    //黑子或白子或空

}
class Wuziqi
{
    /// <summary>
    /// 棋子类型
    /// </summary>
    public enum qiType
    {
        black,
        white,
    }
    public qPos[] qipan;   //棋盘

    public Wuziqi()
    {
        Init();
    }
    public void Clear()
    {
        for (int i = 0; i < qipan.Length; i++)
            qipan[i].r = 0;
    }
    void Init()
    {
        qipan = new qPos[81];
        for (int i = 0; i < 81; i++)
        {
            int column = i % 9;
            int row = i / 9;
            qPos p = new qPos(column, row);
            Debug.Log("init:"+row+"-"+column);
        }
    }
    /// <summary>
    /// 下棋
    /// </summary>
    /// <param name="p"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool AddQi(Pos p,qiType type)
    {
        int index = p.y * 9 + p.x;
        if (qipan[index].r != 0) return false;
        if (type == qiType.black)
            qipan[index].r = 1;
        else
            qipan[index].r = 2;
        return true;    
    }
    /// <summary>
    /// 棋盘任意位置的索引值
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static int indexOf(int x, int y)
    {
        if (x < 0 || x > 8 || y < 0 || y > 8)
            return -1;
        return y * 9 + x;
    }
    /// <summary>
    /// 判断某个点是否赢
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool isWin(Pos p)
    {
        int[][] winp = getWinPos(p);
        for (int i = 0; i < winp.GetLength(0); i++)
        {
            if (isNSequence(winp[i], 5)) return true;
            else continue;
        }
        return false;
    }
    /// <summary>
    /// 判断一个连续的序列中是否有n个连续相同的
    /// </summary>
    /// <param name="array"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    bool isNSequence(int[] array,int n)
    {
        if (array.Length < n)
            return false;
        int p,q = 1;
        int N = 1;
        for (int i = 0; i < array.Length -1; i++)
        {
            p = array[i]; q = array[i + 1];
            //遇到空位或者棋子不连续或者同个位置时
            if (p == -1 || q == -1 || qipan[p].r == 0 || qipan[p].r != qipan[q].r || !qipan[p].Equals(qipan[q]))
            {
                N = 1; continue;
            }
            N++;
            if (N == 5) return true;
        }
        return false;
    }
    /// <summary>
    /// 计算任意棋子对应的赢的位置矩阵
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    int[][] getWinPos(Pos p)
    {
        List<Pos[]> plist = new List<Pos[]>();
        int[] p1 = new int[9];  //横
        int[] p2 = new int[9];  //竖
        int[] p3 = new int[9];  //左
        int[] p4 = new int[9];  //右
        for (int j = 0; j < 9; j++)
        {
            p1[j] = indexOf(p.x - 4 + j, p.y);           
            p2[j] = indexOf(p.x, p.y - 4 + j);
            p3[j] = indexOf(p.x - 4 + j, p.y + 4 - j);
            p4[j] = indexOf(p.x - 4 + j, p.y - 4 + j);
        }
        return new int[][] {p1,p2,p3,p4};
    }
}
