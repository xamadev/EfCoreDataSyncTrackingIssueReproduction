using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Model.Entities.JoinEntities;
using Castle.Core.Internal;
using Newtonsoft.Json;

namespace Model.Entities
{
    public class Contact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Required]  
        public string Id { get; set; }

        public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();

        public virtual ICollection<UnitToElement> Responsibilities { get; set; } = new List<UnitToElement>();
    }
}