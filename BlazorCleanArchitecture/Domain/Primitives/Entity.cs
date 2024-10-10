using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitives
{
    /// <summary>
    /// Base type for all database Entities
    /// </summary>
    public abstract class Entity
    {
        public int Id { get; set; }
    }
}
