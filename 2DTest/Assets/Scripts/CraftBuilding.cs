using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
빌딩이 가져야할 기본적인 정보들(이름, 건물의 타입(공격, 자원 채굴), 최대 체력, 현재 체력, 방어력 
 */
public class CraftBuilding : MonoBehaviour
{
    public BuildingInfo buildingInfo;
    public BuildingState buildingState;

    public void TakeDamageOnBuilding(float _dmg) // 건물에 데미지를 입음
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
