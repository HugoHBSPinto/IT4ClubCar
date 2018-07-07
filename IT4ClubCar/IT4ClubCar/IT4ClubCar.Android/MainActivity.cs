using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using System.Runtime.Remoting.Contexts;
using Android.Content;
using Android.Util;
using Plugin.Permissions;
using PluginPermission = Plugin.Permissions.Abstractions;

namespace IT4ClubCar.Droid
{
    [Activity(Label = "IT4ClubCar", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Android.Content.Context Context { get; private set; }



        protected async override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            CrossCurrentActivity.Current.Init(this, bundle);

            Xamarin.FormsMaps.Init(this, bundle);
        }



        public override Android.Views.View OnCreateView(string name, Android.Content.Context context, IAttributeSet attrs)
        {
            Context = context;
            return base.OnCreateView(name, context, attrs);
        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
    }
}

