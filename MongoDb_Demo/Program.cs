using System;

namespace MongoDb_Demo
{

    // install the MongoDb.Driver in NuGet Packages
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("AddressBook");

            // insert
            //PersonModel person = new PersonModel
            //{
            //    FirstName = "Sally",
            //    LastName = "Smith",
            //    PrimaryAddress = new AddressModel
            //    {
            //        StreetAddress = "404 Oak St",
            //        City = "Austin",
            //        State = "Texas",
            //        ZipCode = "50505"
            //    }
            //};
            //db.InsertRecord("Users", person);


            //get list
            //var records = db.LoadRecods<PersonModel>("Users");

            //foreach (var r in records)
            //{
            //    Console.WriteLine($"{r.Id}: {r.FirstName} {r.LastName}");

            //    if (r.PrimaryAddress != null)
            //    {
            //        Console.WriteLine(r.PrimaryAddress.City);
            //    }

            //    Console.WriteLine();
            //}


            // retrieve individual account
            var person = db.LoadRecordById<PersonModel>("Users", new Guid("6fa5971e-ac7c-45df-9fc6-5f7da09c47fd"));

            // update record
            person.DateOfBirth = new DateTime(1988, 10, 25, 0, 0, 0, DateTimeKind.Utc);
            db.UpsertRecord("Users", person.Id, person);


            // delete record
            db.DeleteRecord<PersonModel>("Users", person.Id);
            //Console.WriteLine($"{person.Id}: {person.FirstName} {person.DateOfBirth}");

            Console.ReadKey();
        }
    }



}
