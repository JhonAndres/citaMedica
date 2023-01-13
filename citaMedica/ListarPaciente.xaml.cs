using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citaMedica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListarPaciente : ContentPage
    {
        private const string Url = "http://10.20.23.251/citaMedica/post.php";

        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Datos> _post;
        public string cedulaPaciente = "";
        public string nombrePaciente, apellidoPaciente, correoPaciente, telefonoPaciente, direccionPaciente, estadoPaciente;

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (cedulaPaciente != "")
            {
                await Navigation.PushAsync(new ActualizarPaciente(cedulaPaciente, nombrePaciente, apellidoPaciente, correoPaciente, telefonoPaciente, direccionPaciente, estadoPaciente));
            }
            else
            {
                await DisplayAlert("Alerta", "No se ha seleccionado un registro", "Ok");
            }
        }

        private async void btnAgendar_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new AgendarCita());

        }

        private async void btnMostrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListarCitas());
        }

        private void lstPaciente_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Datos)e.SelectedItem;
            cedulaPaciente = obj.cedulaPaciente;
            nombrePaciente = obj.nombrePaciente;
            apellidoPaciente = obj.apellidoPaciente;
            correoPaciente = obj.correoPaciente;
            telefonoPaciente = obj.telefonoPaciente;

        }
        public ListarPaciente()
        {
            InitializeComponent();
            Get();
        }
        public async void Get()
        {
            try
            {
                var content = await client.GetStringAsync(Url);
                List<Datos> post = JsonConvert.DeserializeObject<List<Datos>>(content);
                _post = new ObservableCollection<Datos>(post);
                lstPaciente.ItemsSource = post;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        private async void btnNuevoPaciente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarPaciente());
        }
        private async void btnEliminaPaciente_Clicked(object sender, EventArgs e)
        {
           
                if (cedulaPaciente != "")
                {
                    string Uri = "http://10.20.23.251/citaMedica/post.php?cedulaPaciente={0}";
                    try
                    {
                        HttpClient client = new HttpClient();
                        var uri = new Uri(string.Format(Uri, cedulaPaciente.ToString()));
                        var result = await client.DeleteAsync(uri);
                        if (result.IsSuccessStatusCode)
                        {
                            Get();
                            await DisplayAlert("Sucess", "Paciente " + nombrePaciente + " " + apellidoPaciente + " eliminado;", "OK");
                        }
                        else
                        {
                            await DisplayAlert("Error", "Error consulte con el administrador.", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alerta", "Ocurrio un error", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Alerta", "No se ha seleccionado un registro", "Ok");
                }
            
        }
    }
}