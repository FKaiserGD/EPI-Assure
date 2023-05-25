using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace backend.models
{
    public class Solicitacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string _id { get; set; }

        [BsonElement("codigoUsuario")]
        public string CodigoUsuario { get; set; }

        [BsonElement("titulo")]
        public string Titulo { get; set; }

        [BsonElement("descricao")]
        public string Descricao { get; set; }

        [BsonElement("data")]
        public DateTime Data { get; set; }
    }
}