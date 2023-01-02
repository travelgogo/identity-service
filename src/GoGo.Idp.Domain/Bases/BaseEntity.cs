using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Idp.Domain.Bases
{
    public class BaseEntity : CoreEntity
    {
        public int Id { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }

    }
}