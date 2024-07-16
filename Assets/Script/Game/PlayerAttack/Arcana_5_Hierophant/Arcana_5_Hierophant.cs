using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;

//public class Arcana_5_Hierophant : ArcanaBase
//{

//    public override void ArcanaEffect()
//    {
//        _pos = GameObject.FindGameObjectWithTag("Player").transform.position;

//        var _playerPos = GameObject.FindGameObjectWithTag("Player");
//        if (_playerPos == null)
//        {
//            Debug.Log("プレイヤーがいません");
//            return;
//        }

//        _prefab = Resources.Load<GameObject>("5_Hierophant/5_Hierophant");
//        Quaternion additionalRotation1 = Quaternion.Euler(0, 0, 0);
//        GameObject _shotEffectL = Instantiate(_prefab, _pos + new Vector3(1, 0, 1), additionalRotation1);
//        Destroy(_shotEffectL.gameObject, 10.0f);

//        Quaternion additionalRotation2 = Quaternion.Euler(0, 0, 0);
//        GameObject _shotEffectF = Instantiate(_prefab, _pos, additionalRotation2);
//        Destroy(_shotEffectF.gameObject, 10.0f);

//        Quaternion additionalRotation3 = Quaternion.Euler(0, 0, 0);
//        GameObject _shotEffectR = Instantiate(_prefab, _pos + new Vector3(-1, 0, -1), additionalRotation3);
//        Destroy(_shotEffectR.gameObject, 10.0f);

//    }


//}

//public class Arcana_5_Hierophant : ArcanaBase
//{
//    public override void ArcanaEffect()
//    {
//        var _playerPos = GameObject.FindGameObjectWithTag("Player");
//        if (_playerPos == null)
//        {
//            Debug.Log("プレイヤーがいません");
//            return;
//        }

//        _pos = _playerPos.transform.position;
//        _prefab = Resources.Load<GameObject>("5_Hierophant/5_Hierophant");

//        // Instantiate left effect
//        GameObject _shotEffectL = Instantiate(_prefab, _pos + _playerPos.transform.right * -1, Quaternion.identity);
//        _shotEffectL.GetComponent<Player_5_Hierophant>().targetDirection = _playerPos.transform.right * -1;
//        Destroy(_shotEffectL.gameObject, 10.0f);

//        // Instantiate forward effect
//        GameObject _shotEffectF = Instantiate(_prefab, _pos, Quaternion.identity);
//        _shotEffectF.GetComponent<Player_5_Hierophant>().targetDirection = _playerPos.transform.forward;
//        Destroy(_shotEffectF.gameObject, 10.0f);

//        // Instantiate right effect
//        GameObject _shotEffectR = Instantiate(_prefab, _pos + _playerPos.transform.right, Quaternion.identity);
//        _shotEffectR.GetComponent<Player_5_Hierophant>().targetDirection = _playerPos.transform.right;
//        Destroy(_shotEffectR.gameObject, 10.0f);
//    }
//}

public class Arcana_5_Hierophant : ArcanaBase
{
    public override void ArcanaEffect()
    {
        var _playerPos = GameObject.FindGameObjectWithTag("Player");
        if (_playerPos == null)
        {
            Debug.Log("プレイヤーがいません");
            return;
        }

        _pos = _playerPos.transform.position+new Vector3(0,1.0f,0);
        _prefab = Resources.Load<GameObject>("5_Hierophant/5_Hierophant");

        //_playerPos.transform.forward * __ここの値を変える

        // Instantiate left effect
        GameObject _shotEffectL = Instantiate(_prefab, _pos + _playerPos.transform.right * -0.5f, Quaternion.identity);
        _shotEffectL.GetComponent<Player_5_Hierophant>().targetDirection = (_playerPos.transform.forward * 4 + _playerPos.transform.right * -1).normalized;
        Destroy(_shotEffectL.gameObject, 10.0f);

        // Instantiate forward effect
        GameObject _shotEffectF = Instantiate(_prefab, _pos, Quaternion.identity);
        _shotEffectF.GetComponent<Player_5_Hierophant>().targetDirection = _playerPos.transform.forward;
        Destroy(_shotEffectF.gameObject, 10.0f);

        // Instantiate right effect
        GameObject _shotEffectR = Instantiate(_prefab, _pos + _playerPos.transform.right * 0.5f, Quaternion.identity);
        _shotEffectR.GetComponent<Player_5_Hierophant>().targetDirection = (_playerPos.transform.forward * 4 + _playerPos.transform.right).normalized;
        Destroy(_shotEffectR.gameObject, 10.0f);
    }
}
