using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP5DistanceManager : MonoBehaviour
{
    private double[] lat = new double[] { 36.94790d, 36.94790d, 36.94790d, 36.94790d, 36.94762d, 36.94762d, 36.94762d, 36.94762d };
    private double[] lng = new double[] { 127.90973d, 127.90973d, 127.90973d, 127.90973d, 127.91020d, 127.91020d, 127.91020d, 127.91020d };

    //private double[] lat = new double[] { 37.5138791d, 37.5138791d, 37.5138791d, 37.5138791d, 37.5138791d, 37.5138791d, 37.5138791d, 37.5138791d };
    //private double[] lng = new double[] { 127.1058283d, 127.1058283d, 127.1058283d, 127.1058283d, 127.1058283d, 127.1058283d, 127.1058283d, 127.1058283d };
    /*
    private double[] lat = new double[] { 36.82287070d, 36.82287070d, 36.82285268d, 36.82288872d,
    36.82179560d, 36.82179560d, 36.82177758d, 36.82181362d };
    private double[] lng = new double[] { 128.09507552d, 128.09503068d, 128.09505310d, 128.09505310d,
    128.09506168d, 128.09510652d, 128.09508410d, 128.09508410d };
    */
    /*
    private double[] lat = new double[] { 36.82293430d, 36.82293430d, 36.82293430d, 36.82293430d,
    36.82179560d, 36.82179560d, 36.82177758d, 36.82181362d };
    private double[] lng = new double[] { 128.09507810d, 128.09507810d, 128.09507810d, 128.09507810d,
    128.09506168d, 128.09510652d, 128.09508410d, 128.09508410d };
    */
    public bool isEP5_Part2;

    public GameObject[] gameObjects;

    private void Start()
    {
        StartCoroutine(CheckDistance());
    }


    IEnumerator CheckDistance()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            double lat = NativeGPSPlugin.GetLatitude();
            double lng = NativeGPSPlugin.GetLongitude();
            if (lat == 0)
            {
                continue;
            }

            for(int i = 0; i < 4; i++)
            {
                if (gameObjects[i] == null)
                {
                    continue;
                }
                gameObjects[i].SetActive(GetDistance(lat, lng, this.lat[i + (isEP5_Part2 ? 4 : 0)], this.lng[i + (isEP5_Part2 ? 4 : 0)]) < 5);
            }
        }
    }

    public double GetDistance(double lat1, double lon1, double lat2, double lon2)
    {
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

    private double deg2rad(double deg)
    {
        return (double)(deg * Math.PI / (double)180d);
    }
    /// 
    private double rad2deg(double rad)
    {
        return (double)(rad * (double)180d / Math.PI);
    }
}
