using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citaMedica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarPaciente : ContentPage
    {
        private const string Url = "http://10.20.23.251/citaMedica/post.php";
        public AgregarPaciente()
        {
            InitializeComponent();
        }

        private async void btnInsertar_Clicked(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            try
            {
                var parameters = new System.Collections.Specialized.NameValueCollection();
                parameters.Add("cedulaPaciente", entCedula.Text);
                parameters.Add("nombrePaciente", entNombre.Text);
                parameters.Add("apellidoPaciente", entApellido.Text);
                parameters.Add("correoPaciente", entCorreo.Text);
                parameters.Add("telefonoPaciente", entTelefono.Text);
                parameters.Add("direccionPaciente", entDireccion.Text);
                parameters.Add("estadoPaciente", entEstado.Text);

                client.UploadValues(Url, "POST", parameters);
                // DisplayAlert("Completados", "Paciente Registrado", "Cerrar");
                await Navigation.PushAsync(new ListarPaciente());
                Limpiar();


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                DisplayAlert("Error", "Se produjo un error comuniquese con el Administrador", "Cerrar");

            }
        }

       
        public void Limpiar()
        {
            entCedula.Text = String.Empty;
            entNombre.Text = String.Empty;
            entApellido.Text = String.Empty;
            entCorreo.Text = String.Empty;
            entTelefono.Text = String.Empty;
            entDireccion.Text= String.Empty;
            entEstado.Text= String.Empty;   
        }
        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListarPaciente());

        }
    }
}