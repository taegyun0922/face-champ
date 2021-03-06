﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Hellgate;

public class BattleController : FaceSceneController
{
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private Text text;
    private List<BattleData> listBattle;
    private int index;

    public static void Main ()
    {
        FaceSceneManager.Instance.Screen ("Battle");
    }

    public override void OnSet (object data)
    {
        base.OnSet (data);

        index = 0;

        listBattle = new List<BattleData> ();
        for (int i = 0; i < sprites.Length; i++) {
            BattleData battle = new BattleData ();
            battle.sprite = sprites[i];

            listBattle.Add (battle);
        }

        SetView ();
    }

    private void SetView ()
    {
        for (int i = 0; i < buttons.Length; i++) {
            buttons [i].image.sprite = listBattle [index + i].sprite;
        }

        text.text = "" + (index + 1) + " / " + listBattle.Count;
    }

    public void OnClick (int i)
    {
        listBattle [index + i].flag = true;

        if (listBattle.Count <= 2) {
            OverController.Main (listBattle [index + i].sprite);
            return;
        }

        index += 2;
        if (listBattle.Count <= index) {
            index = 0;

            listBattle = listBattle.FindAll (x => x.flag == true);
            foreach (BattleData battle in listBattle) {
                battle.flag = false;
            }
        }

        SetView ();
    }

    public void OnClickMain ()
    {
        MainController.Main ();
    }
}
