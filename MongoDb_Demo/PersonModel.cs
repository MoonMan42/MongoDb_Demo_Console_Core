using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDb_Demo
{
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



}
