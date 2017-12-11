using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Nearest_Pharmacy.Models;
using System.Collections.ObjectModel;
using PropertyChanged;

namespace Nearest_Pharmacy.PageModels
{
    [ImplementPropertyChanged]
    class OrderPageModel:FreshBasePageModel
    {
        IPharmacyService _pharmacyService;
        public ObservableCollection<DeliveryMethod> deliveryMethods { get; set; }
        object Select { get; set; }

        public OrderPageModel(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
            Init();
        }

        public async void Init()
        {
            try
            {
                deliveryMethods = new ObservableCollection<DeliveryMethod>();
                IEnumerable<DeliveryMethod> getObjects = await _pharmacyService.GetDeliveryMethods();
                foreach (DeliveryMethod o in getObjects)
                {
                    deliveryMethods.Add(o);
                }
            }
            catch(Exception e)
            {
                await CoreMethods.DisplayAlert("", Convert.ToString(e), "Ok");
            }
        }
    }
}
