
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PapersApi.Models;

public class GraphData{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id {get; set;}
    public required string user {get; set;}
    public required int year {get; set;}
    public required Node[]? authors {get;set;}
    public required Link[]? relationship {get;set;}

}