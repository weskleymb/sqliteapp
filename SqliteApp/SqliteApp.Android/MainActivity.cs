using Android.App;
using Android.Content.PM;
using Android.OS;
using System.IO;
using SqliteApp.Database;

namespace SqliteApp.Droid
{
    [Activity(Label = "SqliteApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "productsDB.db");
            var productsRepository = new ProductsRepository(dbPath);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(productsRepository));
        }
    }
}

