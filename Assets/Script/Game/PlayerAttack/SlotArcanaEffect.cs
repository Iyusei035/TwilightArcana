using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlotArcanaEffect : MonoBehaviour
{
    public List<GameObject> gameObjects;
    private bool initFlg = false;
    bool pushFlg = false;
    private void Start()
    {
        for (int count = 0; gameObjects.Count > count; ++count)
        {
            if (gameObjects[count] == null) return;
            if (gameObjects[count].GetComponent<GameSceneInventory>() == null) return;
            initFlg = true;
        }
    }
    void Update()
    {
        if (!initFlg) return;
        if (gameObjects[0].GetComponent<GameSceneInventory>().GetItem())
            gameObjects[0].GetComponent<GameSceneInventory>().GetItem().GetCoolTime();
        if (gameObjects[1].GetComponent<GameSceneInventory>().GetItem())
            gameObjects[1].GetComponent<GameSceneInventory>().GetItem().GetCoolTime();
        if (gameObjects[2].GetComponent<GameSceneInventory>().GetItem())
            gameObjects[2].GetComponent<GameSceneInventory>().GetItem().GetCoolTime();
        if (gameObjects[3].GetComponent<GameSceneInventory>().GetItem())
            gameObjects[3].GetComponent<GameSceneInventory>().GetItem().GetCoolTime();
        if (Input.GetKeyDown(KeyCode.Mouse0) && !pushFlg)
        {
            pushFlg = true;
            if (gameObjects[0].GetComponent<GameSceneInventory>().GetItem() == null) return;
            if (!gameObjects[0].GetComponent<GameSceneInventory>().GetItem().GetActiveFlg()) return;
            if (gameObjects[0].GetComponent<GameSceneInventory>().GetItem().GetArcana() == null) return;
            gameObjects[0].GetComponent<GameSceneInventory>().GetItem().GetArcana().ArcanaEffect();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && !pushFlg)
        {
            pushFlg = true;
            if (gameObjects[1].GetComponent<GameSceneInventory>().GetItem() == null) return;
            if (!gameObjects[1].GetComponent<GameSceneInventory>().GetItem().GetActiveFlg()) return;
            if (gameObjects[1].GetComponent<GameSceneInventory>().GetItem().GetArcana() == null) return;
            gameObjects[1].GetComponent<GameSceneInventory>().GetItem().GetArcana().ArcanaEffect();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !pushFlg)
        {
            pushFlg = true;
            if (gameObjects[2].GetComponent<GameSceneInventory>().GetItem() == null) return;
            if (!gameObjects[2].GetComponent<GameSceneInventory>().GetItem().GetActiveFlg()) return;
            if (gameObjects[2].GetComponent<GameSceneInventory>().GetItem().GetArcana() == null) return;
            gameObjects[2].GetComponent<GameSceneInventory>().GetItem().GetArcana().ArcanaEffect();
        }
        else if (Input.GetKeyDown(KeyCode.E) && !pushFlg)
        {
            pushFlg = true;
            if (gameObjects[3].GetComponent<GameSceneInventory>().GetItem() == null) return;
            if (!gameObjects[3].GetComponent<GameSceneInventory>().GetItem().GetActiveFlg()) return;
            if (gameObjects[3].GetComponent<GameSceneInventory>().GetItem().GetArcana() == null) return;
            gameObjects[3].GetComponent<GameSceneInventory>().GetItem().GetArcana().ArcanaEffect();
        }
        else if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) && pushFlg)
        {
            pushFlg = false;
        }
    }
}
