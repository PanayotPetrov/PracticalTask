namespace PracticalTask.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum Status
    {
        Active = 0,
        ReadyToSave = 1,
        Saved = 2,
        Cancelled = 3,
    }
}
