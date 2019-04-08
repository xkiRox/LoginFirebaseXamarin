using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Firebase
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Register_Clicked(object sender, System.EventArgs e)
        {
            HttpClient client = new HttpClient();

            LoginFirebase login = new LoginFirebase { 
                email = Email.Text,
                password = Password.Text,
                returnSecureToken = true
            };

            string JsonLogin = JsonConvert.SerializeObject(login);
            var content = new StringContent(JsonLogin, Encoding.UTF8, "application/json");

            var response = await  client.PostAsync(
                "https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=AIzaSyA-rLw8frL5Ztf-oSiF4J8KPJXC1errXqU",
                content);

            var StringResponse = response.Content.ReadAsStringAsync().Result;

            var ModelResponse = JsonConvert.DeserializeObject<RsposneFirebase>(StringResponse);

            if(!string.IsNullOrEmpty(ModelResponse.email))
                await DisplayAlert("USuario", $"El Usuario {ModelResponse.email} fue registrado", "Salir");
            else
                await DisplayAlert("Error", $"El Usuario ya se encuentra registrado", "Salir");
        }

        async void Login_Clicked(object sender, System.EventArgs e)
        {
            HttpClient client = new HttpClient();

            LoginFirebase login = new LoginFirebase
            {
                email = Email.Text,
                password = Password.Text,
                returnSecureToken = true
            };

            string JsonLogin = JsonConvert.SerializeObject(login);
            var content = new StringContent(JsonLogin, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(
                "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=AIzaSyA-rLw8frL5Ztf-oSiF4J8KPJXC1errXqU",
                content);

            var StringResponse = response.Content.ReadAsStringAsync().Result;

            var ModelResponse = JsonConvert.DeserializeObject<ResponseLoginFirebase>(StringResponse);

            if (ModelResponse.registered)
                await DisplayAlert("USuario", $"El Usuario {ModelResponse.email} fue logeuado", "Salir");
            else
                await DisplayAlert("Error", $"El usuario o contraseña son incorrectos", "Salir");
        }

    }
}
