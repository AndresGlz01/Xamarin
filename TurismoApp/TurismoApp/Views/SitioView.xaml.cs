using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurismoApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurismoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SitioView : ContentPage
    {
        public SitioView()
        {
            InitializeComponent();
        }

        private async void SwipeItem_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Por favor, Confirmar eliminación", "¿Está usted seguro de eliminar?", "Sí, Eliminar", "No, Cancelar") == true)
            {
                ((SitioViewModel)BindingContext).EliminarCommand.Execute(((SwipeItem)sender).CommandParameter);
            }
        }
    }
}