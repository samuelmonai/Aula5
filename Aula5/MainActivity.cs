using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using SQLite;

namespace Aula5
{
    [Activity(Label = "Aula5", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        double IngresoColo = 0, IngresoMex = 0, EgresoColo = 0, EgresoMex = 0, CapitalM = 0, CapitalC = 0;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = System.IO.Path.Combine(path,"Base.db3");
            var conn = new SQLiteConnection(path);
            conn.CreateTable<Informacion>();

            Button btnCalcular = FindViewById<Button>
                (Resource.Id.btncalcular);
            EditText txtIngColo = FindViewById<EditText>
                (Resource.Id.txtIngrColo);
            EditText txtEgrColo = FindViewById<EditText>
                (Resource.Id.txtEgrColo);
            EditText txtIngMex = FindViewById<EditText>
                (Resource.Id.txtIngrMex);
            EditText txtEgrMex = FindViewById<EditText>
                (Resource.Id.txtEgrMex);


            btnCalcular.Click += delegate
            {
                try
                {
                    IngresoColo = double.Parse(txtIngColo.Text);
                    IngresoMex = double.Parse(txtIngMex.Text);
                    EgresoColo = double.Parse(txtEgrColo.Text);
                    EgresoMex = double.Parse(txtEgrMex.Text);
                    CapitalM = IngresoMex - EgresoMex;
                    CapitalC = IngresoColo - EgresoColo;

                    var Insert = new Informacion();
                    Insert.IngresosMexico = IngresoMex;
                    Insert.IngresosColombia = IngresoColo;
                    Insert.EgresosMexico = EgresoMex;
                    Insert.EgresosColombia = EgresoColo;
                    conn.Insert(Insert);

                    Toast.MakeText(this, "Guardado em SQLite", ToastLength.Long).Show();
                     Carga();
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message,
                    ToastLength.Short).Show();
                    throw;
                }
            };
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
        public void Carga()
        {
            Intent objIntent = new Intent(this,
                typeof(VistaCapital));
            objIntent.PutExtra("CapitalM", CapitalM);
            objIntent.PutExtra("CapitalC", CapitalC);
            StartActivity(objIntent);
        }
    }
    public class Informacion
    {
        public double IngresosMexico { get; set; }
        public double IngresosColombia { get; set; }
        public double EgresosMexico { get; set; }
        public double EgresosColombia { get; set; }
    }
}

