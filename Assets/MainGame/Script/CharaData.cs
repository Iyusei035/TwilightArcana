using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Create StatusData")]
public class Charadata : ScriptableObject
{
    public string NAME; //�L�����E�G��
    public int MAXHP; //�ő�HP
    public int MAXMP; //�ő�MP
    public int ATK; //�U����
    public int DEF; //�h���
    public int INT; //����
    public int RES; //���@��R��
    public int AGI; //�ړ����x
    public int LV; //���x��
    public int GETEXP; //�擾�o���l
    public int GETGOLD; //�擾�ł��邨��
}