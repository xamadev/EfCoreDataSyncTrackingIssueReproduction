using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities;
using Newtonsoft.Json;

namespace Model.Entities
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public string Desc { get; set; }

        public virtual ICollection<Element> Elements { get; set; } = new List<Element>();

        public override string ToString()
        {
            return Desc;
        }
    }
}