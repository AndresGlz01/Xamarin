using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using TurismoApp.Models;
using TurismoApp.Views;
using Xamarin.Forms;

namespace TurismoApp.ViewModels
{
    public class SitioViewModel : INotifyPropertyChanged
    {
        public Sitio Sitio { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Sitio> Sitios { get; set; } = new ObservableCollection<Sitio>();
        public ICommand AgregarCommand { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand VerDetallesCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand ModificarCommand { get; set; }


        public SitioViewModel()
        {
            Abrir();
            AgregarCommand = new Command(Agregar);
            CambiarVistaCommand = new Command<string>(CambiarVista);
            EliminarCommand = new Command<Sitio>(Eliminar);
            VerDetallesCommand = new Command<Sitio>(VerDetalles);
            ModificarCommand = new Command<Sitio>(Modificar);
            GuardarCommand = new Command(Guardar);
        }

        AgregarSitioView AgregarSitio;
        EditarSitioView EditarSitio;
        DetalleSitioView DetalleSitio;

        int posicion;
        private void Modificar(Sitio Sitio)
        {
            posicion = Sitios.IndexOf(Sitio);
            this.Sitio = new Sitio
            {
                Nombre = Sitio.Nombre,
                Descripcion = Sitio.Descripcion,
                Imagen = Sitio.Imagen,
                Dirección = Sitio.Dirección
            };

            EditarSitio = new EditarSitioView() { BindingContext = this };
            Application.Current.MainPage.Navigation.PushAsync(EditarSitio);
        }

        private void Guardar()
        {
            Sitios[posicion] = Sitio;
            Serializar();
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        private void VerDetalles(Sitio Sitio)
        {
            this.Sitio = Sitio;
            DetalleSitio = new DetalleSitioView() { BindingContext = this };
            Application.Current.MainPage.Navigation.PushAsync(DetalleSitio);
            PropertyChanged(this, new PropertyChangedEventArgs(null));
        }

        private void Eliminar(Sitio Sitio)
        {
            Sitios.Remove(Sitio);
            Serializar();
            Application.Current.MainPage.Navigation.PopToRootAsync();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        private void Agregar()
        {
            Sitios.Add(Sitio);
            Serializar();
            CambiarVista("ver");
        }

        private void CambiarVista(string Vista)
        {
            if (Vista == "agregar")
            {
                Sitio = new Sitio();
                AgregarSitio = new AgregarSitioView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(AgregarSitio);
                PropertyChanged(this, new PropertyChangedEventArgs(null));
            }
            if (Vista == "ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        private void Serializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "Sitios.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(Sitios));
        }
        private void Abrir()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "Sitios.json";
            if (File.Exists(file))
            {
                Sitios = JsonConvert.DeserializeObject<ObservableCollection<Sitio>>(File.ReadAllText(file));
            }
        }
    }
}
