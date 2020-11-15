using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.JoinEntities;
using Newtonsoft.Json;

namespace Model.Entities
{
    public class Unit
    {
        public int Id { get; set; }
        public string Desc { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public virtual ICollection<Element> Elements { get; set; } = new List<Element>();
    }
}