using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using SQLite;

namespace Aula5
{
    [Activity(Label = "VistaCapital")]
    public class VistaCapital : Activity
    {
        double defaultValue;
        protected override void OnCreate(Bundle Bundle)
        {
            base.OnCreate(Bundle);
            SetContentView(Resource.Layout.VistaCapital);
            EditText txtCapitalM = FindViewById<EditText>
                (Resource.Id.txtCapitalM);
            EditText txtCapitalC = FindViewById<EditText>
                (Resource.Id.txtCapitalC);
            ImageView ImagenMex = FindViewById<ImageView>
                (Resource.Id.imageMex);
            ImageView ImagenCol = FindViewById<ImageView>
                (Resource.Id.imageCol);
            Button btnSair = FindViewById<Button>
                (Resource.Id.btnsair);
            try
            {
                txtCapitalC.Text = Intent.GetDoubleExtra("CapitalC", defaultValue).ToString();
                txtCapitalM.Text = Intent.GetDoubleExtra("CapitalM", defaultValue).ToString();
                ImagenMex.SetImageResource(Resource.Drawable.Mexico);
                ImagenCol.SetImageResource(Resource.Drawable.Colombia);

                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = System.IO.Path.Combine(path, "Base.db3");
                var conn = new SQLiteConnection(path);
                var elementos = from s in conn.Table<Informacion>() select s;

                foreach (var fila in elementos)
                {
                    Toast.MakeText
                        (this, fila.IngresosMexico.ToString(), ToastLength.Short).Show();
                    Toast.MakeText
                        (this, fila.IngresosColombia.ToString(), ToastLength.Short).Show();
                    Toast.MakeText
                        (this, fila.EgresosMexico.ToString(), ToastLength.Short).Show();
                    Toast.MakeText
                        (this, fila.EgresosColombia.ToString(), ToastLength.Short).Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText
                    (this, ex.Message,
                    ToastLength.Short).Show();
            }

            btnSair.Click += delegate
            {
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            };//

        }
    }
}