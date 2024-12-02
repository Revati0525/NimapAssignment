using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CRUD_operation.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

}
