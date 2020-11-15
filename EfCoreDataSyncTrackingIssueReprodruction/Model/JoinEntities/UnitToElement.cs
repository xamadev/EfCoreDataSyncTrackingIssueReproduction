using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Model.Entities.JoinEntities
{
    public class UnitToElement
    {
        public int? FK_Unit { get; set; }
        public virtual Unit Unit { get; set; }

        public int? FK_Element { get; set; }
        public virtual Element Element { get; set; }

        public virtual ICollection<Contact> Contacts { get; } = new List<Contact>();
    }
}