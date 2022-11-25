#if UNITY_2018_3_OR_NEWER && PLATFORM_ANDROID
using UnityEngine.Android;
#endif

using System.Collections.Generic;
using JetBrains.Annotations;
using NinevaStudios.GoogleMaps.Internal;
using UnityEngine;
using UnityEngine.UI;
using NinevaStudios.GoogleMaps;

public class GoogleMapUI : MonoBehaviour
{
#pragma warning disable 0649

    #region map_options

    //public Texture2D newArkAreaImage;
    /*public Texture2D icon;

    public TextAsset customStyleJson;
    public TextAsset heatmapDataJsonPoliceStations;
    public TextAsset heatmapDataJsonMedicare;
    public TextAsset markerClusterData;*/


    //public Toggle compassToggle;
    //public Toggle liteModeToggle;
    //public Toggle mapToolbarToggle;
    //public Toggle rotateGesturesToggle;
    //public Toggle scrollGesturesToggle;
    //public Toggle tiltGesturesToggle;
    //public Toggle zoomGesturesToggle;
    //public Toggle zoomControlsToggle;
    //public Toggle trafficControlsToggle;

    //[Header("Map Type")] public Dropdown mapType;

    //[Header("Min/Max Zoom")] public InputField minZoom;

    //public InputField maxZoom;

    public float camPosLat;
    public float camPosLng;
    public float camPosZoom;
    public float camPosTilt;
    public float camPosBearing;
    

    #endregion

#pragma warning restore 0649

    public RectTransform rect;

    GoogleMapsView _map;

    void OnEnable()
    {
        SetInitialOptionsValues();
        // Show the map when the demo starts
        OnShow();
    }

    public void OnDisable()
    {
        _map.Dismiss();
    }

    public void OnDestroy()
    {
        _map.Dismiss();
    }

    void SetInitialOptionsValues()
    {
        //mapType.value = (int)GoogleMapType.Normal;

        // Camera position
        camPosLat = 36.82179560f;
        camPosLng = 128.09508410f;
        camPosZoom = 14f;
        camPosTilt = 1f;
        camPosBearing = 0f;

        // Zoom constraints
        //minZoom.text = "1.0";
       // maxZoom.text = "20.0";
    }

    /// <summary>
    /// Shows the <see cref="GoogleMapsView"/>
    /// </summary>
    [UsedImplicitly]
    public void OnShow()
    {
        Dismiss();

        GoogleMapsView.CreateAndShow(CreateMapViewOptions(), RectTransformToScreenSpace(rect), OnMapReady);
    }

    void OnMapReady(GoogleMapsView googleMapsView)
    {
        _map = googleMapsView;
        _map.SetPadding(0, 0, 0, 0);

        /*var isStyleUpdateSuccess = _map.SetMapStyle(customStyleJson.text);
        if (isStyleUpdateSuccess)
        {
            Debug.Log("Successfully updated style of the map");
        }
        else
        {
            Debug.LogError("Setting new map style failed.");
        }*/

#if UNITY_2018_3_OR_NEWER && PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
#endif

        // UNCOMMENT if testing with showing users location. DON'T FORGET MANIFEST LOCATION PERMISSION!!!
        _map.IsMyLocationEnabled = true;
        //_map.IsTrafficEnabled = trafficControlsToggle.isOn;
        _map.IsTrafficEnabled = false;
        _map.UiSettings.IsMyLocationButtonEnabled = true;
        _map.OnOrientationChange += () => { _map.SetRect(RectTransformToScreenSpace(rect)); };

        _map.SetOnCameraMoveStartedListener(moveReason => Debug.Log("Camera move started because: " + moveReason));
        _map.SetOnCameraIdleListener(() => Debug.Log("Camera is now idle"));
        _map.SetOnCircleClickListener(circle => Debug.Log("Circle clicked: " + circle));
        _map.SetOnPolylineClickListener(polyline => Debug.Log("Polyline clicked: " + polyline));
        _map.SetOnPolygonClickListener(polygon => Debug.Log("Polygon clicked: " + polygon));
        _map.SetOnMarkerClickListener(marker => Debug.Log("Marker clicked: " + marker), false);
        _map.SetOnMarkerDragListener(
            marker => Debug.Log("Marker drag start: " + marker),
            marker => Debug.Log("Marker drag end: " + marker),
            marker => Debug.Log("Marker drag: " + marker));
        _map.SetOnGroundOverlayClickListener(overlay => Debug.Log("Ground overlay clicked: " + overlay));
        _map.SetOnInfoWindowClickListener(marker => Debug.Log("Marker info window clicked: " + marker));
        _map.SetOnMapClickListener(point =>
        {
            Debug.Log("Map clicked: " + point);
            //_map.AddMarker(DemoUtils.RandomColorMarkerOptions(point));
        });
        _map.SetOnLongMapClickListener(point =>
        {
            Debug.Log("Map long clicked: " + point);
            //_map.AddCircle(DemoUtils.RandomColorCircleOptions(point));
        });

        // When the map is ready we can start drawing on it
        

       // AddOtherExampleOverlays();

        Debug.Log("Map is ready: " + _map);
        OnTestUiSettingsButtonClick(true);
    }

    /*void AddOtherExampleOverlays()
    {
        // New Ark overlay image
        _map.AddGroundOverlay(DemoUtils.CreateNewArkGroundOverlay(newArkAreaImage));

        // Talkeetna
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _map.AddGroundOverlay(DemoUtils.CreateTalkeetnaGroundOverlayForIos());
        }

        // Medicare
        var medicareHeatmapOptions = DemoUtils.CreateDemoHeatMap(heatmapDataJsonMedicare.text);
        _map.AddTileOverlay(medicareHeatmapOptions);

        // Berlin marker
        _map.AddMarker(DemoUtils.CreateTexture2DMarkerOptions(icon));
    }*/

    GoogleMapsOptions CreateMapViewOptions()
    {
        var options = new GoogleMapsOptions();

        options.MapType(GoogleMapType.Normal);

        // Camera position
        options.Camera(CameraPosition);

        options.AmbientEnabled(false);//     ε 
        //options.CompassEnabled(compassToggle.isOn);
        options.CompassEnabled(true);
        //options.LiteMode(liteModeToggle.isOn);
        //options.MapToolbarEnabled(mapToolbarToggle.isOn);
        //options.RotateGesturesEnabled(rotateGesturesToggle.isOn);
        //options.ScrollGesturesEnabled(scrollGesturesToggle.isOn);
        //options.TiltGesturesEnabled(tiltGesturesToggle.isOn);
        //options.ZoomGesturesEnabled(zoomGesturesToggle.isOn);
        //options.ZoomControlsEnabled(zoomControlsToggle.isOn);
        options.LiteMode(false);
        options.MapToolbarEnabled(false);
        options.RotateGesturesEnabled(true);
        options.ScrollGesturesEnabled(true);
        options.TiltGesturesEnabled(true);
        options.ZoomGesturesEnabled(true);
        options.ZoomControlsEnabled(true);

        //options.MinZoomPreference(float.Parse(minZoom.text));
        //options.MaxZoomPreference(float.Parse(maxZoom.text));
        options.MinZoomPreference(1);
        options.MaxZoomPreference(20);

        return options;
    }


    CameraPosition CameraPosition => new CameraPosition(
        new LatLng(camPosLat, camPosLng),
        camPosZoom,
        camPosTilt,
        camPosBearing);

    #region update_buttons_click

    [UsedImplicitly]
    public void OnTestUiSettingsButtonClick(bool enable)
    {
        if (_map == null)
        {
            return;
        }

        EnableAllSettings(_map.UiSettings, enable);
    }

    static void EnableAllSettings(UiSettings settings, bool enable)
    {
        Debug.Log("Current Ui Settings: " + settings);

        // Buttons/other
        settings.IsCompassEnabled = enable;
        settings.IsIndoorLevelPickerEnabled = enable;
        settings.IsMapToolbarEnabled = enable;
        settings.IsMyLocationButtonEnabled = enable;
        settings.IsZoomControlsEnabled = enable;

        // Gestures
        settings.IsRotateGesturesEnabled = enable;
        settings.IsScrollGesturesEnabled = enable;
        settings.IsTiltGesturesEnabled = enable;
        settings.IsZoomGesturesEnabled = enable;
        settings.SetAllGesturesEnabled(enable);
    }

    #endregion
    void Dismiss()
    {
        if (_map != null)
        {
            _map.Dismiss();
            _map = null;
        }
    }

    #region camera_animations

    [UsedImplicitly]
    public void AnimateCameraNewCameraPosition()
    {
        AnimateCamera(CameraUpdate.NewCameraPosition(CameraPosition));
    }

    [UsedImplicitly]
    public void AnimateCameraNewLatLng()
    {
        AnimateCamera(CameraUpdate.NewLatLng(new LatLng(camPosLat, camPosLng)));
    }


    [UsedImplicitly]
    public void AnimateCameraNewLatLngZoom()
    {
        const int zoom = 10;
        AnimateCamera(CameraUpdate.NewLatLngZoom(new LatLng(camPosLat, camPosLng), zoom));
    }

    [UsedImplicitly]
    public void AnimateCameraScrollBy()
    {
        const int xPixel = 250;
        const int yPixel = 250;
        AnimateCamera(CameraUpdate.ScrollBy(xPixel, yPixel));
    }

    [UsedImplicitly]
    public void AnimateCameraZoomByWithFixedLocation()
    {
        const int amount = 5;
        const int x = 100;
        const int y = 100;
        AnimateCamera(CameraUpdate.ZoomBy(amount, x, y));
    }

    [UsedImplicitly]
    public void AnimateCameraZoomByAmountOnly()
    {
        const int amount = 5;
        AnimateCamera(CameraUpdate.ZoomBy(amount));
    }

    [UsedImplicitly]
    public void AnimateCameraZoomIn()
    {
        AnimateCamera(CameraUpdate.ZoomIn());
    }

    [UsedImplicitly]
    public void AnimateCameraZoomOut()
    {
        AnimateCamera(CameraUpdate.ZoomOut());
    }

    [UsedImplicitly]
    public void AnimateCameraZoomTo()
    {
        const int zoom = 10;
        AnimateCamera(CameraUpdate.ZoomTo(zoom));
    }

    void AnimateCamera(CameraUpdate cameraUpdate)
    {
        if (_map == null)
        {
            return;
        }

        _map.AnimateCamera(cameraUpdate);
        // _map.MoveCamera(cameraUpdate);
    }

    #endregion

    [UsedImplicitly]
    public void OnLogMyLocation()
    {
        if (_map == null)
        {
            return;
        }

        if (!_map.IsMyLocationEnabled)
        {
            Debug.Log("Location tracking is not enabled. Set 'IsMyLocationButtonEnabled' to 'true' to start tracking location");
            return;
        }

        if (_map.Location != null)
        {
            Debug.Log("My location: " + _map.Location);
        }
        else
        {
            Debug.Log("My location is not available");
        }
    }

    [UsedImplicitly]
    public void OnSetCustomStyle()
    {
        //_map?.SetMapStyle(customStyleJson.text);
    }

    [UsedImplicitly]
    public void OnResetToDefaultStyle()
    {
        _map?.SetMapStyle(null);
    }

    [UsedImplicitly]
    public void OnSetMapVisible()
    {
        if (_map != null)
        {
            _map.IsVisible = true;
        }
    }

    [UsedImplicitly]
    public void OnSetMapInvisible()
    {
        if (_map != null)
        {
            _map.IsVisible = false;
        }
    }

    [UsedImplicitly]
    public void OnSetMapPosition() => _map?.SetRect(new Rect(0f, 0f, Screen.width / 2f, Screen.height / 2f));

    [UsedImplicitly]
    public void OnLogMapProperties()
    {
        if (_map != null)
        {
            Debug.Log("Current map: " + _map);
        }
    }

    [UsedImplicitly]
    public void OnUpdateMapProperties()
    {
        if (_map != null)
        {
            _map.MapType = GoogleMapType.Hybrid;
        }
    }

    [UsedImplicitly]
    public void LogMapProperties()
    {
        Debug.Log(_map);
    }

    [UsedImplicitly]
    public void LogProjectionData()
    {
        var projection = _map.Projection;
        print(projection.GetVisibleRegion());
        print(projection.FromScreenLocation(new Vector2Int((int)Input.mousePosition.x, (int)Input.mousePosition.y)));
        print(projection.ToScreenLocation(new LatLng(90, 90)));
    }

    #region helpers

    static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        Rect rect = new Rect(transform.position.x, Screen.height - transform.position.y, size.x, size.y);
        rect.x -= transform.pivot.x * size.x;
        rect.y -= (1.0f - transform.pivot.y) * size.y;
        rect.x = Mathf.CeilToInt(rect.x);
        rect.y = Mathf.CeilToInt(rect.y);
        return rect;
    }

    #endregion

    void CenterCamera(LatLng latLng)
    {
        _map?.AnimateCamera(CameraUpdate.NewLatLng(latLng));
    }
}