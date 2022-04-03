using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Com.Airbnb.Lottie;
using System.Threading.Tasks;


namespace GamesCatalog.Droid
{
    [Activity(Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        System.Threading.Tasks.Task splashscreen;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //var animationView = FindViewById<LottieAnimationView>(Resource.Id.animation_view);

        }
        protected override void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.SplashScreen);
            splashscreen = new System.Threading.Tasks.Task(() => {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            });
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }
        async void SimulateStartup()
        {
            await Task.Delay(6000);
            splashscreen.Start();
        }
    }

}