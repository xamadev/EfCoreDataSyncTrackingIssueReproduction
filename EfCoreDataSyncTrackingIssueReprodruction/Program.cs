using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using Model.Data;
using Model.Data.DTO;
using Model.Entities;
using Model.Entities.JoinEntities;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace EfCoreDataSyncTrackingIssueReprodruction
{
    class Program
    {
        static void Main(string[] args)
        {
            Seed();
        }

        private static DataContext GetContextSqlServer()
        {
            var _connectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=testdb;MultipleActiveResultSets=True;";

            return new DataContext(new DbContextOptionsBuilder<DataContext>().UseLazyLoadingProxies().EnableSensitiveDataLogging()
                .UseSqlServer(new SqlConnection(_connectionString)).Options);
        }

        private static DataContext GetContextSqlite()
        {
            return new DataContext(new DbContextOptionsBuilder<DataContext>().UseLazyLoadingProxies().EnableSensitiveDataLogging()
                .UseSqlite("Filename=test.sqlite").Options);
        }

        static void Seed()
        {
            string json;

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            using (var context = GetContextSqlServer())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var elements = new[]
                {
                    new Element {Desc = "Desc1"}, new Element {Desc = "Desc2"}, new Element {Desc = "Desc3"},
                    new Element {Desc = "Desc4"}, new Element {Desc = "Desc5"}, new Element {Desc = "Desc6"},
                };

                context.Elements.AddRange(elements);

                var units = new[]
                {
                    new Unit {Desc = "Desc1"}, new Unit {Desc = "Desc2"}, new Unit {Desc = "Desc3"}, new Unit {Desc = "Desc4"}
                };

                context.Units.AddRange(units);

                var unitsToElements = new[]
                {
                    new UnitToElement {Unit = units[0], Element = elements[0]},
                    new UnitToElement {Unit = units[0], Element = elements[1]},
                    new UnitToElement {Unit = units[1], Element = elements[0]},
                    new UnitToElement {Unit = units[1], Element = elements[2]},
                    new UnitToElement {Unit = units[2], Element = elements[1]},
                    new UnitToElement {Unit = units[0], Element = elements[5]},
                    new UnitToElement {Unit = units[2], Element = elements[4]}
                };

                context.UnitsToElements.AddRange(unitsToElements);

                var contacts = new Contact[] {new Contact {Id = "1234567",}, new Contact {Id = "2345678"}};

                context.Contacts.AddRange(contacts);

                contacts[0].Units.Add(units[0]);
                contacts[0].Units.Add(units[2]);
                contacts[1].Units.Add(units[0]);
                contacts[1].Units.Add(units[1]);

                contacts[1].Responsibilities.Add(unitsToElements[1]);

                context.SaveChanges();

                var data = new DataContextDto()
                {
                    Units = context.Units.ToList(), Contacts = context.Contacts.ToList(), Elements = context.Elements.ToList(),
                };

                json = JsonConvert.SerializeObject(data,
                    jsonSerializerSettings);

                Debug.WriteLine(json);
            }

            var dataContextDto = JsonConvert.DeserializeObject<DataContextDto>(json,
                jsonSerializerSettings);

            using (var context = GetContextSqlite())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Units.AddRange(dataContextDto.Units);
                context.Contacts.AddRange(dataContextDto.Contacts);
                context.Elements.AddRange(dataContextDto.Elements);

                context.SaveChanges();
            }
        }
    }
}