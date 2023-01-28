using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Common.Mapping;

internal class OrdersMappingConfig : IRegister
{
    public object OrderDetailModel { get; private set; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Orders, OrderModel>()
            .Map(dest => dest.OrderDate, src => DateOnly.FromDateTime(src.OrderDate).ToString())
            .ShallowCopyForSameType(true);
    }
}
