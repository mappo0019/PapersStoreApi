using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace PapersApi.Models;

public class Login{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id {get; set;}
    public required string user {get; set;}
    public required string password {get; set;}

}