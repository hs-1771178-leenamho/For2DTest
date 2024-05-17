using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
������ �������� �⺻���� ������(�̸�, �ǹ��� Ÿ��(����, �ڿ� ä��), �ִ� ü��, ���� ü��, ���� 
 */
public class CraftBuilding : MonoBehaviour
{
    public BuildingInfo buildingInfo;
    public BuildingState buildingState;

    public void TakeDamageOnBuilding(float _dmg) // �ǹ��� �������� ����
    {
        this.buildingInfo.B_curHp = _dmg;
        if(this.buildingInfo.B_curHp <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public float GetEfficiency()
    {
        return this.buildingInfo.B_curHp / this.buildingInfo.B_maxHp;
    }

       
}
