using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSObjectCreate : MonoBehaviour
{
    //Vector3 ObjSize = Vector3.zero;
    [SerializeField] GameObject[] objects;
    [SerializeField] double[] lat;
    [SerializeField] double[] lng;
    //public double currentLat;
    //public double currentLng; 
    bool[] isCreate;

    [SerializeField] GameObject ARCarmera;


    private void Start() {
        isCreate = new bool[objects.Length];
        for (int i = 0; i < isCreate.Length; i++) {
            isCreate[i] = false;
        }
        StartCoroutine(CheckDistance());
    }


    IEnumerator CheckDistance() {
        while (true) {
            yield return new WaitForSeconds(1f);
            double currentLat = NativeGPSPlugin.GetLatitude();
            double currentLng = NativeGPSPlugin.GetLongitude();
            //double currentLat = 36.94775246d;
            //double currentLng = 127.90994142d;
            if (currentLat == 0) {
                continue;
            }

            for(int i = 0; i < objects.Length; i++) {
                if (GetDistance(currentLat, currentLng, this.lat[i], this.lng[i]) < 5 && !isCreate[i]) {
                    isCreate[i] = true;
                    //objects[i].SetActive(GetDistance(currentLat, currentLng, this.lat[i], this.lng[i]) < 5);
                    Instantiate(objects[i], new Vector3(ARCarmera.transform.position.x, 
                        ARCarmera.transform.position.y - 2, ARCarmera.transform.position.z + 5), Quaternion.identity);
                }
            }
        }
    }

    public double GetDistance(double lat1, double lon1, double lat2, double lon2) {
        double theta, dist;
        theta = lon1 - lon2;

        dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1))
             * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);

        dist = dist * 60 * 1.1515;
        dist = dist * 1.609344;  
        dist = dist * 1000.0;

        return dist;
    }

    private double deg2rad(double deg) {
        return (double)(deg * Math.PI / (double)180d);
    }
    /// 
    private double rad2deg(double rad) {
        return (double)(rad * (double)180d / Math.PI);
    }
}