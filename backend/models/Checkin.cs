using backend.models.requestModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.models
{
    public class Checkin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string _id { get; set; }

        [BsonElement("codigoUsuario")]
        public string CodigoUsuario { get; set; }
        [BsonElement("data")]
        public DateTime Data { get; set; }
        [BsonElement("linkImagemComprobatoria")]
        public string LinkImagemComprobatoria { get; set; }
        [BsonElement("equipamentos")]
        public List<CheckinEquipamentoRequest> Equipamentos { get; set; }
    }
}