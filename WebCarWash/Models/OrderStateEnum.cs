using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCarWash.Models
{
    public enum  OrderState
    {
        None=0,
        IsForm=1,         // формируется
        IsConfirmed,    // подтвержден
        IsCancel,       // отмена
        IsDone          // выполнен
    }
}