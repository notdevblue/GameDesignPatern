using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image icon { get; set; }

    public GameObject owner { get; set; }
}

public class Radar : MonoBehaviour
{
    // 나의 현재 위치(플레이어)
    // 맵 화면 비율에 따른 스케일값
    // 레이더안에 들어온 객체들을 담아줄 리스트

    public Transform playerTrm;

    public float mapScale = 2.0f;

    public static List<RadarObject> radObjList = new List<RadarObject>();
    public Text text;

    private void Update()
    {
        DrawRadarDots();
    }

    void DrawRadarDots()
    {
        foreach (RadarObject radObj in radObjList) {
            // 벡터
            // 삼각함수를 사용한다
            Vector3 radarPos = (radObj.owner.transform.position - playerTrm.position);

            float distToObj = Vector3.Distance(playerTrm.position, radObj.owner.transform.position) * mapScale;
            float deltaY = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - playerTrm.eulerAngles.y;

            radarPos.x = distToObj * Mathf.Cos(deltaY * Mathf.Deg2Rad) * -1;
            radarPos.z = distToObj * Mathf.Sin(deltaY * Mathf.Deg2Rad);

            radObj.icon.transform.SetParent(this.transform);
            RectTransform rt = this.GetComponent<RectTransform>();

            radObj.icon.transform.position = new Vector3(radarPos.x + rt.pivot.x, radarPos.z + rt.pivot.y, 0) + this.transform.position;
        }
    }

    public void ItemDropped(GameObject obj)
    {
        print("ItemDropped");
        RegisterRadarObj(obj, obj.GetComponent<Item>().icon);
    }

    void RegisterRadarObj(GameObject obj, Image img)
    {
        Image image = Instantiate(img);
        radObjList.Add(new RadarObject() { owner = obj, icon = image });
    }

    public void DeleteRadarObj(GameObject obj)
    {
        RadarObject radarObject = radObjList.Find(e => e.owner == obj);
        if(radarObject == null) return;
        
        radObjList.Remove(radarObject);
        Destroy(radarObject.icon.gameObject);
        Destroy(radarObject.owner);
    }

    public void SetText(GameObject obj)
    {
        text.gameObject.SetActive(true);
        text.text = $"{obj.name} 획득";
        Invoke(nameof(DisableText), 2.0f);
    }

    private void DisableText()
    {
        text.gameObject.SetActive(false);
    }
}