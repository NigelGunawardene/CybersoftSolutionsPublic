using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.ApplicationCore.Entities
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
