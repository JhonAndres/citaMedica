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
    public partial class ActualizarPaciente : ContentPage
    {

        public ActualizarPaciente(string cedulaPaciente, string nombrePaciente, string apellidoPaciente, string correoPaciente, string telefonoPaciente, string direccionPaciente, string estadoPaciente)
        {
            InitializeComponent();
            txtCedula.Text = cedulaPaciente.ToString();
            txtNombre.Text = nombrePaciente;
            txtApellido.Text = apellidoPaciente;
            txtCorreo.Text = correoPaciente;
            txtTelefono.Text = telefonoPaciente;
            txtDireccion.Text = direccionPaciente;
            txtEstado.Text= estadoPaciente;

        }

        public const string Url = "http://10.20.23.251/citaMedica/post.php?cedulaPaciente={0}&nombrePaciente={1}&apellidoPaciente={2}&correoPaciente={3}&telefonoPaciente={4}&direccionPaciente={5}&estadoPaciente={6}";
        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            WebClient cliente = new WebClient();
            try
            {
                using (var webClient = new WebClient())
                {
                    var uri = new Uri(string.Format(Url,
                        txtCedula.Text, txtNombre.Text, txtApellido.Text, txtCorreo.Text, txtTelefono.Text, txtDireccion.Text, txtEstado.Text));
                    webClient.UploadString(uri, "PUT", string.Empty);
                    DisplayAlert("Sucess", "Registro de " + txtNombre.Text + " " + txtApellido.Text + " actualizado correctamente",
                       "Cerrar");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "Cerrar");
            }
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListarPaciente());

        }
    }
}