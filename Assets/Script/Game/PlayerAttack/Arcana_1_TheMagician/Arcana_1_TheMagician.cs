using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_1_TheMagician : ArcanaBase
{
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("1_TheMagician/FireSkill001 1");
        _normalEffect = Instantiate(_prefab, _pos, Quaternion.identity);
        _normalEffect.gameObject.SetActive(true);
    }
}
