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
        //DB URL�� URI���·� ���� �� ����
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(databaseUrl);

        //������ ���� �Լ��� ����
        SaveData();
    }

    void SaveData()
    {
        //����� Ŭ���� ���� ����(������ �̹������� �����ϴ� ��)
        Solicmonster data1 = new Solicmonster("SolicMon1", 37.48985f, 126.9601f, false);
        Solicmonster data2 = new Solicmonster("SolicMon2", 37.47811f, 126.95151f, false);
        Solicmonster data3 = new Solicmonster("SolicMon3", 37.47815f, 126.95651f, false);

        //Ŭ���� ���� Json �����ͷ� �����ϴ� ��
        string jsonCat = JsonUtility.ToJson(data1);
        string jsonScar = JsonUtility.ToJson(data2);
        string jsonWolf = JsonUtility.ToJson(data3);

        //DB�� �ֻ��(Root) ���͸��� ����
        DatabaseReference refData = FirebaseDatabase.DefaultInstance.RootReference;

        //�ֻ�� ���丮 �������� ���� ���丮�� ���� json �����͸� DB�� ����

        refData.Child("Monsters").Child("Data1").SetRawJsonValueAsync(jsonCat);
        refData.Child("Monsters").Child("Data2").SetRawJsonValueAsync(jsonScar);
        refData.Child("Monsters").Child("Data3").SetRawJsonValueAsync(jsonWolf);

        print("������ ���� �Ϸ�!");

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
