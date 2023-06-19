using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.models
{
    public class Conteudo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Texto { get; set; }
    }
}
