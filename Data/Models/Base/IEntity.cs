using System;

namespace Catstagram.Server.Data.Models.Base
{
    public interface IEntity
    {
        public DateTime CreatedOn { get; set; }

        string CreatedBy { get; set; }

        DateTime? ModifiedOn { get; set; }

        string ModifiedBy { get; set; }

    }
}
