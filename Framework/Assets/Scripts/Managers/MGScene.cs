using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MGScene : MonoBehaviour
{
    static private MGScene _instance;
    static public MGScene Instance
    {
        get
        {
            if(Instance == null) {
                _instance = FindObjectOfType(typeof(MGScene)) as MGScene;
                if(_instance == null) {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<MGScene>();
                }
            }

            return _instance;
        }
    }

    public void Generate() { }

    public StringBuilder _sb;
    public Canvas _rootCvs;
    public Transform _rootTransform;
    public Transform _addUiTrm;

    public eSceneName curScene;
    public eSceneName prevScene;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        _sb = new StringBuilder();

        this.gameObject.hideFlags = HideFlags.HideAndDontSave;

        GameObject obj = GameObject.Instantiate(Global.prefabsDictionary[ePrefabs.UIRoot]) as GameObject;
        if(obj != null)
        {
            print("UIRoot 생성!");
            _rootCvs = obj.GetComponent<Canvas>();
            _rootTransform = obj.transform;
        }

        _addUiTrm = null;

        InitFirstScene();
    }

    private void InitFirstScene()
    {
        prevScene = eSceneName.None;

        ChangeScene(eSceneName.Title);
    }

    public void ChangeScene(eSceneName inScene)
    {
        curScene = inScene;

        _sb.Remove(0, _sb.Length);
        _sb.AppendFormat("{0}Scene", eSceneName.Loading);

        SceneManager.LoadScene(_sb.ToString());

        if(curScene == eSceneName.Title) {
            ChangeUI(eSceneName.Title);
        }
        else {
            ChangeUI(eSceneName.Loading);
        }
    }

    private void ChangeUI(eSceneName inScene)
    {
        _sb.Remove(0, _sb.Length);
        _sb.AppendFormat("UIRoot{0}", inScene.ToString());
        ePrefabs addUIPrefab = (ePrefabs)(Enum.Parse(typeof(ePrefabs), _sb.ToString()));

        if(_addUiTrm != null) {
            _addUiTrm.SetParent(null);
            GameObject.Destroy(_addUiTrm.gameObject);
        }

        GameObject obj = GameObject.Instantiate(Global.prefabsDictionary[addUIPrefab]) as GameObject;
        _addUiTrm = obj.transform;
        _addUiTrm.SetParent(_rootTransform, false);
        _addUiTrm.localPosition = Vector3.zero;
        _addUiTrm.localScale = Vector3.one;

        if(inScene >= eSceneName.Loading) {
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.offsetMax = Vector2.zero;
            rt.offsetMin = Vector2.zero;
        }
    }

    public void LoadingDone()
    {
        prevScene = curScene;
        ChangeUI(curScene);

        if(curScene == eSceneName.Game) {
            GameObject.Instantiate(Global.prefabsDictionary[ePrefabs.MGPool]);
            GameObject.Instantiate(Global.prefabsDictionary[ePrefabs.MGGame]);
        }
    }


}
