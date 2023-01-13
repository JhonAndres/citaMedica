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
    public partial class AgendarCita : ContentPage
    {
       
        public AgendarCita()
        {
            InitializeComponent();
           

        }

        public string Url = "http://10.20.23.251/citaMedica/post1.php?cedulaPaciente={0}&nombrePaciente{1}";
        private async void btnInsertar_Clicked(object sender, EventArgs e)
        {

            WebClient client = new WebClient();
            try
            {
                var parameters = new System.Collections.Specialized.NameValueCollection();
                parameters.Add("nombreMedico", entMedico.Text);
                parameters.Add("especialidad", entEspecialidad.Text);
                parameters.Add("fechaCita", entFecha.Text);
                parameters.Add("horaCita", entHora.Text);

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

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListarPaciente());

        }
        public void Limpiar()
        {
            entEspecialidad.Text = String.Empty;
            entFecha.Text = String.Empty;
            entHora.Text = String.Empty;
            entMedico.Text = String.Empty;
            txtCedula.Text = String.Empty;
            txtPaciente.Text = String.Empty;
            
        }
    }
}