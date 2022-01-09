using FinalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class CheckoutVM
    {
        public List<CartItem> Carts { get; set; }
        public UserAddress UserAddressDefault { get; set; }
    }
}
