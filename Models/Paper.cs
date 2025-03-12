
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PapersApi.Models;

public class Paper{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id {get; set;}
    public string? user {get; set;}
    public required string raw {get;set;}

}