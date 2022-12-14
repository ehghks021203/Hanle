using System.Collections.Generic;

namespace GetSocialSdk.Editor.Android.Manifest.GoogleMaps
{
    public class UsesPermission : AndroidManifestNode
    {
        public const string InternetPermission = "android.permission.INTERNET";
        public const string LocationPermission = "android.permission.ACCESS_FINE_LOCATION";
        public const string AccessNetoworkStatePermission = "android.permission.ACCESS_NETWORK_STATE";
        
        public UsesPermission(string name) : base(
            "uses-permission", 
            ManifestTag, 
            new Dictionary<string, string>{{AndroidManifestNode.NameAttribute, name}})
        {
        }

        public override string ToString()
        {
            return Attributes[NameAttribute];
        }
    }
}