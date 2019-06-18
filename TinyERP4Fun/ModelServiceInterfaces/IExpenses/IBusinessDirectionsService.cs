﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Models.Expenses;

namespace TinyERP4Fun.ModelServiceInterfaces
{
    public interface IBusinessDirectionsService : IAllServises<BusinessDirection>, ISimpleList<BusinessDirection>
    {
    }
}
