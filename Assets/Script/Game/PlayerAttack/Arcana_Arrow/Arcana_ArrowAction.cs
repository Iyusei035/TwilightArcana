using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Arcana_ArrowAction : ArcanaBase
{
    public override void ArcanaEffect()
    {
        var _player = GameObject.FindGameObjectWithTag("Player").transform.Find("male00");
        if (_player == null)
        {
            Debug.Log("プレイヤーがいません");
            return;
        }
        _prefab = Resources.Load<GameObject>("ArrowEffect/ArcanaEffect_Arrow");
        Debug.Log(_prefab.name);
        _effect = _prefab.GetComponent<VisualEffect>();
        _pos = _player.transform.position;
        _shotEffect = Instantiate(_effect, _pos, Quaternion.identity);
        _playerObject = _player.gameObject;
        Debug.Log(_shotEffect.name + ":" + _pos + ":" + _effect.name + ":" + _playerObject.name);
        _rb = _prefab.GetComponent<Rigidbody>();
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (enemy)
        {
            Debug.Log(enemy.name + "を探知しました|Position:" + enemy.transform.position);
            Debug.Log(_shotEffect.transform.position);
            _shotEffect.transform.forward = enemy.transform.position - _player.transform.position;
            _shotEffect.transform.forward.Normalize();
            Debug.Log(_shotEffect.transform.forward);
        }
        else
        {
            Debug.Log("敵を探知できませんでした");
            _shotEffect.transform.forward = _player.transform.forward;
        }
        _shotEffect.gameObject.SetActive(true);
        Destroy(_shotEffect.gameObject, 5.0f);
    }
}
