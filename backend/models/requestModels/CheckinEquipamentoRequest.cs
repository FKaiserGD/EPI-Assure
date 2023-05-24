using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.models.requestModels
{
    public class CheckinEquipamentoRequest
    {
        
        [BsonElement("codigo")]
        public string Codigo { get; set; }
        [BsonElement("nome")]
        public string Nome { get; set; }
    }
}