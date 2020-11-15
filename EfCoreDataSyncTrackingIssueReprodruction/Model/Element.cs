using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.JoinEntities;
using Newtonsoft.Json;

namespace Model.Entities
{
    public class Element
    {
        public int Id { get; set; }
        public string Desc { get; set; }

        public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}