using System;
using FreshMvvm;
using System.Collections.ObjectModel;
using Nearest_Pharmacy.Models;
using PropertyChanged;
using AutoMapper;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nearest_Pharmacy.PageModels
{
    [ImplementPropertyChanged]
    public class BasketPageModel:FreshBasePageModel
    {
        public ObservableCollection<Basket> basket { get; set; }
        public bool Refresh { get; set; }
        public bool isEmpty = false;
        public BasketPageModel()
        {
            if (isEmpty !=true)
            {
                basket = new ObservableCollection<Basket>(Application.Database.GetBasket());
            }
        }

        public async override void Init(object initData)
        {
            if (initData != null)
            {
                try
                {
                    Product productItem = (Product)initData;
                    Mapper.Initialize(cfg => cfg.CreateMap<Product, Basket>());  
                    var basketItems = Mapper.Map<Product, Basket>(productItem);
                    
                  
                    Application.Database.AddBasket(basketItems);
                    basket = new ObservableCollection<Basket>(Application.Database.GetBasket());
                    isEmpty = false;
                }
                catch(Exception e)
                {
                    await CoreMethods.DisplayAlert("Произошла ошибка", Convert.ToString(e), "Ок");
                }
            }
        }

        public ICommand DeleteCommand => new Command((value) =>
        {
            Basket item = value as Basket;
            Application.Database.DeleteBasketItem(item);
            basket.Remove(item);
        });

        public ICommand OrderCommand => new Command((value) =>
        {
            var items = basket;
            CoreMethods.PushPageModel<OrderPageModel>();
        });
    }
}
