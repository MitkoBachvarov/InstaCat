using System;

namespace Catstagram.Server.Data.Models.Base
{
    public abstract class Entity
    {
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
