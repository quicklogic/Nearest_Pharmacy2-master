using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nearest_Pharmacy.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketPage : FreshBaseContentPage
    {
        public BasketPage()
        {
            InitializeComponent();
        }

        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var menuItem= ((MenuItem)sender);
            
        }
    }
}