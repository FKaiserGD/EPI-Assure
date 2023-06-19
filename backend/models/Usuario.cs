using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("codigo")]
        public string Codigo { get; set; }
        [BsonElement("cpf")]
        public string CPF { get; set; }
        [BsonElement("nomeCompleto")]
        public string NomeCompleto { get; set; }
        [BsonElement("nomeSocial")]
        public string NomeSocial { get; set; }
        [BsonElement("cargo")]
        public string Cargo { get; set; }
        [BsonElement("emailCorporativo")]
        public string EmailCorporativo { get; set; }
        [BsonElement("fotoPerfil")]
        public string FotoPerfil { get; set; }
    }
}
