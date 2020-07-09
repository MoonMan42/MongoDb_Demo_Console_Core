# MongoDb_Demo_Console_Core
Mongo DB test and tutorial

1. install the MongoDb.driver nuget package. 

1. setup a Model 

        public class PersonModel
        {
            [BsonId]  // primary key (_id)
            public Guid Id { get; set; }
            public String FirstName { get; set; }
            public String LastName { get; set; }

            public AddressModel PrimaryAddress { get; set; }

            [BsonElement("dob")]
            public DateTime DateOfBirth { get; set; }

        }

        public class AddressModel
        {
            public string StreetAddress { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
        }
    
    
1. setup the CRUD operations for accessing the database/files

       public class MongoCRUD
        {
            // main db connection
            private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient(); // setup the client
            db = client.GetDatabase(database); // create the connection to the db
        }
        
        // insert into the databese
        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table); // retrieve collection(table)

            collection.InsertOne(record);
        }
        
        // retrieve all documents in a List
        public List<T> LoadRecods<T>(string table)
        {
            var collection = db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        // retrieve record by ID
        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);

            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        // Merge/Update record. 
        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            // if record is found then update
            // if not found insert new record
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new ReplaceOptions { IsUpsert = true });
        }

        // Delete record
        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
        }
    
    
    
1. set db connection and call functions

        MongoCRUD db = new MongoCRUD("AddressBook");
    
1. create new record and insert it into the database and set it to a new table

            PersonModel person = new PersonModel
            {
                FirstName = "Sally",
                LastName = "Smith",
                PrimaryAddress = new AddressModel
                {
                    StreetAddress = "404 Oak St",
                    City = "Austin",
                    State = "Texas",
                    ZipCode = "50505"
                }
            };
            
                            // Table, record to insert
            db.InsertRecord("Users", person);
            
1. Get list of all records
  
        var records = db.LoadRecods<PersonModel>("Users");

1. retrieve individual accounts by ID 
  
          var person = db.LoadRecordById<PersonModel>("Users", new Guid("6fa5971e-ac7c-45df-9fc6-5f7da09c47fd"));

1. Update a record by ID above
  
        person.DateOfBirth = new DateTime(1988, 10, 25, 0, 0, 0, DateTimeKind.Utc);
        db.UpsertRecord("Users", person.Id, person);
  
1. Delete a record by ID above 

        db.DeleteRecord<PersonModel>("Users", person.Id);
  
  
