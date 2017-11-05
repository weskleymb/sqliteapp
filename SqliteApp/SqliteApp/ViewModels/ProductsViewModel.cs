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

        private Product _product;
        public Product Product
        {
            get
            {
                return _product;
            }
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

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

        public bool Cursor { get; set; }
        
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
                    _productsRepository.InsertProductAsync(this.Product);
                    this.Products = _productsRepository.GetProductsAsync();
                    this.Product = new Product();
                    this.Cursor = true;
                });
            }
        }

        public ProductsViewModel(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
            this.Product = new Product();
            this.Products = _productsRepository.GetProductsAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
