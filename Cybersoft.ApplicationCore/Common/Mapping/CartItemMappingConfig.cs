using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Common.Mapping
{
    internal class CartItemMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CartItems, CartItemModel>().Map(dest => dest.Price, src => src.Price);

        }
    }
}
