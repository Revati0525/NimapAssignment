using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CRUD_operation.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
    }

}
