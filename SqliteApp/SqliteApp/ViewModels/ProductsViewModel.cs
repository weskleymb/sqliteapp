using SqliteApp.Daos;
using SqliteApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace SqliteApp.ViewModels
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        private readonly IProductsRepository _productsRepository;

        public string ProductTitle { get; set; }
        public double ProductPrice { get; set; }

        private IList<Product> _products;
        public IList<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    Products = _productsRepository.GetProductsAsync();
                });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new Command(() =>
                {
                    var product = new Product
                    {
                        Title = ProductTitle,
                        Price = ProductPrice,
                    };
                    _productsRepository.InsertProductAsync(product);
                    Products = _productsRepository.GetProductsAsync();
                });
            }
        }

        public ProductsViewModel(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
            Products = _productsRepository.GetProductsAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
