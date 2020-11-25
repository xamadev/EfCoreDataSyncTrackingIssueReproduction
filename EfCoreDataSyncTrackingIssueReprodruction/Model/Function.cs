using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities;
using Newtonsoft.Json;

namespace Model.Entities
{
    public class Function
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        
        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        public override string ToString()
        {
            return Desc;
        }
    }
}