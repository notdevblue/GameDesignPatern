using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAwake : MonoBehaviour
{
    static public GameAwake Instance { get; private set;}

    private StringBuilder _sb;
    private bool bFirstInit = false;
    private string tempStr;

    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

        _sb = new StringBuilder();

        this.gameObject.hideFlags = HideFlags.HideAndDontSave;
    }

    private void Start()
    {
        if(!bFirstInit) {
            InitOnce();
            bFirstInit = true;
        }
    }

    private void InitOnce()
    {
        LoadPrefabDictionary();
        LoadSpriteDictionary();

        // MGScene.Instance.Generate();
    }

    private void LoadPrefabDictionary()
    {
        Global.prefabsDictionary = new Dictionary<ePrefabs, GameObject>();

        object[] files;

        _sb.Remove(0, _sb.Length);
        _sb.Append("Prefabs/");

        files = Resources.LoadAll(_sb.ToString());
        SetPrefabDicionary(files);
    }

    private void SetPrefabDicionary(object[] files)
    {
        for (int i = 0; i < files.Length; ++i) {
            GameObject outObj;

            tempStr = GetFileName(files[i].ToString());

            if(Global.prefabsDictionary.TryGetValue((ePrefabs)Enum.Parse(typeof(ePrefabs), tempStr), out outObj)) {
                Global.prefabsDictionary.Add((ePrefabs)Enum.Parse(typeof(ePrefabs), tempStr), (GameObject)files[i]);
            }

        }
    }

    private void LoadSpriteDictionary()
    {
        Global.spriteDictionary = new Dictionary<string, Sprite>();

        Sprite[] files;

        _sb.Remove(0, _sb.Length);
        _sb.Append("Sprites/");

        files = Resources.LoadAll<Sprite>(_sb.ToString());
        SetSpriteDictionary(files);
    }

    private void SetSpriteDictionary(Sprite[] files)
    {
        for (int i = 0; i < files.Length; ++i) {
            Sprite outObj;

            tempStr = GetFileName(files[i].ToString());

            if(!Global.spriteDictionary.TryGetValue(tempStr, out outObj)) {
                Global.spriteDictionary.Add(tempStr, (Sprite)files[i]);
            }
        }
    }


    string GetFileName(string objName)
    {
        string rtn = null;
        rtn = objName.Substring(0, objName.IndexOf(' '));

        return rtn;
    }
}
