using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ARLocationProvider>().MockLocationData = LocationData.FromLocation(new Location(37.6602233, 127.0668063));   
        //GetComponent<>
    }
}
