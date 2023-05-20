using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.models
{
    public class Equipamento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string _id { get; set; }
        
        [BsonElement("codigo")]
        public string Codigo { get; set; }
        [BsonElement("nome")]
        public string Nome { get; set; }
        [BsonElement("descricao")]
        public string Descricao { get; set; }
        [BsonElement("linkImagem")]
        public string LinkImagem { get; set; }
    }
}