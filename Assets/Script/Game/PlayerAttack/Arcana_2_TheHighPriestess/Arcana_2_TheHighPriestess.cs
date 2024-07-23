using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_2_TheHighPriestess : ArcanaBase
{
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("2_TheHighPriestess/BasicBeam");
        _normalEffect = Instantiate(_prefab, _pos, Quaternion.identity);
        _normalEffect.gameObject.SetActive(true);
    }
}
