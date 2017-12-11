using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using System.Windows.Input;
using Nearest_Pharmacy.Models;

namespace Nearest_Pharmacy.PageModels
{
    [ImplementPropertyChanged]
    class MenuPageModel : FreshBasePageModel
    {
        public MenuPageModel()
        {
            if (Convert.ToString(Xamarin.Forms.Application.Current.Properties["Auth"]) == "True")
            {
                IsLoggingIn = true;
            }
            else
            {
                IsLoggingIn = false;
            }
        }
           
        public bool IsLoggingIn { get; set; }
       

        public override void Init(object initData)
        {
            base.Init(initData);  
        }
     
        public ICommand goMain => new Command(async (value) =>
        {
            await CoreMethods.PopToRoot(true);
        });
        public ICommand goUserProfile => new Command(async (value) =>
        {
            if (Convert.ToString(Xamarin.Forms.Application.Current.Properties["Auth"]) == "True")
            {
                await CoreMethods.PopToRoot(true);
                await CoreMethods.PushPageModel<UserProfilePageModel>();
            }
            else
            {
                await CoreMethods.DisplayAlert("Внимание:", "Требуется авторизация.", "Oк");
                await CoreMethods.PopToRoot(true);
                await CoreMethods.PushPageModel<LoginPageModel>();
            }
            
        });
        public ICommand goOrders => new Command(async (value) =>
        {
            if (Convert.ToString(Xamarin.Forms.Application.Current.Properties["Auth"]) == "True")
            {
                await CoreMethods.PopToRoot(true);
                await CoreMethods.PushPageModel<ProductListPageModel>();
            }
            else
            {
                await CoreMethods.DisplayAlert("Внимание:", "Требуется авторизация.", "Oк");
                await CoreMethods.PopToRoot(true);
                await CoreMethods.PushPageModel<LoginPageModel>();
            }
        });
        public ICommand goBasket => new Command(async (value) =>
        {
            if (Convert.ToString(Xamarin.Forms.Application.Current.Properties["Auth"]) == "True")
            {
                await CoreMethods.PopToRoot(true);
                await CoreMethods.PushPageModel<BasketPageModel>();
            }
            else
            {
                await CoreMethods.DisplayAlert("Внимание:", "Требуется авторизация.", "Oк");
                await CoreMethods.PopToRoot(true);
                await CoreMethods.PushPageModel<LoginPageModel>();
            }
        });


        public ICommand goLogin => new Command(async async =>
        {
            if (Convert.ToString(Xamarin.Forms.Application.Current.Properties["Auth"]) != "True")
            {
                await CoreMethods.PopToRoot(true);
                await CoreMethods.PushPageModel<LoginPageModel>();
            }
            else await CoreMethods.DisplayAlert("Внимание:", "Вы уже авторизованы, чтобы войти как другой пользователь, требуется выйти из этого аккаунта.", "Oк");
        });

        public ICommand goRegister => new Command(async async =>
        {
            if (Convert.ToString(Xamarin.Forms.Application.Current.Properties["Auth"]) != "True")
            {
                await CoreMethods.PopToRoot(true);
                await CoreMethods.PushPageModel<RegisterPageModel>();
            }
            else await CoreMethods.DisplayAlert("Внимание:", "Чтобы зарегистрироваться, требуется выйти из этого аккаунта.", "Oк");
        });
    }
}
