
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace PapersApi.Models;

public class User{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id {get; set;}
    public string? openAlex_id {get; set;}
    public required string name {get;set;}
    public required bool rol {get; set;}
    public string? photo {get; set;} 
    public string[]? project {get; set;}
    public string[]? magazines {get; set;}
    public string[]? coworkers {get; set;}

}