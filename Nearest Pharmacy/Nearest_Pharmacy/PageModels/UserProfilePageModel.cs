using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using Nearest_Pharmacy.Models;
using Xamarin.Forms;
using PropertyChanged;

namespace Nearest_Pharmacy.PageModels
{
    [ImplementPropertyChanged]
    public class UserProfilePageModel: FreshBasePageModel
    {
        IPharmacyService _pharmacyService;

        public UserProfilePageModel(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
            isEdit = false;
            Mode = "Редактировать";
            Initial();
        }

        public bool isEdit { get; set; }
        public string Mode { get; set; }
        public string Name { get; set; }
        public string BornDate { get; set; }
        public string Number { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        //public UserInfo User { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
        }

        public async void Initial()
        {
            try
            {
                UserInfo user = new UserInfo();
                string login = Convert.ToString(Xamarin.Forms.Application.Current.Properties["UserLogin"]);
                var MyobjList = await _pharmacyService.GetUserInfo(login);
                user = MyobjList[0];
                Name = user.FirstName;
                BornDate = user.BornDate.Substring(0, user.BornDate.IndexOf(@"T"));
                Number = user.Number;
                Mail = user.Mail;
                Address = user.Address;
            }
            catch (Exception e) 
            {
                await CoreMethods.DisplayAlert(Convert.ToString(e), "", "Ок");
            }
        }

   
        public ICommand editBtn => new Command(async (value) =>
        {
            if(isEdit != true)
            {
                isEdit = true;
                Mode = "Сохранить";
            }
            else
            {
                isEdit = false;
                UserInfo editedUser = new UserInfo
                {
                    login = Convert.ToString(Xamarin.Forms.Application.Current.Properties["UserLogin"]),
                    Number = Number,
                    Mail = Mail,
                    Address = Address
                };
               editedUser = await _pharmacyService.SaveInfo(editedUser);
                Mode = "Редактировать";
            }

        });

        public ICommand ExitAcc => new Command(async (value) =>
        {
            Xamarin.Forms.Application.Current.Properties.Remove("UserLogin");
            Xamarin.Forms.Application.Current.Properties["Auth"] = "False";
            await CoreMethods.PopToRoot(true);
        });
    }
}
