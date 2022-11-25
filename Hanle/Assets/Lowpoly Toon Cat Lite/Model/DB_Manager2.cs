using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;

public class DB_Manager2 : MonoBehaviour
{
    public string databaseUrl = "https://monsterball-adb89-default-rtdb.firebaseio.com/";
    // Start is called before the first frame update
    void Start()
    {
        //DB URL을 URI형태로 변경 및 설정
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(databaseUrl);

        //데이터 저장 함수를 실행
        SaveData();
    }

    void SaveData()
    {
        //저장용 클래스 변수 생성(저장할 이미지파일 설정하는 곳)
        Solicmonster data1 = new Solicmonster("SolicMon1", 37.48985f, 126.9601f, false);
        Solicmonster data2 = new Solicmonster("SolicMon2", 37.47811f, 126.95151f, false);
        Solicmonster data3 = new Solicmonster("SolicMon3", 37.47815f, 126.95651f, false);

        //클래스 변수 Json 데이터로 변경하는 곳
        string jsonCat = JsonUtility.ToJson(data1);
        string jsonScar = JsonUtility.ToJson(data2);
        string jsonWolf = JsonUtility.ToJson(data3);

        //DB의 최상단(Root) 디렉터리를 참조
        DatabaseReference refData = FirebaseDatabase.DefaultInstance.RootReference;

        //최상단 디렉토리 기준으로 하위 디렉토리를 지정 json 데이터를 DB에 저장

        refData.Child("Monsters").Child("Data1").SetRawJsonValueAsync(jsonCat);
        refData.Child("Monsters").Child("Data2").SetRawJsonValueAsync(jsonScar);
        refData.Child("Monsters").Child("Data3").SetRawJsonValueAsync(jsonWolf);

        print("데이터 저장 완료!");

    }

}
public class Solicmonster
{
    public string name;
    public float latitude;
    public float longitude;
    public bool isCaptured = false;

    public Solicmonster(string objName, float lat, float lon, bool captured)
    {
        name = objName;
        latitude = lat;
        longitude = lon;
        isCaptured = captured;
    }
}
