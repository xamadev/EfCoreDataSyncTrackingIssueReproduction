﻿using System.Collections.Generic;
using Model.Entities;
using Model.Entities.JoinEntities;

namespace Model.Data.DTO
{
    public class DataContextDto
    {
        public List<Contact> Contacts { get; set; } =  new List<Contact>();
        public List<Element> Elements { get; set; } = new List<Element>();
        public List<Unit> Units { get; set; } = new List<Unit>();
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<Function> Functions { get; set; } = new List<Function>();
    }
}
