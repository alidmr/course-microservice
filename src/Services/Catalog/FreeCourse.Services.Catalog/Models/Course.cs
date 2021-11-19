using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FreeCourse.Services.Catalog.Models
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public string Picture { get; set; }

        public string UserId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public Feature Feature { get; set; }

        public string CategoryId { get; set; }

        [BsonIgnore]
        public Category Category { get; set; }  // mongo db tarafında collectionda olmasın ignore ediyoruz
    }
}
